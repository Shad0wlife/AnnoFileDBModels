namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class Mesh
    {
        public Flags? Flags { get; set; }
        public int? DyeColor { get; set; }
        public int? DyeColorDetail { get; set; }
        public SequenceData? SequenceData { get; set; }
        public byte? Damage { get; set; }
        public float? Scale { get; set; }
        public int? Conditions { get; set; }
        public float[]? Orientation { get; set; }
    }

    public class Flags
    {
        public int? flags { get; set; }
    }

    public class SequenceData
    {
        public int? CurrentSequenceID { get; set; }
        public long? CurrentSequenceStartTime { get; set; }
        public int? IdleSequenceID { get; set; }
        public bool? Dirty { get; set; }
    }
}
