using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared;
using FileDBReader;
using FileDBReader.src;
using InterpreterDoc = FileDBReader.src.Interpreter;
using FileDBReader.src.XmlRepresentation;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;
using Microsoft.XmlDiffPatch;
using System.Xml;

namespace SerializeGamedata_ManualTest
{
    public class Program
    {
        public const string NestedInterpreterSubPath = @"TestData\NestedInterpreter.xml";

        public static string NestedInterpreterPath 
        { 
            get
            {
                return Path.Combine(ExecutionFolder, NestedInterpreterSubPath);
            } 
        }

        public static string ExecutionFolder
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
            }
        }

        private static InterpreterDoc _nestedInterpreter = null;
        public static InterpreterDoc NestedInterpreter
        {
            get
            {
                if(_nestedInterpreter == null)
                {
                    if (!File.Exists(NestedInterpreterPath))
                    {
                        throw new FileNotFoundException($"Could not find Interpreter document for nested data. Default path would be: \r\n{NestedInterpreterPath}");
                    }

                    using (FileStream interpreterDocStream = new FileStream(NestedInterpreterPath, FileMode.Open, FileAccess.Read))
                    {
                        _nestedInterpreter = new InterpreterDoc(InterpreterDoc.ToInterpreterDoc(interpreterDocStream));
                    }
                }
                return _nestedInterpreter;
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Interpreter Doc Path is \"{NestedInterpreterPath}\"");

            RunOnGameFiles gameFileTester = new RunOnGameFiles();
            RunOnIncludedTestdata includedDataTester = new RunOnIncludedTestdata();

            await gameFileTester.RunOnAnnoGameFiles();
            //includedDataTester.RunOnIncludedTestData();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
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

        private static (Gamedata, FileDBDocumentVersion) DeserializeTest(IFileDBDocument fileDBDocument)
        {
            FileDBDocumentVersion Version = fileDBDocument.VERSION;
            FileDBDocumentDeserializer<Gamedata> deserializer = new FileDBDocumentDeserializer<Gamedata>(new FileDBSerializerOptions() { Version = Version });
            var deserializedResult = deserializer.GetObjectStructureFromFileDBDocument(fileDBDocument);

            Console.WriteLine("Finished Deserializing.");

            if (deserializedResult is Gamedata gd)
            {
                if (gd.GameSessionManager?.AreaManagerData?.Count > 0)
                {
                    int idx = 0;
                    foreach (Tuple<short, AreaManagerDataItem> dataItemTuple in gd.GameSessionManager.AreaManagerData)
                    {
                        Console.WriteLine($"Deserializing nested AreaManagerData with id {dataItemTuple.Item1} at position {idx}.");
                        dataItemTuple.Item2.DecompressData();
                        Console.WriteLine("Finished Deserialing nested AreaManagerData Item.");
                        idx++;
                    }
                }
                return (gd, Version);
            }
            else
            {
                throw new InvalidCastException("Gamedata FileDBSerializer returned an object that is not Gamedata.");
            }
        }

        private static IFileDBDocument SerializeBack(Gamedata toSerialize, FileDBDocumentVersion toVersion)
        {
            if(toSerialize.GameSessionManager?.AreaManagerData?.Count > 0)
            {
                int idx = 0;
                foreach (Tuple<short, AreaManagerDataItem> dataItemTuple in toSerialize.GameSessionManager.AreaManagerData)
                {
                    Console.WriteLine($"Reserializing nested AreaManagerData with id {dataItemTuple.Item1} at position {idx}.");
                    dataItemTuple.Item2.CompressData();
                    Console.WriteLine("Finished Reserialing nested AreaManagerData Item.");
                    idx++;
                }
            }

            FileDBDocumentSerializer fileDBDocumentSerializer = new FileDBDocumentSerializer(new FileDBSerializerOptions() { Version = toVersion });
            IFileDBDocument fdbDoc = fileDBDocumentSerializer.WriteObjectStructureToFileDBDocument(toSerialize);
            return fdbDoc;
        }

        public static IFileDBDocument FileToFileDbDoc(string path)
        {
            Console.WriteLine($"Trying to parse {path}.");
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return StreamToFileDbDoc(stream);
            }
        }

        public static IFileDBDocument StreamToFileDbDoc(Stream stream)
        {
            var Version = VersionDetector.GetCompressionVersion(stream);

            DocumentParser loader = new DocumentParser(Version);
            IFileDBDocument fileDBDocument = loader.LoadFileDBDocument(stream);
            return fileDBDocument;
        }

        private static XmlDocument InterpretNestedFileDB(XmlDocument toInterpret)
        {
            XmlInterpreter interpreter = new XmlInterpreter();

            return interpreter.Interpret(toInterpret, NestedInterpreter);
        }

        private static string FileDBToString(IFileDBDocument doc, bool interpretNested)
        {
            XmlDocument xmlDoc;
            try
            {
                xmlDoc = new FileDbXmlConverter().ToXml(doc);

                if (interpretNested)
                    xmlDoc = InterpretNestedFileDB(xmlDoc);
            }
            catch(Exception ex)
            {
                return "";
            }

            using (MemoryStream stream = new MemoryStream())
            {
                xmlDoc.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                using(StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static (bool, string, string) CompareTest(IFileDBDocument originalDoc)
        {
            string originalDocString = FileDBToString(originalDoc, true);

            Gamedata? gamedata = null;
            FileDBDocumentVersion? fileVersion = null;
            string serializedString = "<Content>";
            try
            {
                (gamedata, fileVersion) = DeserializeTest(originalDoc);

                if (gamedata != null && fileVersion != null)
                {
                    IFileDBDocument serialized = SerializeBack(gamedata, fileVersion.Value);
                    serializedString = FileDBToString(serialized, true);
                }
            }
            catch(InvalidProgramException ex)
            {
                Console.WriteLine("Exception during De- or Reserialization. Comparing to empty.");
                serializedString += "<ErrorMsg>" + ex.Message + "</ErrorMsg>";
                serializedString += "</Content>";
            }


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