using Anno.FileDBModels.Anno1800.Shared.Grids;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class WorldManager
    {
        public WorldManager()
        {

        }

        public WorldManager(int size)
        {
            StreetMap = new StreetMap(size);
            Water = new BitGrid(size);
            RiverGrid = new BitGrid(size);
            EnvironmentGrid = new EnvironmentGrid(size / 4);
        }

        public BitGrid? FogOfWarGrid { get; set; }
        public StreetMap? StreetMap { get; set; }
        public BitGrid? Water { get; set; }
        public BitGrid? RiverGrid { get; set; }
        public EnvironmentGrid? EnvironmentGrid { get; set; }
        public BitGrid? AirBlockerGrid { get; set; }
    }

    public class StreetMap
    {
        public StreetMap()
        {

        }

        public StreetMap(int size)
        {
            StreetID = new ByteGrid(size, false);
            VarMapData = new VarMapDataGrid(size);
        }

        public ByteGrid? StreetID { get; set; }
        public VarMapDataGrid? VarMapData { get; set; }
        public bool? m_QuayReplacementExecuted { get; set; }
        public VarMapDataGrid? V2_VarMapData { get; set; }

    }

    public class EnvironmentGrid
    {
        public EnvironmentGrid()
        {

        }

        public EnvironmentGrid(int gridSize)
        {
            EnvironmentGRid = new ByteGrid(gridSize, false);
        }

        //sic
        public ByteGrid? EnvironmentGRid { get; set; }
    }
}
