namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class CommandQueue
    {
        public Empty? Commands { get; set; }
        public float[]? BasePosition { get; set; }
        public float? BaseRotation { get; set; }
        public Empty? SpecialAssignment { get; set; }
        public StateParams? StateParams { get; set; }

    }

    public class StateParams
    {
        public float? Radius { get; set; } //Maybe int, but probably float
        public StateParamsObject? Object { get; set; }
        public float? AttachRadius { get; set; } //Maybe int, but probably float
        public StateParamsObject? AttachObject { get; set; }
        public long? EscortedObjectMetaID { get; set; }
        public SessionTranferInfo? SessionTranferInfo { get; set; } //sic
        public Empty? StateOnQuitPassiveAttack { get; set; }
        public OptimalAttackMovementData? OptimalAttackMovementData { get; set; }

    }

    public class StateParamsObject
    {
        public long? ObjectID { get; set; }
    }

    public class OptimalAttackMovementData
    {
        public float? OptimalAngleWS { get; set; }
        public float? OptimalDistanceWS { get; set; }
    }

    /// <summary>
    /// sic.
    /// </summary>
    public class SessionTranferInfo
    {
        public long? EscortMetaID { get; set; }
    }
}
