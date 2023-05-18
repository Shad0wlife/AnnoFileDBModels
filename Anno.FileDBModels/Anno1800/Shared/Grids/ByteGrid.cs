using FileDBSerializer.ObjectSerializer;

namespace Anno.FileDBModels.Anno1800.Shared.Grids
{
    [PropertyLocation(PropertyLocationOption.AFTER_PARENT)]
    public class ByteGrid : SparseGridBase<blockByte, byte>
    {
        public ByteGrid()
        {

        }

        public ByteGrid(int mapSize, bool SparseEnabled)
        {
            x = mapSize;
            y = mapSize;
            if (!SparseEnabled)
            {
                val = new byte[mapSize * mapSize];
            }
            else
            {
                SparseBlockFactoryPrimitive<blockByte, byte> blockFactory = new();
                block = new List<blockByte>() { blockFactory.MakeStart(), blockFactory.MakeEnd() };
            }
        }


        public byte[]? val { get; set; }
    }

    public class blockByte : blockBasePrimitive<byte>
    {

    }
}
