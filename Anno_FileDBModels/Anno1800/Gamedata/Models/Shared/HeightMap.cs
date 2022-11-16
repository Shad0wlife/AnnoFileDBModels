using System.Linq;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class HeightMapElement
    {
        public HeightMapElement()
        {

        }

        public HeightMapElement(int mapSize)
        {
            int heightMapSize = 2 * mapSize + 1;
            Width = heightMapSize;
            Height = heightMapSize;
            HeightMap = Enumerable.Repeat((short)0, heightMapSize * heightMapSize).ToArray();
        }

        public int? Width { get; set; }
        public int? Height { get; set; }
        public short[]? HeightMap { get; set; }
    }
}
