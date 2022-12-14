using FileDBSerializer.ObjectSerializer;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.Grids
{
    public abstract class SparseBlockFactory<BLOCK, ITEM> where BLOCK : blockBase<ITEM>, new()
    {
        public BLOCK MakeEnd()
        {
            return new BLOCK() { mode = 0 };
        }

        protected const short DEFAULT_BLOCK_SIZE = 16;

        protected BLOCK MakeStartInternal(short tileSize)
        {
            return new BLOCK()
            {
                mode = 1,
                x = tileSize,
                y = tileSize
            };
        }

        public abstract BLOCK MakeStart();

        public abstract BLOCK MakeStart(short tileSize);


        protected BLOCK MakeDefaultInternal(short xCoord, short yCoord)
        {
            return new BLOCK()
            {
                mode = 2,
                x = xCoord != 0 ? xCoord : null,
                y = yCoord != 0 ? yCoord : null
            };
        }

        public abstract BLOCK MakeDefault(short xCoord, short yCoord, ITEM defaultVal);
    }

    public class SparseBlockFactoryReference<BLOCK, ITEM> : SparseBlockFactory<BLOCK, ITEM> where BLOCK : blockBaseReference<ITEM>, new() where ITEM : new()
    {
        public override BLOCK MakeStart()
        {
            BLOCK block = MakeStartInternal(DEFAULT_BLOCK_SIZE);
            block.@default = new List<ITEM>() { new ITEM() };
            return block;
        }

        public override BLOCK MakeStart(short tileSize)
        {
            BLOCK block = MakeStartInternal(tileSize);
            block.@default = new List<ITEM>() { new ITEM() };
            return block;
        }

        public override BLOCK MakeDefault(short xCoord, short yCoord, ITEM defaultVal)
        {
            BLOCK block = MakeDefaultInternal(xCoord, yCoord);
            block.@default = new List<ITEM>() { defaultVal };
            return block;
        }
    }

    public class SparseBlockFactoryPrimitive<BLOCK, ITEM> : SparseBlockFactory<BLOCK, ITEM> where BLOCK : blockBasePrimitive<ITEM>, new() where ITEM : struct
    {
        public override BLOCK MakeStart()
        {
            BLOCK block = MakeStartInternal(DEFAULT_BLOCK_SIZE);
            block.@default = new ITEM[1];
            return block;
        }

        public override BLOCK MakeStart(short tileSize)
        {
            BLOCK block = MakeStartInternal(tileSize);
            block.@default = new ITEM[1];
            return block;
        }

        public override BLOCK MakeDefault(short xCoord, short yCoord, ITEM defaultVal)
        {
            BLOCK block = MakeDefaultInternal(xCoord, yCoord);
            block.@default = new ITEM[1] { defaultVal };
            return block;
        }
    }

    public abstract class blockBase<T>
    {
        public byte? mode { get; set; }

        public short? x { get; set; }
        public short? y { get; set; }
    }

    [PropertyLocation(PropertyLocationOption.AFTER_PARENT)]
    public abstract class blockBaseReference<T> : blockBase<T> where T : new()
    {
        public List<T>? @default { get; set; }
        public List<T>? values { get; set; }
    }

    [PropertyLocation(PropertyLocationOption.AFTER_PARENT)]
    public abstract class blockBasePrimitive<T> : blockBase<T> where T : struct
    {
        public T[]? @default { get; set; }
        public T[]? values { get; set; }
    }
}
