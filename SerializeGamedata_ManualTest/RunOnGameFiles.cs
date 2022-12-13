using Anno.FileDBModels.Anno1800.Gamedata.Models.Shared;
using Anno.FileDBModels.Anno1800.MapTemplate;
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

namespace SerializeGamedata.ManualTest
{
    public class RunOnGameFiles
    {
        public RunOnGameFiles(bool excessiveMode)
        {
            ExcessiveMode = excessiveMode;
        }

        private bool ExcessiveMode { get; }

        public const string MAP_GAMEDATA_EXTENSION = ".a7t";
        public const string MAP_TEMPLATE_EXTENSION = ".a7tinfo";

        public const string ISLAND_GAMEDATA_EXTENSION = ".a7m";

        /// <summary>
        /// Test different values for best performance.
        /// </summary>
        private const int ParallelismCount = 16;
        private static readonly Dictionary<string, string> ReplaceOps = new Dictionary<string, string>()
            {
                { "Delayed Construction", "DelayedConstruction" },
                { "Bus Activation", "BusActivation" },
            };

        public static string? GetInstallDirFromRegistry()
        {
            string installDirKey = @"SOFTWARE\WOW6432Node\Ubisoft\Anno 1800";
            using RegistryKey? key = Registry.LocalMachine.OpenSubKey(installDirKey);
            return key?.GetValue("InstallDir") as string;
        }

        public async Task RunOnAnnoGameFiles()
        {
            Console.WriteLine("Running on anno game Files.");
            UISettings.EnableConsole = false; //Disable RDAExplorer Console Output

            string? gamePath = GetInstallDirFromRegistry();
            if (string.IsNullOrEmpty(gamePath))
            {
                return;
            }

            Console.WriteLine("Found game files at: " + gamePath);

            string outPath = Program.CreateCleanLocalOutputDir();

            Console.WriteLine("Opening game files.");
            IDataArchive archive = await DataArchive.OpenAsync(gamePath, MAP_GAMEDATA_EXTENSION, ISLAND_GAMEDATA_EXTENSION, MAP_TEMPLATE_EXTENSION);
            int fileCount = archive.Files.Count();
            Console.WriteLine($"Finished scanning game files, found {fileCount} candidates.");

            //Handle replace ops here, so the interpreter doesn't conflict with itself
            //by multithreadedly adding and removing the replace ops.
            RegisterSpaceTagReplaceOps();

            IEnumerable<string> messages;
            string errorFilePath;
            //Gamedata
            errorFilePath = Path.Combine(outPath, "_gamedata_fileErrors.txt");
            messages = RunOnArchiveParallel(archive, outPath, UnpackNestedGamedataAndTest, MAP_GAMEDATA_EXTENSION, ISLAND_GAMEDATA_EXTENSION);
            File.WriteAllLines(errorFilePath, messages);

            //Map Templates
            errorFilePath = Path.Combine(outPath, "_a7tinfo_fileErrors.txt");
            messages = RunOnArchiveParallel(archive, outPath, UnpackMapTemplatesAndTest, MAP_TEMPLATE_EXTENSION);
            File.WriteAllLines(errorFilePath, messages);

            //Clean up replace ops, just in case the program does not end here at some point in the future :D
            UnregisterSpaceTagReplaceOps();
        }

        private static void RegisterSpaceTagReplaceOps()
        {
            FileDBReader.src.XmlRepresentation.InvalidTagNameHelper.RegisterReplaceOperations(ReplaceOps);
        }

        private static void UnregisterSpaceTagReplaceOps()
        {
            FileDBReader.src.XmlRepresentation.InvalidTagNameHelper.UnregisterReplaceOperations(ReplaceOps);
        }


        #region TestRunners

        private IEnumerable<string> RunOnArchiveSequential(IDataArchive archive, string outFolder, Action<string, TestInformationParams> testFunction, params string[] fileExtensions)
        {
            IEnumerable<string> targetCollection = archive.FilesFor(fileExtensions);
            int fileCount = targetCollection.Count();

            TestInformationParams testInfo = new TestInformationParams(fileCount, archive, outFolder);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            foreach (string file in targetCollection)
            {
                testFunction(file, testInfo);
            }

            stopwatch.Stop();
            Console.WriteLine($"Tested against {fileCount} in {stopwatch.Elapsed}");

            return testInfo.failureMessages;
        }

        private IEnumerable<string> RunOnArchiveParallel(IDataArchive archive, string outFolder, Action<string, TestInformationParams> testFunction, params string[] fileExtensions)
        {
            IEnumerable<string> targetCollection = archive.FilesFor(fileExtensions);
            int fileCount = targetCollection.Count();

            TestInformationParams testInfo = new TestInformationParams(fileCount, archive, outFolder);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            ParallelLoopResult result = Parallel.ForEach(
                targetCollection, 
                new ParallelOptions() { MaxDegreeOfParallelism = ParallelismCount }, 
                file => {
                    testFunction(file, testInfo);
                }
            );

            stopwatch.Stop();
            Console.WriteLine($"Tested against {fileCount} in {stopwatch.Elapsed}");

            return testInfo.failureMessages;
        }

        #endregion



