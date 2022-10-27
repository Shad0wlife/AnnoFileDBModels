namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SelectionManager
    {
        public SelectionManager()
        {
            SelectionGroupController = new SelectionGroupController();
        }

        public SelectionGroupController SelectionGroupController { get; set; }
    }

    public class SelectionGroupController
    {
        public SelectionGroupController()
        {
            StoredSelections = new List<StoredSelection>();
        }


        public List<StoredSelection> StoredSelections { get; set; }
    }

    public class StoredSelection
    {
        public StoredSelection_value? value { get; set; }
    }

    public class StoredSelection_value
    {
        public short? id { get; set; }
    }
}
