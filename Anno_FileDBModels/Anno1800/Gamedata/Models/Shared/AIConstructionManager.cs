namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class AIConstructionManager
    {
        public AIConstructionManager()
        {
            PlannedSettlements = new Empty();
        }

        public Empty PlannedSettlements { get; set; }
    }
}
