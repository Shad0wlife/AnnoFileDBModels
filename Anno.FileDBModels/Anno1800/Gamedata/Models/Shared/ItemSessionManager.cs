namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class ItemSessionManager
    {
        public ItemSessionManager()
        {

        }

        public ItemSessionManager(bool createDefault)
        {
            ItemMap = new Empty();
            ProductItem = new Empty();
            SetBuffs = new Empty();
            SessionBuffs = new List<Tuple<SessionBuff, Empty>>();
            AreaBuffs = new List<Tuple<short, Empty>>();
        }

        public Empty? ItemMap { get; set; }
        public Empty? ProductItem { get; set; }
        public Empty? SetBuffs { get; set; }

        public Empty? AdditionalAreaEffects { get; set; }
        public Empty? AdditionalSessionEffects { get; set; }
        public Empty? TrackedPaths { get; set; }
        public Empty? DynamicRangeSources { get; set; }

        public List<Tuple<SessionBuff, Empty>>? SessionBuffs { get; set; }
        public List<Tuple<short, Empty>>? AreaBuffs { get; set; }
    }

    public class SessionBuff
    {
        public SessionBuffValue? value { get; set; }
    }

    public class SessionBuffValue
    {
        public short? id { get; set; }
    }
}
