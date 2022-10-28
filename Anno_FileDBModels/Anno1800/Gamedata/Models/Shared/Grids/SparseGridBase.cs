using FileDBSerializing.ObjectSerializer;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids
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
