using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared;
using FileDBReader;
using FileDBReader.src.XmlRepresentation;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;
using Microsoft.XmlDiffPatch;
using System.Xml;

namespace SerializeGamedata_ManualTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            RunOnGameFiles gameFileTester = new RunOnGameFiles();

            //await gameFileTester.RunOnAnnoGameFiles();
            RunOnIncludedTestData();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        private static void RunOnIncludedTestData()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string newSnowflakePath = Path.Combine(projectDirectory, @"TestData\Map\moderate_snowflake_ss_01_gamedata.data");
            string oldMapPath = Path.Combine(projectDirectory, @"TestData\Map\gamedata_1408_old.data");
            string nwPoolMap = Path.Combine(projectDirectory, @"TestData\Map\colony01_l_03_gamedata.data");
            string scenario03Map = Path.Combine(projectDirectory, @"TestData\Map\scenario_03_colony_01_gamedata.data");
            string enbesaMap = Path.Combine(projectDirectory, @"TestData\Map\colony02_01_gamedata.data");
            string arcticMap = Path.Combine(projectDirectory, @"TestData\Map\colony_03_sp_gamedata.data");


            string communityIslandPath = Path.Combine(projectDirectory, @"TestData\Island\community_island_a7m_gamedata.data");
            string scenario03StoryIsland01 = Path.Combine(projectDirectory, @"TestData\Island\scenario03_storyisland_01_gamedata.data");

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

            string outPath = CreateCleanLocalOutputDir();

            foreach (string testPath in allTestFiles)
            {
                string fileName = Path.GetFileName(testPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(testPath);
                (bool result, string org, string created) = CompareTest(testPath);

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                if (result)
                {
                    Console.WriteLine($"[SUCCESS] De- and Reserialized File {fileName} matches original.");
                }
                else
                {
                    Console.WriteLine($"[FAILURE] De- and Reserialized File {fileName} differs from original.");
                    File.WriteAllText(Path.Combine(outPath, fileNameWithoutExt+"_org.xml"), org);
                    File.WriteAllText(Path.Combine(outPath, fileNameWithoutExt + "_created.xml"), created);
                }
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
            }
        }

        public static string CreateCleanLocalOutputDir()
        {
            string selfFolderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
            string outPath = Path.Combine(selfFolderPath, "output");

            if (Directory.Exists(outPath))
            {
                Console.WriteLine("Deleting previous output.");
                Directory.Delete(outPath, true);
            }
            else
            {
                Console.WriteLine("No previous output.");
            }

            DirectoryInfo outDir = Directory.CreateDirectory(outPath);

            return outPath;
        }

        private static (Gamedata, FileDBDocumentVersion) DeserializeTest(string path)
        {
            Console.WriteLine($"Trying to parse {path}.");

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var Version = VersionDetector.GetCompressionVersion(stream);

                FileDBSerializer<Gamedata> deserializer = new FileDBSerializer<Gamedata>(Version);
                var deserializedResult = deserializer.Deserialize(stream);
                Console.WriteLine("Finished Deserializing.");

                if(deserializedResult is Gamedata gd)
                {
                    return (gd, Version);
                }
                else
                {
                    throw new InvalidCastException("Gamedata FileDBSerializer returned an object that is not Gamedata.");
                }
            }
        }

        private static IFileDBDocument SerializeBack(Gamedata toSerialize, FileDBDocumentVersion toVersion)
        {
            FileDBDocumentSerializer fileDBDocumentSerializer = new FileDBDocumentSerializer(new FileDBSerializerOptions() { Version = toVersion });
            IFileDBDocument fdbDoc = fileDBDocumentSerializer.WriteObjectStructureToFileDBDocument(toSerialize);
            return fdbDoc;
        }

        private static IFileDBDocument FileToFileDbDoc(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var Version = VersionDetector.GetCompressionVersion(stream);

                DocumentParser loader = new DocumentParser(Version);
                IFileDBDocument fileDBDocument = loader.LoadFileDBDocument(stream);
                return fileDBDocument;
            }
        }

        private static string FileDBToString(IFileDBDocument doc)
        {
            XmlDocument xmlDoc = new FileDbXmlConverter().ToXml(doc);
            using(MemoryStream stream = new MemoryStream())
            {
                xmlDoc.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                using(StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static (bool, string, string) CompareTest(string path)
        {
            IFileDBDocument originalDoc = FileToFileDbDoc(path);
            string originalDocString = FileDBToString(originalDoc);

            (Gamedata gamedata, FileDBDocumentVersion fileVersion) = DeserializeTest(path);
            IFileDBDocument serialized = SerializeBack(gamedata, fileVersion);
            string serializedString = FileDBToString(serialized);

            using (TextReader orgReader = new StringReader(originalDocString))
            using (TextReader serializedReader = new StringReader(serializedString))
            {
                XmlReader xmlReaderOrg = XmlReader.Create(orgReader);
                XmlReader xmlReaderSerialized = XmlReader.Create(serializedReader);

                XmlDiff xmlDiff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreComments | XmlDiffOptions.IgnoreWhitespace);
                bool compareResult = xmlDiff.Compare(xmlReaderOrg, xmlReaderSerialized);

                return (compareResult, originalDocString, serializedString);
            }
        }
    }
}