namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
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

        public int? FileVersion { get; set; }
        public GameSessionManager? GameSessionManager { get; set; }
    }
}