        #region Gamedata Tests
        private void UnpackNestedGamedataAndTest(string file, TestInformationParams testInfoTracker)
        {
            Interlocked.Increment(ref testInfoTracker.counter);

            Console.WriteLine($"[{testInfoTracker.counter}/{testInfoTracker.maxCount}] - Handling a7m/a7t file: {file}");
            string fileName = Path.GetFileNameWithoutExtension(file);
            using (Stream? itemStream = testInfoTracker.archive.OpenRead(file))
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
                                    TestDeserializeGamedata(memStream, file, testInfoTracker.outputFolder);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error {ex.GetType()} with Message \"{ex.Message}\" on gamedata.data from {file}");

                                    string outFileName = Path.GetFileNameWithoutExtension(file) + "_" + "gamedata.data";
                                    File.WriteAllBytes(Path.Combine(testInfoTracker.outputFolder, outFileName), memStream.ToArray());

                                    testInfoTracker.failureMessages.Add($"{outFileName} -> {ex.Message}");
                                }
                            }
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "\r\n" + file);
                }

            }
            Console.WriteLine();
        }

        private void TestDeserializeGamedata(Stream gamedataStream, string file, string outPath)
        {
            Console.WriteLine($"Trying to parse gamedata.data from \"{file}\".");
            string debugName = Path.GetFileNameWithoutExtension(file);

            IFileDBDocument fileDBDocument = Program.StreamToFileDbDoc(gamedataStream);

            TestResultWithFileContents testResult = Program.CompareTest<Gamedata>(fileDBDocument, ExcessiveMode);

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            if (testResult.Success)
            {
                Console.WriteLine($"[SUCCESS] De- and Reserialized File {debugName} matches original.");
            }
            else
            {
                Console.WriteLine($"[FAILURE] De- and Reserialized File {debugName} differs from original.");
                string orgFilePath = Path.Combine(outPath, debugName + "_org.xml");
                File.WriteAllText(orgFilePath, testResult.OriginalContent);

                string createdFilePath = Path.Combine(outPath, debugName + "_created.xml");
                File.WriteAllText(createdFilePath, testResult.CreatedContent);

                if(ExcessiveMode)
                {
                    string orgBinaryFilePath = Path.Combine(outPath, debugName + "_orgBinary.xml");
                    string createdBinaryFilePath = Path.Combine(outPath, debugName + "_createdBinary.xml");

                    File.WriteAllText(orgBinaryFilePath, testResult.OriginalContentWithBinaryData);
                    File.WriteAllText(createdBinaryFilePath, testResult.CreatedContentWithBinaryData);
                }
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
        }

        #endregion


        private void UnpackMapTemplatesAndTest(string file, TestInformationParams testInfoTracker)
        {
            Interlocked.Increment(ref testInfoTracker.counter);

            Console.WriteLine($"[{testInfoTracker.counter}/{testInfoTracker.maxCount}] - Handling a7tinfo file: {file}");
            string fileName = Path.GetFileNameWithoutExtension(file);
            using (Stream? itemStream = testInfoTracker.archive.OpenRead(file))
            {
                try
                {
                    TestDeserializeMapTemplate(itemStream, file, testInfoTracker.outputFolder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex.GetType()} with Message \"{ex.Message}\" on gamedata.data from {file}");

                    string outFileName = Path.GetFileNameWithoutExtension(file) + "_" + "gamedata.data";
                    using (FileStream fs = File.OpenWrite(Path.Combine(testInfoTracker.outputFolder, outFileName)))
                    {
                        itemStream?.CopyTo(fs);
                    }

                    testInfoTracker.failureMessages.Add($"{outFileName} -> {ex.Message}");
                }

            }
            Console.WriteLine();
        }

        private void TestDeserializeMapTemplate(Stream templateStream, string file, string outPath)
        {
            Console.WriteLine($"Trying to parse map template from \"{file}\".");
            string debugName = Path.GetFileNameWithoutExtension(file);

            IFileDBDocument fileDBDocument = Program.StreamToFileDbDoc(templateStream);

            TestResultWithFileContents testResult = Program.CompareTest<MapTemplateDocument>(fileDBDocument, false); //No nested data for excessive mode in a7tinfo

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            if (testResult.Success)
            {
                Console.WriteLine($"[SUCCESS] De- and Reserialized File {debugName} matches original.");
            }
            else
            {
                Console.WriteLine($"[FAILURE] De- and Reserialized File {debugName} differs from original.");
                string orgFilePath = Path.Combine(outPath, debugName + "_org.xml");
                File.WriteAllText(orgFilePath, testResult.OriginalContent);

                string createdFilePath = Path.Combine(outPath, debugName + "_created.xml");
                File.WriteAllText(createdFilePath, testResult.CreatedContent);

                if (ExcessiveMode)
                {
                    string orgBinaryFilePath = Path.Combine(outPath, debugName + "_orgBinary.xml");
                    string createdBinaryFilePath = Path.Combine(outPath, debugName + "_createdBinary.xml");

                    File.WriteAllText(orgBinaryFilePath, testResult.OriginalContentWithBinaryData);
                    File.WriteAllText(createdBinaryFilePath, testResult.CreatedContentWithBinaryData);
                }
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
        }


        public class TestInformationParams
        {
            public TestInformationParams(int itemCount, IDataArchive archive, string outputFolder)
            {
                maxCount = itemCount;
                counter = 0;
                this.archive = archive;
                failureMessages = new ConcurrentBag<string>();
                this.outputFolder = outputFolder;
            }

            public readonly int maxCount;
            public int counter;

            public readonly string outputFolder;

            public readonly IDataArchive archive;
            public readonly ConcurrentBag<string> failureMessages;
        }
    }
}
