using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared;
using FileDBSerializing;
using FileDBSerializing.ObjectSerializer;

namespace SerializeGamedata_ManualTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string newSnowflakePath = Path.Combine(projectDirectory, @"TestData\Map\moderate_snowflake_ss_01_gamedata.data");
            string oldMapPath = Path.Combine(projectDirectory, @"TestData\Map\gamedata_1408_old.data");
            string nwPoolMap = Path.Combine(projectDirectory, @"TestData\Map\colony01_l_03_gamedata.data");
            string scenario03Map = Path.Combine(projectDirectory, @"TestData\Map\scenario_03_colony_01_gamedata.data");
            string enbesaMap = Path.Combine(projectDirectory, @"TestData\Map\colony02_01_gamedata.data");
            string arcticMap = Path.Combine(projectDirectory, @"TestData\Map\colony_03_sp_gamedata.data");


            string communityIslandPath = Path.Combine(projectDirectory, @"TestData\Island\community_island_a7m_gamedata.data");
            string scenario03StoryIsland01 = Path.Combine(projectDirectory, @"TestData\Island\scenario03_storyisland_01_gamedata.data");


            DeserializeTest(arcticMap);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        private static void DeserializeTest(string path)
        {
            Console.WriteLine($"Trying to parse {path}.");

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var Version = VersionDetector.GetCompressionVersion(stream);

                FileDBSerializer<Gamedata> deserializer = new FileDBSerializer<Gamedata>(Version);
                var deserializedResult = deserializer.Deserialize(stream);
                Console.WriteLine("Finished Deserializing.");
            }
        }
    }
}