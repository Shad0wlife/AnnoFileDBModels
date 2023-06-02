namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class Gamedata
    {
        public Gamedata()
        {

        }

        public Gamedata(int mapSize, (int x, int y, int size) playableArea, string ambientName, byte[] areaManagerData)
        {
            FileVersion = 8;
            GameSessionManager = new GameSessionManager(mapSize, playableArea, ambientName, areaManagerData);
        }

        public Gamedata(int mapSize, (int x, int y, int size) playableArea, string ambientName, bool createAreaManagerData)
        {
            FileVersion = 8;
            GameSessionManager = new GameSessionManager(mapSize, playableArea, ambientName, createAreaManagerData);
        }

        public int? FileVersion { get; set; }
        public GameSessionManager? GameSessionManager { get; set; }
    }
}
