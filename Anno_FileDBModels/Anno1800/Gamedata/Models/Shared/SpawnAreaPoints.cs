namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public static class SpawnAreaPointsHelper
    {

        public static byte[] TupleIndexOrder
        {
            get
            {
                return new byte[] { 12, 10, 9, 14, 8, 7, 15, 6, 13, 4, 3, 11, 2, 1, 5, 0 };
            }
        }

        public static List<Tuple<byte, SpawnAreaPoint>> MakeEmptySpawnAreaPoints(int mapSize)
        {
            byte[] indices = TupleIndexOrder;
            List<Tuple<byte, SpawnAreaPoint>> result = new List<Tuple<byte, SpawnAreaPoint>>(indices.Length);
            for (int cnt = 0; cnt < indices.Length; cnt++)
            {
                result.Add(new Tuple<byte, SpawnAreaPoint>(indices[cnt], new SpawnAreaPoint(mapSize)));
            }
            return result;
        }
    }

    public class SpawnAreaPoint
    {
        public SpawnAreaPoint()
        {
            m_AreaPointGrid = new List<Tuple<int, int[]>>();
            m_AreaRect = new List<Tuple<int, int[]>>();
        }

        public SpawnAreaPoint(int mapSize)
        {
            m_XSize = mapSize;
            m_YSize = mapSize;
            m_AreaPointGrid = new List<Tuple<int, int[]>>();
            m_AreaRect = new List<Tuple<int, int[]>>();
        }

        public int m_XSize { get; set; }
        public int m_YSize { get; set; }
        public List<Tuple<int, int[]>> m_AreaPointGrid { get; set; }
        public List<Tuple<int, int[]>> m_AreaRect { get; set; }
    }
}
