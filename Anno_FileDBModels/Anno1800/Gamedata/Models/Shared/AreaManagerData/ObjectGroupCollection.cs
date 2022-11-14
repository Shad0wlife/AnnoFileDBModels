using FileDBSerializing.EncodingAwareStrings;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData
{
    public class ObjectGroupCollection
    {
        public ObjectGroupCollection()
        {

        }

        public ObjectGroupCollection(bool createDefault)
        {
            ObjectGroups = new List<Tuple<UTF8String, ObjectGroup>>();
        }

        public List<Tuple<UTF8String, ObjectGroup>>? ObjectGroups { get; set; }
    }

    public class ObjectGroup
    {
        public List<long>? GameObjects { get; set; }
    }
}
