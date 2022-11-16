using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Win32;
using RDAExplorer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SerializeGamedata_ManualTest
{
    public static class DataArchive
    {
        public static readonly IDataArchive Default = new InvalidDataPath("");

        public static async Task<IDataArchive> OpenAsync(string? folderPath, params string[] fileExtensions)
        {
            if (folderPath is null)
                return Default;

            var adjustedPath = AdjustDataPath(folderPath);

            if (adjustedPath is null)
                return Default;

            IDataArchive archive = Default;
            if (File.Exists(Path.Combine(adjustedPath, "maindata/data0.rda")))
                archive = new RdaDataArchive(adjustedPath);

            await archive.LoadAsync(fileExtensions);
            return archive;
        }

        private static string? AdjustDataPath(string? path)
        {
            if (path is null)
                return null;
            if (File.Exists(Path.Combine(path, "maindata/data0.rda")))
                return path;
            if (File.Exists(Path.Combine(path, "data0.rda")))
                return Path.GetDirectoryName(path);
            if (Directory.Exists(Path.Combine(path, "data/dlc01")))
                return path;
            if (Directory.Exists(Path.Combine(path, "dlc01")))
                return Path.GetDirectoryName(path);
            return null;
        }

        public static string? GetInstallDirFromRegistry()
        {
            string installDirKey = @"SOFTWARE\WOW6432Node\Ubisoft\Anno 1800";
            using RegistryKey? key = Registry.LocalMachine.OpenSubKey(installDirKey);
            return key?.GetValue("InstallDir") as string;
        }
    }

    public interface IDataArchive
    {
        bool IsValid { get; }
        string Path { get; }

        IEnumerable<string> Files { get; }
        IEnumerable<string> FilesFor(params string[] extensions);

        Stream? OpenRead(string filePath);
        IEnumerable<string> Find(string pattern);
        Task LoadAsync(params string[] forEndings);
    }

    public class InvalidDataPath : IDataArchive
    {
        public bool IsValid { get; } = false;
        public Stream? OpenRead(string filePath) => null;
        public string Path { get; }
        public IEnumerable<string> Files { get; }

        public InvalidDataPath(string path)
        {
            Path = path;
            Files = Enumerable.Empty<string>();
        }

        public Task LoadAsync(params string[] forEndings)
        {
            return Task.Run(() => { });
        }

        public IEnumerable<string> Find(string pattern)
        {
            return Array.Empty<string>();
        }

        public IEnumerable<string> FilesFor(params string[] extensions)
        {
            return Enumerable.Empty<string>();
        }
    }

    public static class RDAExtensions
    {
        public static RDAFile? GetFileByPath(this RDAReader that, string path)
        {
            Queue<string>? parts = new(Path.GetDirectoryName(path)?.Split('\\') ?? Array.Empty<string>());
            if (!parts.Any())
                return null;

            RDAFolder? currentFolder = that.rdaFolder;

            while (parts.Count > 0 && currentFolder is not null)
            {
                var part = parts.Dequeue();
                currentFolder = currentFolder.Folders.FirstOrDefault(x => x.Name == part);
            }

            if (currentFolder is null)
                return null;

            var fileName = Path.GetFileName(path);
            RDAFile? file = currentFolder.Files.FirstOrDefault(x => Path.GetFileName(x.FileName) == fileName);
            return file;
        }
    }

    public class RdaDataArchive : IDataArchive, IDisposable
    {
        public string Path { get; }
        public bool IsValid { get; } = true;

        private RDAReader[]? readers;

        private HashSet<string> allowedFileExtensions;

        private Dictionary<string, Dictionary<string, (RDAFile, (string, object))>> allFiles { get; } = new();

        private bool filesValid = false;
        private List<string> files = new List<string>();
        public IEnumerable<string> Files
        {
            get
            {
                if (!filesValid)
                {
                    files = new List<string>();
                    foreach (var dict in allFiles.Values)
                    {
                        files.AddRange(dict.Keys);
                    }
                    filesValid = true;
                }
                return files;
            }
        }

        public IEnumerable<string> FilesFor(params string[] extensions)
        {
            List<string> fileList = new List<string>();
            foreach(string s in extensions)
            {
                if (allFiles.ContainsKey(s))
                {
                    fileList.AddRange(allFiles[s].Keys);
                }
            }
            return fileList;
        }

        public RdaDataArchive(string folderPath)
        {
            Path = folderPath;
        }

        public async Task LoadAsync(params string[] forEndings)
        {
            allowedFileExtensions = new HashSet<string>(forEndings);
            await Task.Run(() =>
            {
                // let's skip a few to speed up the loading: 0, 1, 2, 3, 4, 7, 8, 9
                var archives = Directory.
                    GetFiles(System.IO.Path.Combine(Path, "maindata"), "*.rda")
                    // filter some rda we don't use for sure
                    .Where(x => System.IO.Path.GetFileName(x).StartsWith("data") &&
                        !x.EndsWith("data0.rda") && !x.EndsWith("data1.rda") && !x.EndsWith("data2.rda") && !x.EndsWith("data3.rda") &&
                        !x.EndsWith("data4.rda") && !x.EndsWith("data7.rda") && !x.EndsWith("data8.rda") && !x.EndsWith("data9.rda"))
                    // load highest numbers last to overwrite lower numbers
                    .OrderBy(x => int.TryParse(System.IO.Path.GetFileNameWithoutExtension(x)["data".Length..], out int result) ? result : 0);
                readers = archives.Select(x =>
                {
                    try
                    {
                        //Console.WriteLine("Opening archive: " + System.IO.Path.GetFileName(x));
                        var reader = new RDAReader
                        {
                            FileName = x
                        };
                        //Add a Lock per RDAReader to avoid reading at the same time and creating invalid a7t/a7m reads.
                        object readerLock = new object();
                        reader.ReadRDAFile();
                        foreach (var file in reader.rdaFolder.GetAllFiles())
                        {
                            string fileExtension = System.IO.Path.GetExtension(file.FileName);

                            if (!allowedFileExtensions.Contains(fileExtension))
                            {
                                continue;
                            }


                            if (!allFiles.ContainsKey(fileExtension))
                            {
                                allFiles.Add(fileExtension, new());
                            }

                            allFiles[fileExtension][file.FileName] = (file, (System.IO.Path.GetFileNameWithoutExtension(x), readerLock));

                            filesValid = false;
                        }
                        return reader;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"error loading RDAs from {x}");
                        Console.ResetColor();
                        return null;
                    }
                }).Where(x => x is not null).Select(x => x!).ToArray();

                if (readers.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"No .rda files found at {System.IO.Path.Combine(Path, "maindata")}");
                    Console.WriteLine($"Something went wrong opening the RDA files.\n\nDo you have another Editor or the RDAExplorer open by any chance?");
                    Console.ResetColor();
                }
            });
        }

        public Stream? OpenRead(string filePath)
        {
            string targetExt = System.IO.Path.GetExtension(filePath);
            Dictionary<string, (RDAFile, (string, object))> targetDict;
            if (!allFiles.ContainsKey(targetExt))
                return null;
            else
                targetDict = allFiles[targetExt];

            if (!IsValid || readers is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"archive not ready: {filePath}");
                Console.ResetColor();
                return null;
            }
            Stream? stream = null;

            if (!targetDict.TryGetValue(filePath.Replace('\\', '/'), out (RDAFile rdaFile, (string lockName, object readerLock) mutexPack) file) || file.rdaFile is null || file.mutexPack.readerLock is null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"not found in archive: {filePath}");
                Console.ResetColor();
                return null;
            }


            lock (file.mutexPack.readerLock)
            {
                try
                {
                    Console.WriteLine($"Acquired Lock for {file.mutexPack.lockName}");
                    stream = new MemoryStream(file.rdaFile.GetData());
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"error reading archive: {filePath}", e);
                    Console.ResetColor();
                }
            }

            Console.WriteLine($"Released Lock for {file.mutexPack.lockName}");

            return stream;
        }

        public IEnumerable<string> Find(string pattern)
        {
            if (!IsValid || readers is null)
                return Array.Empty<string>();

            Matcher matcher = new();
            matcher.AddIncludePatterns(new string[] { pattern });

            PatternMatchingResult result = matcher.Match(allFiles.Keys);

            return result.Files.Select(x => x.Path);
        }

        public void Dispose()
        {
            if (readers is not null)
            {
                foreach (var reader in readers)
                    reader.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
