namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class ItemSessionManager
    {
        public ItemSessionManager()
        {
            ItemMap = new Empty();
            ProductItem = new Empty();
            SetBuffs = new Empty();
            SessionBuffs = new Empty();
            AreaBuffs = new Empty();
        }

        public Empty ItemMap { get; set; }
        public Empty ProductItem { get; set; }
        public Empty SetBuffs { get; set; }

        public Empty? AdditionalAreaEffects { get; set; }
        public Empty? AdditionalSessionEffects { get; set; }
        public Empty? TrackedPaths { get; set; }
        public Empty? DynamicRangeSources { get; set; }

        public Empty SessionBuffs { get; set; }
        public Empty AreaBuffs { get; set; }
    }
}
