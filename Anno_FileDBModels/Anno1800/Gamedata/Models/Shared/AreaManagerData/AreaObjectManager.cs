namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData
{
    public class AreaObjectManager
    {
        public AreaObjectManager()
        {

        }

        public AreaObjectManager(bool createDefault)
        {
            GameObjectIDCounter = 0;
            NonGameObjectIDCounter = 0;
            QueuedChangeGUID = new Empty();
            QueuedDeletes = new byte[0];
            ObjectGroupFilterCollection = new ObjectGroupFilterCollection(createDefault);
            ObjectGroupCollection = new ObjectGroupCollection(createDefault);
            GameObject = new GameObjectCollection(createDefault);
            NaturePreset = new GameObjectCollection(createDefault);
            EditorObject = new GameObjectCollection(createDefault);
        }

        public long? GameObjectIDCounter { get; set; }
        public long? NonGameObjectIDCounter { get; set; }
        public Empty? QueuedChangeGUID { get; set; }
        public byte[]? QueuedDeletes { get; set; } //byte[] is for safety, it's an empty Attrib and byte[] matches everything. Probably a long[]?
        public ObjectGroupFilterCollection? ObjectGroupFilterCollection { get; set; }
        public ObjectGroupCollection? ObjectGroupCollection { get; set; }
        public GameObjectCollection? GameObject { get; set; }
        public GameObjectCollection? NaturePreset { get; set; }
        public GameObjectCollection? EditorObject { get; set; }
        public GameObjectCollection? Prop { get; set; }

    }
}
