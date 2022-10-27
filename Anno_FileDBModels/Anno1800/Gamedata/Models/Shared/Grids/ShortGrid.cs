using FileDBSerializing.ObjectSerializer;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids
{
    public class ShortGrid : SparseGridBaseIntIndex<blockShort, short>
    {
        public ShortGrid()
        {

        }

        public ShortGrid(int mapSize, bool SparseEnabled)
        {
            x = mapSize;
            y = mapSize;
            if (!SparseEnabled)
            {
                val = new short[mapSize * mapSize];
            }
            else
            {
                SparseBlockFactoryPrimitive<blockShort, short> blockFactory = new();
                block = new List<blockShort>() { blockFactory.MakeStart(), blockFactory.MakeEnd() };
            }
        }


        public short[]? val { get; set; }
    }

    public class blockShort : blockBasePrimitive<short>
    {

    }
}
