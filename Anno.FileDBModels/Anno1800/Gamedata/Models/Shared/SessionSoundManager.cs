namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SessionSoundManager
    {
        public SessionSoundManager()
        {

        }

        public SessionSoundManager(bool createDefault)
        {
            AmbientMoodSoundHandler = new AmbientMoodSoundHandler();
            EnvironmentHandler = new Empty();
        }

        public AmbientMoodSoundHandler? AmbientMoodSoundHandler { get; set; }
        public Empty? EnvironmentHandler { get; set; }
    }

    public class AmbientMoodSoundHandler
    {
        public AmbientMoodSoundHandler()
        {

        }

        public AmbientMoodSoundHandler(bool create)
        {
            AmbientMoodGrid = new AmbientMoodGrid(true);
        }

        public AmbientMoodGrid? AmbientMoodGrid { get; set; }
    }

    public class AmbientMoodGrid
    {
        public AmbientMoodGrid()
        {

        }

        public AmbientMoodGrid(bool create)
        {
            DatasetCountGrid = new DatasetCountGrid();
        }

        public DatasetCountGrid? DatasetCountGrid { get; set; }
    }

    public class DatasetCountGrid
    {

        public DatasetCountGrid()
        {

        }

        public DatasetCountGrid(int size) : this(size, size)
        {

        }

        public DatasetCountGrid(int sizeX, int sizeY)
        {
            x = sizeX;
            y = sizeY;
            val = new List<DatasetCountGrid_Item>(sizeX * sizeY);
        }

        public int? x { get; set; }
        public int? y { get; set; }

        public List<DatasetCountGrid_Item>? val { get; set; }
    }

    public class DatasetCountGrid_Item
    {
        public Tuple<byte, byte>[]? counts { get; set; } //counts Tag with size and nested Tuples
    }
}
