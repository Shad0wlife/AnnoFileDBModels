using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;
using System.IO;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class AreaManagerDataHelper
    {
        public static List<Tuple<short, AreaManagerDataItem>> MakeAreaManagerDataMapTupleList(byte[] areaManagerRaw)
        {
            return new List<Tuple<short, AreaManagerDataItem>>() { new Tuple<short, AreaManagerDataItem>(1, new AreaManagerDataItem(areaManagerRaw)) };
        }

        public static List<Tuple<short, AreaManagerDataItem>> MakeDefaultAreaManagerDataMapTupleList()
        {
            AreaManagerDataItem defaultItem = new AreaManagerDataItem();
            defaultItem.CreateDefaultAreaManagerData();
            return new List<Tuple<short, AreaManagerDataItem>>() { new Tuple<short, AreaManagerDataItem>(1, defaultItem) };
        }
    }

    public class AreaManagerDataItem
    {
        public AreaManagerDataItem()
        {

        }

        public AreaManagerDataItem(byte[] data)
        {
            ByteCount = data.Length;
            Data = data;
        }

        //Check Bytecount: Vanilla Pool Map == 351
        public int? ByteCount { get; set; }
        public byte[]? Data { get; set; }

        private DataContent? DecompressedData;
        private FileDBDocumentVersion UsedFileDBCompression = FileDBDocumentVersion.Version1;

        public DataContent? GetDecompressedData(bool decompress = false)
        {
            if (decompress)
            {
                DecompressData();
            }

            return DecompressedData;
        }

        public void SetDecompressedData(DataContent? decompressed, bool compress = false)
        {
            DecompressedData = decompressed;

            if (compress)
            {
                CompressData();
            }
        }

        public void DecompressData()
        {
            if (Data is not null)
            {
                using (MemoryStream stream = new MemoryStream(Data))
                {

                    var actualVersion = VersionDetector.GetCompressionVersion(stream);

                    UsedFileDBCompression = actualVersion;

                    stream.Seek(0, SeekOrigin.Begin);

                    FileDBSerializer<DataContent> deserializer = new FileDBSerializer<DataContent>(UsedFileDBCompression);
                    var deserializedResult = deserializer.Deserialize(stream);
                    Console.WriteLine("Finished Deserializing.");

                    if (deserializedResult is DataContent dc)
                    {
                        DecompressedData = dc;
                    }
                    else
                    {
                        throw new InvalidCastException("DataContent FileDBSerializer returned an object that is not DataContent.");
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("The DecompressedData was null, and there was no Data to decompress.");
            }
        }

        public void CompressData()
        {
            if (DecompressedData is not null)
            {
                FileDBDocumentSerializer fileDBDocumentSerializer = new FileDBDocumentSerializer(new FileDBSerializerOptions() { Version = UsedFileDBCompression });
                IFileDBDocument fdbDoc = fileDBDocumentSerializer.WriteObjectStructureToFileDBDocument(DecompressedData);

                DocumentWriter streamWriter = new DocumentWriter();
                using(MemoryStream stream = new MemoryStream())
                {
                    streamWriter.WriteFileDBToStream(fdbDoc, stream);
                    Data = stream.ToArray();
                    ByteCount = Data.Length;
                }
            }
            else
            {
                throw new ArgumentNullException("The DecompressedData was null and thus could not be compressed.");
            }

        }

        public void CreateDefaultAreaManagerData()
        {
            DataContent dataContent = new DataContent(true);
            SetDecompressedData(dataContent, true);
        }

    }
}
