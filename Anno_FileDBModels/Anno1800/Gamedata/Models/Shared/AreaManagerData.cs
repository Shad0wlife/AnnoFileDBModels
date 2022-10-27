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
    }
}
