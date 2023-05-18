using Anno.FileDBModels.Anno1800.Gamedata.Models.Shared;
using FileDBReader;
using FileDBReader.src;
using InterpreterDoc = FileDBReader.src.Interpreter;
using FileDBReader.src.XmlRepresentation;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;
using Microsoft.XmlDiffPatch;
using System.Xml;

namespace SerializeGamedata.ManualTest
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

            RunOnGameFiles gameFileTester = new RunOnGameFiles(false);
            RunOnIncludedTestdata includedDataTester = new RunOnIncludedTestdata(false);

            await gameFileTester.RunOnAnnoGameFiles();
            //includedDataTester.RunOnIncludedGamedata();
            //includedDataTester.RunOnIncludedTemplates();

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

        public static List<T> ShuffleToList<T>(IEnumerable<T> input)
        {
            Random rng = new Random();
            List<T> list = new List<T>(input);

            int n = list.Count;
            while(n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        private static (T?, FileDBDocumentVersion) DeserializeTest<T>(IFileDBDocument fileDBDocument) where T : class, new()
        {
            FileDBDocumentVersion Version = fileDBDocument.VERSION;
            FileDBDocumentDeserializer<T> deserializer = new FileDBDocumentDeserializer<T>(new FileDBSerializerOptions() { Version = Version });
            var deserializedResult = deserializer.GetObjectStructureFromFileDBDocument(fileDBDocument);

            Console.WriteLine("Finished Deserializing.");

            if (typeof(T) == typeof(Gamedata) && deserializedResult is Gamedata gd)
            {
                if (gd.GameSessionManager?.AreaManagerData?.Count > 0)
                {
                    int idx = 0;
                    foreach (Tuple<short, AreaManagerDataItem> dataItemTuple in gd.GameSessionManager.AreaManagerData)
                    {
                        Console.WriteLine($"Deserializing nested AreaManagerData with id {dataItemTuple.Item1} at position {idx}.");
                        if(dataItemTuple.Item2.ByteCount != dataItemTuple.Item2.Data?.Length)
                        {
                            Console.WriteLine($"[WARNING] AreaManagerDataItem has ByteCount {dataItemTuple.Item2.ByteCount} but array length {dataItemTuple.Item2.Data?.Length}.");
                        }

                        dataItemTuple.Item2.DecompressData();
                        Console.WriteLine("Finished Deserialing nested AreaManagerData Item.");
                        idx++;
                    }
                }
                return (gd as T, Version);
            }
            else
            {
                return (deserializedResult, Version);
            }
        }

        private static IFileDBDocument SerializeBack<T>(T toSerialize, FileDBDocumentVersion toVersion) where T : class, new()
        {
            if (typeof(T) == typeof(Gamedata) && toSerialize is Gamedata gd)
                if (gd.GameSessionManager?.AreaManagerData?.Count > 0)
                {
                    int idx = 0;
                    foreach (Tuple<short, AreaManagerDataItem> dataItemTuple in gd.GameSessionManager.AreaManagerData)
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

        public static TestResultWithFileContents CompareTest<T>(IFileDBDocument originalDoc, bool excessiveMode) where T : class, new()
        {
            string originalDocStringNested = FileDBToString(originalDoc, true);

            string originalDocStringBinaryData;
            if (excessiveMode)
                originalDocStringBinaryData = FileDBToString(originalDoc, false);
            else
                originalDocStringBinaryData = "";

            T? target = null;
            FileDBDocumentVersion? fileVersion = null;
            string serializedString = "<Content>";
            string serializedStringBinaryData = "";
            try
            {
                (target, fileVersion) = DeserializeTest<T>(originalDoc);

                if (target != null && fileVersion != null)
                {
                    if(target is Anno.FileDBModels.Anno1800.IslandTemplate.IslandTemplateDocument itd)
                    {
                        Anno.FileDBModels.Anno1800.IslandTemplate.IslandTemplateDocument copy = new Anno.FileDBModels.Anno1800.IslandTemplate.IslandTemplateDocument(itd.MapSize[0]);
                        Console.WriteLine($"[IslandTemplate] ActiveMapGrid has {itd.ActiveMapGrid.bits.Length*8} bits, " +
                            $"the IslandSize is {itd.MapSize[0]}x{itd.MapSize[1]}, " +
                            $"and an ActiveMapGrid size of {itd.ActiveMapGrid.x}x{itd.ActiveMapGrid.y}={itd.ActiveMapGrid.x * itd.ActiveMapGrid.y} " +
                            $"with an array of size {itd.ActiveMapGrid.bits.Length * 8}: " +
                            $"--> bits/size = {((double)itd.ActiveMapGrid.bits.Length * 8)/(itd.ActiveMapGrid.x * itd.ActiveMapGrid.y)}" +
                            $"\r\n\t\tThe copy now has a map size of {copy.MapSize[0]}x{copy.MapSize[1]}, " +
                            $"and an ActiveMapGrid size of {copy.ActiveMapGrid.x}x{copy.ActiveMapGrid.y}={copy.ActiveMapGrid.x * copy.ActiveMapGrid.y} " +
                            $"with an array of size {copy.ActiveMapGrid.bits.Length * 8}: " +
                            $"--> bits/size = {((double)copy.ActiveMapGrid.bits.Length * 8) / (copy.ActiveMapGrid.x * copy.ActiveMapGrid.y)}");
                    }

                    IFileDBDocument serialized = SerializeBack<T>(target, fileVersion.Value);
                    serializedString = FileDBToString(serialized, true);

                    if (excessiveMode)
                        serializedStringBinaryData = FileDBToString(serialized, false);


                }
            }
            catch(InvalidProgramException ex)
            {
                Console.WriteLine("Exception during De- or Reserialization. Comparing to empty.");
                serializedString += "<ErrorMsg>" + ex.Message + "</ErrorMsg>";
                serializedString += "</Content>";
                serializedStringBinaryData = serializedString;
            }


            using (TextReader orgReader = new StringReader(originalDocStringNested))
            using (TextReader serializedReader = new StringReader(serializedString))
            {
                XmlReader xmlReaderOrg = XmlReader.Create(orgReader);
                XmlReader xmlReaderSerialized = XmlReader.Create(serializedReader);

                XmlDiff xmlDiff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder | XmlDiffOptions.IgnoreComments | XmlDiffOptions.IgnoreWhitespace);
                bool compareResult = false;
                try
                {
                    compareResult = xmlDiff.Compare(xmlReaderOrg, xmlReaderSerialized);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Exception during XML Compare: {ex.Message}");
                }
                

                return new TestResultWithFileContents(compareResult, originalDocStringNested, serializedString, originalDocStringBinaryData, serializedStringBinaryData);
            }
        }
    }
}
