using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class RegrowManager
    {
        public RegrowManager()
        {

        }

        public RegrowManager(int islandSize)
        {
            TreeMap = new BitGrid(islandSize);
        }

        public BitGrid? TreeMap { get; set; }
    }
}