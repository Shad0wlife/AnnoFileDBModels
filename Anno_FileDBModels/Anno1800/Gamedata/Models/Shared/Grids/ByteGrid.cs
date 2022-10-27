﻿namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids
{
    public class ByteGridShortIndex : SparseGridBaseShortIndex<blockByte, byte>
    {
        public ByteGridShortIndex()
        {

        }

        public ByteGridShortIndex(int mapSize, bool SparseEnabled)
        {
            x = (short)mapSize;
            y = (short)mapSize;
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

    public class ByteGrid : SparseGridBaseIntIndex<blockByte, byte>
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
