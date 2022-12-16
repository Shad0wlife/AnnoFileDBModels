namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SeasonManager
    {
        public long? SeasonStartTime { get; set; }
        public long? SeasonEndTime { get; set; }

        public Season? CurrentSeason { get; set; }
        public Season? NextSeason { get; set; }

        public Empty? UsedSeasons { get; set; } //Probably some List<>, but of what?
        public Empty? OverwriteSeasons { get; set; } //Probably some List<>, but of what?
    }

    public class Season
    {
        public long? AddedTime { get; set; }
    }
}
