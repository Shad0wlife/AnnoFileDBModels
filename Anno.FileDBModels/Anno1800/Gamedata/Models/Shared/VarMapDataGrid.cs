using Anno.FileDBModels.Anno1800.Shared.Grids;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class VarMapDataGrid : SparseGridBase<blockVarMapData, dataContainer>
    {
        public VarMapDataGrid()
        {

        }

        public VarMapDataGrid(int size)
        {
            SparseBlockFactoryReference<blockVarMapData, dataContainer> blockFactory = new();

            SparseEnabled = true;
            x = size;
            y = size;
            block = new List<blockVarMapData> { blockFactory.MakeStart(), blockFactory.MakeEnd() };
        }

        public VarMapDataGrid(int size, short blockSize)
        {
            SparseBlockFactoryReference<blockVarMapData, dataContainer> blockFactory = new();

            SparseEnabled = true;
            x = size;
            y = size;
            block = new List<blockVarMapData> { blockFactory.MakeStart(blockSize), blockFactory.MakeEnd() };
        }

    }

    public class blockVarMapData : blockBaseReference<dataContainer>
    {

    }

    public class dataContainer
    {
        public dataContainer()
        {

        }

        public short[]? data { get; set; }
    }
}
