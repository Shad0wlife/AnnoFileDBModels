using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared;
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
        public RunOnIncludedTestdata(bool excessiveMode)
        {
            ExcessiveMode = excessiveMode;
        }

        private bool ExcessiveMode { get; }

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
                TestResultWithFileContents testResult = CompareTest<Gamedata>(testPath, ExcessiveMode);

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                if (testResult.Success)
                {
                    Console.WriteLine($"[SUCCESS] De- and Reserialized File {fileName} matches original.");
                }
                else
                {
                    Console.WriteLine($"[FAILURE] De- and Reserialized File {fileName} differs from original.");
                    string orgFilePath = Path.Combine(outPath, fileNameWithoutExt + "_org.xml");
                    File.WriteAllText(orgFilePath, testResult.OriginalContent);

                    string createdFilePath = Path.Combine(outPath, fileNameWithoutExt + "_created.xml");
                    File.WriteAllText(createdFilePath, testResult.CreatedContent);

                    if(ExcessiveMode)
                    {
                        string orgBinaryFilePath = Path.Combine(outPath, fileNameWithoutExt + "_orgBinary.xml");
                        string createdBinaryFilePath = Path.Combine(outPath, fileNameWithoutExt + "_createdBinary.xml");

                        File.WriteAllText(orgBinaryFilePath, testResult.OriginalContentWithBinaryData);
                        File.WriteAllText(createdBinaryFilePath, testResult.CreatedContentWithBinaryData);
                    }
                }
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
            }
        }

        private TestResultWithFileContents CompareTest<T>(string path, bool excessiveMode) where T : class, new()
        {
            IFileDBDocument fileDBDoc = Program.FileToFileDbDoc(path);

            return Program.CompareTest<T>(fileDBDoc, excessiveMode);
        }

    }
}
