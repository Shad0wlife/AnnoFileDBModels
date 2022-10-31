using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;
using Microsoft.Win32;
using RDAExplorer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeGamedata_ManualTest
{
    public class RunOnGameFiles
    {
        public static string? GetInstallDirFromRegistry()
        {
            string installDirKey = @"SOFTWARE\WOW6432Node\Ubisoft\Anno 1800";
            using RegistryKey? key = Registry.LocalMachine.OpenSubKey(installDirKey);
            return key?.GetValue("InstallDir") as string;
        }

        public async Task RunOnAnnoGameFiles()
        {
            Console.WriteLine("Running on anno game Files.");
            UISettings.EnableConsole = false;

            string? gamePath = GetInstallDirFromRegistry();
            if (string.IsNullOrEmpty(gamePath))
            {
                return;
            }

            Console.WriteLine("Found game files at: " + gamePath);

            string outPath = Program.CreateCleanLocalOutputDir();
            string errorFilePath = Path.Combine(outPath, "_fileErrors.txt");

            Console.WriteLine("Opening game files.");
            IDataArchive archive = await DataArchive.OpenAsync(gamePath);
            int fileCount = archive.Files.Count();
            Console.WriteLine($"Finished scanning game files, found {fileCount} candidates.");

            IEnumerable<string> messages = RunOnArchiveParallel(archive, outPath, errorFilePath);
            File.WriteAllLines(errorFilePath, messages);
        }

        private IEnumerable<string> RunOnArchive(IDataArchive archive, string outFolder, string errorLog)
        {
            int fileCount = archive.Files.Count();
            int step = 0;

            List<string> failureMessages = new List<string>();

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            foreach (string file in archive.Files)
            {
                step++;
                Console.WriteLine($"[{step}/{fileCount}] - Handling a7m file: {file}");
                string fileName = Path.GetFileNameWithoutExtension(file);
                using (Stream? itemStream = archive.OpenRead(file))
                {
                    RDAReader reader = new RDAReader();
                    reader.ReadRDAFileFromStream(itemStream);

                    foreach (var rdaFile in reader.rdaFolder.GetAllFiles())
                    {
                        if (rdaFile.FileName == "gamedata.data")
                        {
                            using (MemoryStream memStream = new MemoryStream(rdaFile.GetData()))
                            {
                                try
                                {
                                    TestDeserializeGamedata(memStream, file);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error {ex.GetType()} with Message \"{ex.Message}\" on gamedata.data from {file}");

                                    string outFileName = Path.GetFileNameWithoutExtension(file) + "_" + "gamedata.data";
                                    File.WriteAllBytes(Path.Combine(outFolder, outFileName), memStream.ToArray());

                                    failureMessages.Add($"{outFileName} -> {ex.Message}");
                                }
                            }
                            break;
                        }
                    }
                }
                Console.WriteLine();
            }

            stopwatch.Stop();
            Console.WriteLine($"Tested against {fileCount} in {stopwatch.Elapsed}");

            return failureMessages;
        }

        private IEnumerable<string> RunOnArchiveParallel(IDataArchive archive, string outFolder, string errorLog)
        {
            int fileCount = archive.Files.Count();
            int step = 0;

            ConcurrentBag<string> failureMessages = new ConcurrentBag<string>();

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            ParallelLoopResult result = Parallel.ForEach(
                archive.Files, 
                new ParallelOptions() { MaxDegreeOfParallelism = 16 }, 
                file => {
                    step++;

                    //Task.Delay((step % 16) * 100);

                    Console.WriteLine($"[{step}/{fileCount}] - Handling a7m file: {file}");
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    using (Stream? itemStream = archive.OpenRead(file))
                    {
                        try
                        {
                            RDAReader reader = new RDAReader();
                            reader.ReadRDAFileFromStream(itemStream);

                            foreach (var rdaFile in reader.rdaFolder.GetAllFiles())
                            {
                                if (rdaFile.FileName == "gamedata.data")
                                {
                                    using (MemoryStream memStream = new MemoryStream(rdaFile.GetData()))
                                    {
                                        try
                                        {
                                            TestDeserializeGamedata(memStream, file);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Error {ex.GetType()} with Message \"{ex.Message}\" on gamedata.data from {file}");

                                            string outFileName = Path.GetFileNameWithoutExtension(file) + "_" + "gamedata.data";
                                            File.WriteAllBytes(Path.Combine(outFolder, outFileName), memStream.ToArray());

                                            failureMessages.Add($"{outFileName} -> {ex.Message}");
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(ex.Message + "\r\n" + file);
                        }
                        
                    }
                    Console.WriteLine();
                }
            );

            stopwatch.Stop();
            Console.WriteLine($"Tested against {fileCount} in {stopwatch.Elapsed}");

            return failureMessages;
        }

        private void TestDeserializeGamedata(Stream gamedataStream, string file)
        {
            Console.WriteLine($"Trying to parse gamedata.data from \"{file}\".");

            gamedataStream.Seek(0, SeekOrigin.Begin);

            var Version = VersionDetector.GetCompressionVersion(gamedataStream);

            gamedataStream.Seek(0, SeekOrigin.Begin);

            FileDBSerializer<Gamedata> deserializer = new FileDBSerializer<Gamedata>(Version);
            var deserializedResult = deserializer.Deserialize(gamedataStream);
            Console.WriteLine("Finished Deserializing.");
        }
    }
}
