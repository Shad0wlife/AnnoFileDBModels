namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class Gamedata
    {
        public Gamedata()
        {

        }

        public Gamedata(int mapSize, int playableSize, string ambientName, byte[] areaManagerData)
        {
            FileVersion = 8;
            GameSessionManager = new GameSessionManager(mapSize, playableSize, ambientName, areaManagerData);
        }

        public Gamedata(int mapSize, int playableSize, string ambientName, bool createAreaManagerData)
        {
            FileVersion = 8;
            GameSessionManager = new GameSessionManager(mapSize, playableSize, ambientName, createAreaManagerData);
        }

        public int? FileVersion { get; set; }
        public GameSessionManager? GameSessionManager { get; set; }
    }
}
