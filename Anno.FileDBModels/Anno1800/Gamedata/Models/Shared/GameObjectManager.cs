using FileDBSerializing.EncodingAwareStrings;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class GameObjectManager
    {
        public GameObjectManager()
        {

        }

        public GameObjectManager(bool createDefault)
        {
            GameObjectLabelMap = new List<Tuple<UTF8String, long>>();
        }

        public List<Tuple<UTF8String, long>>? GameObjectLabelMap { get; set; }

        public DynamicObjectGroupCollection? DynamicObjectGroupCollection { get; set; }
    }

    public class DynamicObjectGroupCollection
    {
        public Empty? ObjectGroups { get; set; }
    }
}
