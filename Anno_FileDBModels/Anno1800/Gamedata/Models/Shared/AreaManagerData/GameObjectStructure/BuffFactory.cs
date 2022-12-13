namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class BuffFactory
    {
        public AppliedEffect? AppliedEffect { get; set; }
        public AppliedEffect? AppliedBaseEffect { get; set; }
        public ProductionState? ProductionState { get; set; }
    }

    public class AppliedEffect
    {
        public AppliedEffectShortIDChild? scope { get; set; }
        public int? distance { get; set; }
        public short? targetArea { get; set; }
        public AppliedEffectShortIDChild? targetParticipant { get; set; }
        public int? effect { get; set; }
        public AppliedEffectSource? source { get; set; }
        public Empty? distanceSource { get; set; }

    }

    public class AppliedEffectSource
    {
        public long? ObjectID { get; set; }
    }

    public class AppliedEffectShortIDChild
    {
        public short? id { get; set; }
    }
}
