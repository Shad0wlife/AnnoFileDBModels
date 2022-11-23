using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared;
using Anno_FileDBModels.Anno1800.MapTemplate;
using FileDBReader;
using FileDBReader.src;
using FileDBReader.src.XmlRepresentation;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SerializeGamedata_ManualTest
{
    public class RunOnIncludedTestdata
    {
        public RunOnIncludedTestdata(bool excessiveMode)
        {
            ExcessiveMode = excessiveMode;
        }

        private bool ExcessiveMode { get; }

        public string WorkingDirectory => Environment.CurrentDirectory;
        public string ProjectDirectory => Directory.GetParent(WorkingDirectory).Parent.Parent.FullName;

        public const string TestDataFolderName = "TestData";
        public const string MapTestDataFolderName = "Map";
        public const string IslandTestDataFolderName = "Island";
        public const string MapTemplateTestDataFolderName = "InterpretedTemplates";

        public string GetFilePathFromTestDataFolder(string testFolder, string fileName)
        {
            return Path.Combine(ProjectDirectory, TestDataFolderName, testFolder, fileName);
        }

        public void RunOnIncludedGamedata()
        {
            string newSnowflakePath = GetFilePathFromTestDataFolder(MapTestDataFolderName, @"moderate_snowflake_ss_01_gamedata.data");
            string oldMapPath = GetFilePathFromTestDataFolder(MapTestDataFolderName, @"gamedata_1408_old.data");
            string nwPoolMap = GetFilePathFromTestDataFolder(MapTestDataFolderName, @"colony01_l_03_gamedata.data");
            string scenario03Map = GetFilePathFromTestDataFolder(MapTestDataFolderName, @"scenario_03_colony_01_gamedata.data");
            string enbesaMap = GetFilePathFromTestDataFolder(MapTestDataFolderName, @"colony02_01_gamedata.data");
            string arcticMap = GetFilePathFromTestDataFolder(MapTestDataFolderName, @"colony_03_sp_gamedata.data");


            string communityIslandPath = GetFilePathFromTestDataFolder(IslandTestDataFolderName, @"community_island_a7m_gamedata.data");
            string scenario03StoryIsland01 = GetFilePathFromTestDataFolder(IslandTestDataFolderName, @"scenario03_storyisland_01_gamedata.data");

            List<string> allTestFiles = new List<string>()
            {
                newSnowflakePath,
                oldMapPath,
                nwPoolMap,
                scenario03Map,
                enbesaMap,
                arcticMap,
                communityIslandPath,
                scenario03StoryIsland01
            };

            string outPath = Program.CreateCleanLocalOutputDir();

            foreach (string testPath in allTestFiles)
            {
                string fileName = Path.GetFileName(testPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(testPath);
                TestResultWithFileContents testResult = CompareTest<Gamedata>(testPath, ExcessiveMode);

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                if (testResult.Success)
                {
                    Console.WriteLine($"[SUCCESS] De- and Reserialized File {fileName} matches original.");
                }
                else
                {
                    Console.WriteLine($"[FAILURE] De- and Reserialized File {fileName} differs from original.");
                    string orgFilePath = Path.Combine(outPath, fileNameWithoutExt + "_org.xml");
                    File.WriteAllText(orgFilePath, testResult.OriginalContent);

                    string createdFilePath = Path.Combine(outPath, fileNameWithoutExt + "_created.xml");
                    File.WriteAllText(createdFilePath, testResult.CreatedContent);

                    if(ExcessiveMode)
                    {
                        string orgBinaryFilePath = Path.Combine(outPath, fileNameWithoutExt + "_orgBinary.xml");
                        string createdBinaryFilePath = Path.Combine(outPath, fileNameWithoutExt + "_createdBinary.xml");

                        File.WriteAllText(orgBinaryFilePath, testResult.OriginalContentWithBinaryData);
                        File.WriteAllText(createdBinaryFilePath, testResult.CreatedContentWithBinaryData);
                    }
                }
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
            }
        }

        public void RunOnIncludedTemplates()
        {
            string campaign_ch03 = GetFilePathFromTestDataFolder(MapTemplateTestDataFolderName, @"campaign_chapter03_colony01.xml");
            string colonly02 = GetFilePathFromTestDataFolder(MapTemplateTestDataFolderName, @"colony02_01.xml");
            string capeMap = GetFilePathFromTestDataFolder(MapTemplateTestDataFolderName, @"moderate_c_01.xml");
            string islandarc = GetFilePathFromTestDataFolder(MapTemplateTestDataFolderName, @"moderate_islandarc_ss_01.xml");
            string scenario = GetFilePathFromTestDataFolder(MapTemplateTestDataFolderName, @"scenario_02_colony_01.xml");

            List<string> allTestFiles = new List<string>()
            {
                campaign_ch03,
                colonly02,
                capeMap,
                islandarc,
                scenario,
            };

            string outPath = Program.CreateCleanLocalOutputDir();

            string interpreterPath = Path.Combine(ProjectDirectory, @"TestData\InterpretedTemplates\UsedInterpreter\a7tinfo.xml");
            Interpreter interpr;

            using (FileStream interpreterStream = File.OpenRead(interpreterPath))
            {
                XmlDocument interpreterDocument = new();
                interpreterDocument.Load(interpreterStream);
                interpr = new Interpreter(interpreterDocument);
            }

            foreach (string testPath in allTestFiles)
            {
                string fileName = Path.GetFileName(testPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(testPath);



                using (FileStream fs = File.OpenRead(testPath))
                {
                    // load xml
                    XmlDocument xmlDocument = new();
                    xmlDocument.Load(fs);

                    // convert to bytes


                    XmlDocument xmlWithBytes = new XmlExporter().Export(xmlDocument, interpr);

                    // convert to FileDB
                    XmlFileDbConverter converter = new(FileDBDocumentVersion.Version3);
                    IFileDBDocument doc = converter.ToFileDb(xmlWithBytes);

                    // construct deserialize into objects
                    FileDBDocumentDeserializer<MapTemplateDocument> deserializer = new(new FileDBSerializerOptions() { IgnoreMissingProperties = true });
                    MapTemplateDocument res = deserializer.GetObjectStructureFromFileDBDocument(doc);
                }
            }
        }

        private TestResultWithFileContents CompareTest<T>(string path, bool excessiveMode) where T : class, new()
        {
            IFileDBDocument fileDBDoc = Program.FileToFileDbDoc(path);

            return Program.CompareTest<T>(fileDBDoc, excessiveMode);
        }

    }
}
