namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class Walking
    {
        public Reservation? Reservation { get; set; }
        public List<int>? Way { get; set; }
        public float[]? LastPos { get; set; }
        public float[]? LastWaystepPos { get; set; }
        public float[]? LastTickPos { get; set; }
        public float? LastDirView { get; set; }
        public float? LastDirLogic { get; set; }
        public float? LastDeltaDirDelta { get; set; }
        public float[]? DriftingOffset { get; set; }
        public float[]? DriftingForce { get; set; }
        public Empty? GroupMemberInformation { get; set; }
        public Empty? TargetPosition { get; set; }
        public Empty? CombatRange { get; set; }
        public bool? PreventDriftBack { get; set; }
        public int? ArrivalTimer { get; set; }
        public long? OldUnitID { get; set; }
        public float? CatchUpFactor { get; set; }
    }

    public class Reservation
    {
        public float[]? Position { get; set; }
        public float? Direction { get; set; }
        public int[]? RectSize { get; set; }
    }
}