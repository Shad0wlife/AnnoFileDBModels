namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SelectionManager
    {
        public SelectionManager()
        {

        }

        public SelectionManager(bool createDefault)
        {
            SelectionGroupController = new SelectionGroupController(createDefault);
        }


        public SelectionGroupController? SelectionGroupController { get; set; }
    }

    public class SelectionGroupController
    {
        public SelectionGroupController()
        {

        }

        public SelectionGroupController(bool createDefault)
        {
            StoredSelections = new List<Tuple<StoredSelection, Empty>>();
        }

        public List<Tuple<StoredSelection, Empty>>? StoredSelections { get; set; }
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
