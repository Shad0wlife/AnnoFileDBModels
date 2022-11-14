using FileDBSerializing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeGamedata_ManualTest
{
    public class RunOnIncludedTestdata
    {
        public void RunOnIncludedTestData()
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

            List<string> allTestFiles = new List<string>()
            {
                newSnowflakePath,
                oldMapPath,
                nwPoolMap,
                scenario03Map,
                enbesaMap,
                arcticMap,
                communityIslandPath,
                scenario03StoryIsland01
            };

            string outPath = Program.CreateCleanLocalOutputDir();

            foreach (string testPath in allTestFiles)
            {
                string fileName = Path.GetFileName(testPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(testPath);
                (bool result, string org, string created) = CompareTest(testPath);

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                if (result)
                {
                    Console.WriteLine($"[SUCCESS] De- and Reserialized File {fileName} matches original.");
                }
                else
                {
                    Console.WriteLine($"[FAILURE] De- and Reserialized File {fileName} differs from original.");
                    File.WriteAllText(Path.Combine(outPath, fileNameWithoutExt + "_org.xml"), org);
                    File.WriteAllText(Path.Combine(outPath, fileNameWithoutExt + "_created.xml"), created);
                }
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
            }
        }

        private (bool, string, string) CompareTest(string path)
        {
            IFileDBDocument fileDBDoc = Program.FileToFileDbDoc(path);

            return Program.CompareTest(fileDBDoc);
        }

    }
}
