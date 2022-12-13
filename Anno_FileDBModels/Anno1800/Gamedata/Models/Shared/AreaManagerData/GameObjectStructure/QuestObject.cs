namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class QuestObject
    {
        public Empty? QuestIDs { get; set; } //Probably a List<int> or so, could be any child type, it's empty for now
        public List<bool>? ObjectWasVisible { get; set; }
        public Empty? OverwriteVisualParticipant { get; set; }
    }
}
