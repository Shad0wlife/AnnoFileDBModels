namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class AIConstructionManager
    {
        public AIConstructionManager()
        {

        }

        public AIConstructionManager(bool createDefault)
        {
            PlannedSettlements = new Empty();
        }

        public Empty? PlannedSettlements { get; set; }
    }
}
