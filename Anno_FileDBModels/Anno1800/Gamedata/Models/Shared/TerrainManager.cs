namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class TerrainManager
    {
        public TerrainManager()
        {

        }

        public TerrainManager(int mapSize)
        {
            WorldSize = new int[] { mapSize, mapSize };
            HeightMap = new HeightMapElement(mapSize);
        }

        public int[]? WorldSize { get; set; }

        public HeightMapElement? HeightMap { get; set; }
    }
}
