namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class Monument
    {
        public ProductionState? ProductionState { get; set; }
        public int? CurrentProductivity { get; set; }
        public int[]? EventRewards { get; set; } //int[] is a guess here, but I expect GUIDs
        public Empty? EventRewards2 { get; set; } //probably some list type, but always empty in ggj large island 01
        public Empty? EventPreparation { get; set; }
        public long? lastReducedAttractivenessTimestamp { get; set; }
        public int? feedbackTextAmount { get; set; }
        public int[]? FeedbackTextIcon { get; set; } //int[] is a guess here, it's just zeroes
        public int[]? FeedbackTextGUID { get; set; } //int[] is a guess here, it's just zeroes
        public float[]? FeedbackTextPosition { get; set; } //float[] is a guess here, it's just zeroes, but positions are usually floats
        public Empty? NextIdleVariationTime { get; set; }
    }
}
