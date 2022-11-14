using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData
{
    public class GameObjectCollection
    {
        public GameObjectCollection()
        {

        }

        public GameObjectCollection(bool createDefault)
        {
            objects = new List<GameObject>();
        }

        public List<GameObject>? objects { get; set; }
    }
}
