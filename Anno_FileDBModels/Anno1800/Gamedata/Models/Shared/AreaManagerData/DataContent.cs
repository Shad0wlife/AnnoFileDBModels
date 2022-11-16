namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData
{
    public class DataContent
    {
        public DataContent()
        {

        }

        public DataContent(bool createDefault)
        {
            AreaObjectManager = new AreaObjectManager(createDefault);
        }

        public AreaObjectManager? AreaObjectManager { get; set; }
    }
}
