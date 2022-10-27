using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class AnimalManager
    {
        public BitGrid? HerdGrid { get; set; }
        public List<HerdArea>? HerdAreas { get; set; }
    }

    public class HerdArea
    {
        public int[]? RandomPoint { get; set; }
        public int[]? BBox { get; set; }
        public int? CellsCount { get; set; }
        public int[][]? DangerPoints { get; set; } //int[][] = Array of int[], int[] is Reference Type, so size item is added
        public bool? ConstructionArea { get; set; }
    }
}