using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class IrrigationManager
    {
        public IrrigationManager()
        {

        }

        public IrrigationManager(int mapSize)
        {
            m_StaticTileGrid = new ByteGrid(mapSize, true);
        }

        public ByteGrid? m_StaticTileGrid { get; set; }
        public bool? IsInitialized { get; set; }

    }
}