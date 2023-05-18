namespace Anno.FileDBModels.Anno1800.IslandTemplate
{
    public class ObjectMetaInfo
    {
        public List<Tuple<ShortIdValueWrapper, List<ObjectItem>>>? SlotObjects { get; set; }
    }

    public class ShortIdValueWrapper
    {
        public ShortIdValue? value { get; set; }
    }

    public class ShortIdValue
    {
        public short? id { get; set; }
    }

    public class ObjectItem
    {
        public long? ObjectId { get; set; }
        public int? ObjectGuid { get; set; }
        public float[]? Position { get; set; }
    }
}