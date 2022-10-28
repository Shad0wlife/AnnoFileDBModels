using Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class GameSessionManager
    {
        public GameSessionManager()
        {

        }

        /// <summary>
        /// GameSessionManager for a Pool Map
        /// </summary>
        /// <param name="mapSize"></param>
        /// <param name="playableSize"></param>
        /// <param name="ambientName"></param>
        /// <param name="areaManagerData"></param>
        public GameSessionManager(int mapSize, int playableSize, string ambientName, byte[] areaManagerData)
        {
            SessionSettings = new SessionSettings(ambientName, mapSize, playableSize);
            SessionRandomManager = new Empty();
            TerrainManager = new TerrainManager(mapSize);
            SessionCameraManager = new SessionCameraManager(true);
            MessageManager = new Empty();
            GameObjectManager = new GameObjectManager(true);
            SessionParticipantManager = new Empty();
            IslandManager = new Empty();
            MinimapManager = new Empty();
            SessionCoastManager = new Empty();
            WorldManager = new WorldManager(mapSize);
            PathManager = new Empty();
            SessionEconomyManager = new Empty();
            DiscoveryManager = new Empty();
            RegrowManager = new RegrowManager();
            SessionSoundManager = new SessionSoundManager(true);
            ActiveTradeManager = new Empty();
            StreetOverlayManager = new Empty();
            SelectionManager = new SelectionManager(true);
            CSelectionCursorManager = new Empty();
            IncidentManager = new Empty();
            CameraSequenceManager = new CameraSequenceManager();
            AIUnitManager = new Empty();
            AIConstructionManager = new AIConstructionManager(true);
            AnimalManager = new AnimalManager();
            CSlotManager = new CSlotManager();
            VisitorManager = new Empty();
            ItemSessionManager = new ItemSessionManager(true);
            MilitaryManager = new Empty();
            BlueprintManager = new Empty();
            LoadingPierManager = new Empty();
            SessionFreeAreaProductivityManager = new Empty();
            WorkforceTransferManager = new WorkforceTransferManager(true);
            AreaManager = new Empty();
            AreaLinks = new Empty();
            AreaIDs = new ShortGrid(mapSize, false);
            SpawnAreaPoints = SpawnAreaPointsHelper.MakeEmptySpawnAreaPoints(mapSize);
            AreaManagerData = AreaManagerDataHelper.MakeAreaManagerDataMapTupleList(areaManagerData);
        }

        //TODO GameSessionManager for Pool Island

        public SessionSettings? SessionSettings { get; set; }
        public Empty? SessionRandomManager { get; set; }
        public TerrainManager? TerrainManager { get; set; }
        public SessionCameraManager? SessionCameraManager { get; set; }
        public Empty? MessageManager { get; set; }
        public GameObjectManager? GameObjectManager { get; set; }
        public Empty? SessionParticipantManager { get; set; }
        public Empty? IslandManager { get; set; }
        public Empty? MinimapManager { get; set; }
        public Empty? SessionCoastManager { get; set; }
        public WorldManager? WorldManager { get; set; }
        public Empty? PathManager { get; set; }
        public Empty? SessionEconomyManager { get; set; }
        public Empty? DiscoveryManager { get; set; }
        public RegrowManager? RegrowManager { get; set; }
        public SessionSoundManager? SessionSoundManager { get; set; }
        public Empty? ActiveTradeManager { get; set; }
        public Empty? StreetOverlayManager { get; set; }
        public SelectionManager? SelectionManager { get; set; }
        public Empty? CSelectionCursorManager { get; set; }
        public Empty? IncidentManager { get; set; }
        public CameraSequenceManager? CameraSequenceManager { get; set; }
        public Empty? AIUnitManager { get; set; }
        public AIConstructionManager? AIConstructionManager { get; set; }
        public AnimalManager? AnimalManager { get; set; }
        public CSlotManager? CSlotManager { get; set; }
        public Empty? VisitorManager { get; set; }
        public ItemSessionManager? ItemSessionManager { get; set; }
        public Empty? MilitaryManager { get; set; }
        public Empty? BlueprintManager { get; set; }
        public Empty? LoadingPierManager { get; set; }
        public Empty? SessionFreeAreaProductivityManager { get; set; }
        public WorkforceTransferManager? WorkforceTransferManager { get; set; }
        public IrrigationManager? IrrigationManager { get; set; }
        public Empty? WaterManager { get; set; }
        public Empty? CoopPingManager { get; set; }
        public Empty? MemorizeManager { get; set; }
        public SeasonManager? SeasonManager { get; set; }
        public Empty? AreaManager { get; set; }
        public Empty? AreaLinks { get; set; }
        public ShortGrid? AreaIDs { get; set; }
        public List<Tuple<byte, SpawnAreaPoint>>? SpawnAreaPoints { get; set; }
        public List<Tuple<short, AreaManagerDataItem>>? AreaManagerData { get; set; }


    }
}
