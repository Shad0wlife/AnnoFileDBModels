using FileDBSerializing.EncodingAwareStrings;
using FileDBSerializing.ObjectSerializer;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class GameObject
    {
        public int? guid { get; set; }
        public long? ID { get; set; }
        public int? Variation { get; set; }
        public int? ObjectFolderID { get; set; }
        public float[]? Position { get; set; }
        public float? Direction { get; set; }
        public ParticipantID? ParticipantID { get; set; }
        public Prop? Prop { get; set; }
        public Empty? Blocking { get; set; }
        public Empty? Building { get; set; }
        public BuildingModule? BuildingModule { get; set; }
        public Empty? ItemCrafterBuilding { get; set; }
        public Empty? DistributionCenter { get; set; }
        public LoadingPier? LoadingPier { get; set; }
        public RepairCrane? RepairCrane { get; set; }
        public ModuleOwner? ModuleOwner { get; set; }
        public Empty? StreetActivation { get; set; }
        public Empty? Culture { get; set; }

        [RenameProperty("Bus Activation")]
        public Empty? BusActivation { get; set; }
        public Monument? Monument { get; set; }
        public Shipyard? Shipyard { get; set; }
        public Empty? VisitorHarbor { get; set; }
        public PropertyHacienda? PropertyHacienda { get; set; }
        public Empty? PropertyInfluence { get; set; }
        public ItemGenerator? ItemGenerator { get; set; }
        public QuestObject? QuestObject { get; set; }
        public Empty? Attackable { get; set; }
        public Empty? Attacker { get; set; }
        public Collector? Collector { get; set; }
        public Collectable? Collectable { get; set; }
        public CommandQueue? CommandQueue { get; set; }

        [RenameProperty("Delayed Construction")]
        public Empty? DelayedConstruction { get; set; }
        public Draggable? Draggable { get; set; }
        public CampaignBehaviour? CampaignBehaviour { get; set; }
        public FeedbackController? FeedbackController { get; set; }
        public Infolayer? Infolayer { get; set; }
        public Empty? Lifetime { get; set; }
        public Empty? VisualOwnerID { get; set; }
        public Herd? Herd { get; set; }
        public BirdFlock? BirdFlock { get; set; }
        public Empty? FishShoal { get; set; }

        //MESH
        public Mesh? Mesh { get; set; }
        public Projectile? Projectile { get; set; }
        public FogBank? FogBank { get; set; }
        public Empty? EcoSystemProvider { get; set; }
        public DynamicVariation? DynamicVariation { get; set; }
        public CommentBox? CommentBox { get; set; }
        public AmbientArea? AmbientArea { get; set; }
        public Empty? RandomMapObject { get; set; }
        public Empty? IslandUndiscover { get; set; }
        public MetaPersistent? MetaPersistent { get; set; }
        public Empty? Nameable { get; set; }
        public Empty? Pausable { get; set; }
        public Empty? PropertyRandomDummySpawner { get; set; }
        public Rentable? Rentable { get; set; }
        public Selection? Selection { get; set; }
        public BezierPath? BezierPath { get; set; }
        public BodyOfWater? River { get; set; }
        public BodyOfWater? Lake { get; set; }
        public Empty? CoastLine { get; set; }
        public Sellable? Sellable { get; set; }
        public ShipIncident? ShipIncident { get; set; }
        public Empty? ShipMaintenance { get; set; }
        public PropertyTradeRouteVehicle? PropertyTradeRouteVehicle { get; set; }
        public Walking? Walking { get; set; }
        public Empty? LinkedObject { get; set; }
        public TimeBudgetHost? AmbientMoodProvider { get; set; }
        public TimeBudgetHost? SoundEmitter { get; set; }
        public Empty? Distribution { get; set; }
        public Empty? IrrigationSource { get; set; }
        public BuffFactory? BuffFactory { get; set; }
        public LogisticNode? LogisticNode { get; set; }
        public Maintenance? Maintenance { get; set; }
        public Empty? Motorizable { get; set; }
        public Empty? Warehouse { get; set; }
        public Empty? Market { get; set; }
        public Empty? PublicService { get; set; }
        public ItemContainer? ItemContainer { get; set; }
        public UpgradeList? UpgradeList { get; set; }
        public Empty? Electric { get; set; }
        public Empty? Heated { get; set; }
        public Factory7? Factory7 { get; set; }
        public Residence7? Residence7 { get; set; }
        public TrainStation? TrainStation { get; set; }
        public FreeAreaproductivity? FreeAreaproductivity { get; set; }
        public Empty? IncidentInfectable { get; set; }
        public Empty? IncidentInfluencer { get; set; }
        public Empty? IncidentResolver { get; set; }
    }

    public class ParticipantID
    {
        public short? id { get; set; }
    }

    public class Collectable
    {
        public long? Collector { get; set; }
    }

    public class Collector
    {
        public Empty? Collected { get; set; }
    }
    
    public class Draggable
    {
        public long? Dragger { get; set; }
    }

    public class FeedbackController
    {
        public int? CurrentSequence { get; set; }
        public bool? IsFeedbackEnabled { get; set; }
        public Empty? OverrideSequence { get; set; }
        public long? SequenceStartTime { get; set; }
    }

    public class Infolayer
    {
        public List<Empty>? Icons { get; set; }
    }

    public class MetaPersistent
    {
        public long MetaID { get; set; }
    }

    public class Rentable
    {
        public Empty? Tenant { get; set; }
    }

    public class Selection
    {
        public byte[]? Detected { get; set; }
    }

    public class Sellable
    {
        public byte[]? allowedBuyers { get; set; }
    }

    public class ShipIncident
    {
        public ShipIncidentActiveType? activeType { get; set; }
    }

    public class ShipIncidentActiveType
    {
        public Empty? value { get; set; }
    }

    public class TimeBudgetHost
    {
        public int? TimeBudget { get; set; }
    }

    public class ItemContainer
    {
        public long? KamikazeDecal { get; set; }
    }

    public class AmbientArea
    {
        public UnicodeString? AmbientAreaName { get; set; }
        public float? InnerRadius { get; set; }
        public float? OuterRadius { get; set; }
    }

    public class Prop
    {
        public float[]? Position { get; set; }
        public float[]? Rotation { get; set; }
        public float[]? Scale { get; set; }
        public int[]? Color { get; set; }
        public int? RenderFlags { get; set; }
        public bool ? DisablePropGridHandling { get; set; }
    }

    public class CommentBox
    {
        public UnicodeString? Author { get; set; }
        public UnicodeString? CommentText { get; set; }
        public int? Color { get; set; }
    }

    public class BuildingModule
    {
        public long? ParentFactoryID { get; set; }
        public int? CultureSlotIndex { get; set; }
        public bool? Working { get; set; }
    }

    public class LogisticNode
    {
        public List<Tuple<int, StorageItem>>? Storage { get; set; }
        public TransporterQueue? TransporterQueue { get; set; }
    }

    public class StorageItem
    {
        public int? ProductGUID { get; set; }
        public int? CurrentAmount { get; set; }
        public int? MaxAmount { get; set; }
        public Empty? ReservedAmount { get; set; }
        public Empty? ReservedSpace { get; set; }
    }

    public class TransporterQueue
    {
        public Empty? QueuedTransporters { get; set; }
        public Empty? ProcessingTransporters { get; set; }
    }

    public class Herd
    {
        public int? AnimalCount { get; set; }
    }

    public class BodyOfWater
    {
        public int? WaterColor { get; set; }
        public int? RenderFlags { get; set; }
        public float? Turbidity { get; set; }
        public int? RenderOrder { get; set; }
    }

    public class BirdFlock
    {
        public float[]? AreaExtents { get; set; }
        public int? AnimalCount { get; set; }
    }

    public class Maintenance
    {
        public Empty? overridenWorkforce { get; set; }
    }

    public class Factory7
    {
        public ProductionState? ProductionState { get; set; }
        public int? CurrentProductivity { get; set; }
    }

    public class ProductionState
    {
        public Empty? Productivity { get; set; }
    }

    public class FreeAreaproductivity
    {
        public Empty? WorkerInfo { get; set; }
    }

    public class LoadingPier
    {
        public CurrentTradePartner? CurrentTradePartner { get; set; }
        public long? LoadingHarbourID { get; set; }
        public List<CurrentTradePartner>? CurrentTradePartnerArray { get; set; }
        public List<LoadingPierDummy>? LoadingPierDummies { get; set; }
        public Empty? ExclusiveTradeGood { get; set; }
    }

    public class LoadingPierDummy
    {
        public float[]? Position { get; set; }
        public float? RotationY { get; set; }
    }

    public class CurrentTradePartner
    {
        public long? ObjectID { get; set; }
    }

    public class ItemGenerator
    {
        public int? TimeBudget { get; set; }
        public List<int[]>? GeneratedItem { get; set; }
    }

    public class ModuleOwner
    {
        public long[]? BuildingModules { get; set; }
        public long[]? BinArray { get; set; }
        public int? SlotIndexGenerator { get; set; }
        public ModuleOwnerSubmodule? AdditionalModule { get; set; }
        public ModuleOwnerSubmodule? FertilizerModule { get; set; }

    }

    public class ModuleOwnerSubmodule
    {
        public long? ObjectID { get; set; }
    }

    public class CampaignBehaviour
    {
        public Empty? DestructingParticipant { get; set; }
    }

    public class PropertyHacienda
    {
        public ActivePolicy? ActivePolicy { get; set; }
        public List<Empty>? DecreeStates { get; set; }
        public Empty? ActiveEffects { get; set; }
    }

    public class ActivePolicy
    {
        public Empty? value { get; set; }
    }

    public class Residence7
    {
        public int? ResidentCount { get; set; }
        public Empty? ResidenceState { get; set; }
        public int? TimeBeforeRuinLeft { get; set; }
        public long? PopulationMoveTimeout { get; set; }
        public Empty? Saturation { get; set; }
        public List<Tuple<int, ConsumptionItem>>? ConsumptionStates { get; set; }
        public List<Tuple<int, PublicServiceItem>>? PublicServiceStates { get; set; }
    }

    public class ConsumptionItem
    {
        public Empty? SaturationTracker { get; set; }
    }

    public class PublicServiceItem
    {
        public int? DistanceSaturation { get; set; } //might also be a float (distances are often in float), but seen values are all zeroes
        public Empty? SaturationTracker { get; set; }
    }

    public class DynamicVariation
    {
        public UnicodeString? ConfigFilename { get; set; }
    }

    public class FogBank
    {
        public float[]? Area { get; set; }
        public FogBankDesc? FogBankDesc { get; set; }
    }

    public class FogBankDesc
    {
        public float[]? Color { get; set; }
        public float? Density { get; set; }
        public float? MinHeight { get; set; }
        public float? AlphaPower { get; set; }
        public MinMaxItem? LifeTime { get; set; }
        public MinMaxItem? Velocity { get; set; }
        public MinMaxItem? Size { get; set; }
        public MinMaxItem? Deflection { get; set; }

    }

    public class MinMaxItem
    {
        public float? Min { get; set; }
        public float? Max { get; set; }
    }

    public class RepairCrane
    {
        public long? currentRepairTarget { get; set; }
    }

    public class Shipyard
    {
        public Empty? ConstructionQueue { get; set; }
        public float[]? RallyPoint { get; set; }
    }

    public class TrainStation
    {
        public Empty? WaypointList { get; set; }
        public Empty? BlockedTrainInfos { get; set; }
        public long? LastTrainSend { get; set; }
    }
}
