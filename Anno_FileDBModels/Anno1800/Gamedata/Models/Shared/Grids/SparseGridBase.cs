using FileDBSerializing.ObjectSerializer;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids
{
    public abstract class SparseGridBase<T, DATA> where T : blockBase<DATA>
    {
        public bool? SparseEnabled { get; set; }

        [FlatArray]
        public List<T>? block { get; set; }
    }

    public abstract class SparseGridBaseIntIndex<T, DATA> : SparseGridBase<T, DATA> where T : blockBase<DATA>
    {
        public int? x { get; set; }
        public int? y { get; set; }
    }
    public abstract class SparseGridBaseShortIndex<T, DATA> : SparseGridBase<T, DATA> where T : blockBase<DATA>
    {
        public short? x { get; set; }
        public short? y { get; set; }
    }
}
