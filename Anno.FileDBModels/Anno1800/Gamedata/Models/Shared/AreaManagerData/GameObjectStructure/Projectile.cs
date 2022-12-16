namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class Projectile
    {
        public ProjectileDesc? ProjectileDesc { get; set; }
        public float[]? LaunchRotationWS { get; set; } //Guess it's 4 floats? Could be 2 doubles, but there've never been doubles before
        public byte? ProjectileState { get; set; }
        public long? HitTimeout { get; set; }
        public float? Velocity { get; set; }
        public long? LaunchTime { get; set; }
    }

    public class ProjectileDesc
    {
        public long? OwnerID { get; set; }
        public long? TargetID { get; set; }
        public Empty? HitType { get; set; }
        public Empty? AdditionalStatusEffects { get; set; } //Probably a List or Array
    }
}