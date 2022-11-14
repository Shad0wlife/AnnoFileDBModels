namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData
{
    public class ObjectGroupFilterCollection
    {
        public ObjectGroupFilterCollection()
        {

        }

        public ObjectGroupFilterCollection(bool createDefault)
        {
            Root = new ObjectGroupFilterCollectionRoot(createDefault);
        }

        public ObjectGroupFilterCollectionRoot? Root { get; set; }
    }

    public class ObjectGroupFilterCollectionRoot
    {
        public ObjectGroupFilterCollectionRoot()
        {

        }

        public ObjectGroupFilterCollectionRoot(bool createDefault)
        {
            Filter = new List<FilterItem>() { new FilterItem(createDefault) };
        }

        public List<FilterItem>? Filter { get; set; }
    }

    public class FilterItem
    {
        public FilterItem()
        {

        }

        public FilterItem(bool createDefault)
        {
            FolderID = 1;
            Filter = new List<FilterItem>();
        }

        public int? FolderID { get; set; }
        public List<FilterItem>? Filter { get; set; }
        public int? DifficultyFlags { get; set; }
    }
}
