using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class AreaManagerDataHelper
    {
        public static List<Tuple<short, AreaManagerDataItem>> MakeAreaManagerDataMapTupleList(byte[] areaManagerRaw)
        {
            return new List<Tuple<short, AreaManagerDataItem>>() { new Tuple<short, AreaManagerDataItem>(1, new AreaManagerDataItem(areaManagerRaw)) };
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

        public DataContent? GetDecompressedData(bool decompress = false)
        {
            if (decompress)
            {
                DecompressData();
            }

            return DecompressedData;
        }

        public void SetDecompresseddata(DataContent? decompressed, bool compress = false)
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
                    //Nested AreaManagerData is always Version 1
                    FileDBDocumentVersion Version = FileDBDocumentVersion.Version1;

                    FileDBSerializer<DataContent> deserializer = new FileDBSerializer<DataContent>(Version);
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
                //Nested AreaManagerData is always Version 1
                FileDBDocumentVersion Version = FileDBDocumentVersion.Version1;
                FileDBDocumentSerializer fileDBDocumentSerializer = new FileDBDocumentSerializer(new FileDBSerializerOptions() { Version = Version });
                IFileDBDocument fdbDoc = fileDBDocumentSerializer.WriteObjectStructureToFileDBDocument(DecompressedData);

                DocumentWriter streamWriter = new DocumentWriter();
                using(MemoryStream stream = new MemoryStream())
                {
                    streamWriter.WriteFileDBToStream(fdbDoc, stream);
                    Data = stream.ToArray();
                }
            }
            else
            {
                throw new ArgumentNullException("The DecompressedData was null and thus could not be compressed.");
            }

        }

    }
}
