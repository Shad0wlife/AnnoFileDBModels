namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class WorkforceTransferManager
    {
        public WorkforceTransferManager()
        {

        }

        public WorkforceTransferManager(bool createDefault)
        {
            ParticipantData = new Empty();
        }

        public Empty? ParticipantData { get; set; }
    }
}
