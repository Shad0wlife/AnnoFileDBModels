using FileDBSerializing.ObjectSerializer;

namespace Anno.FileDBModels.Anno1800.Shared.Grids
{
    public abstract class SparseGridBase<T, DATA> where T : blockBase<DATA>
    {
        public bool? SparseEnabled { get; set; }
        public int? x { get; set; }
        public int? y { get; set; }

        [FlatArray]
        public List<T>? block { get; set; }
    }
}
