namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class PropertyTradeRouteVehicle
    {
        public RouteStatusNode? RouteStatus { get; set; }
        public long? TargetBuildingID { get; set; }
        public long? TargetLoadingHarbour { get; set; }
        public Empty? PlannedLoad { get; set; }
        public TradeDescription? TradeDescription { get; set; }
    }

    public class RouteStatusNode
    {
        public int? RouteStatus { get; set; }
    }

    public class TradeDescription
    {
        public Empty? TradeInfos { get; set; }
        public Empty? Source { get; set; }
        public Empty? Target { get; set; }
        public short? Area { get; set; }
        public PassiveTradeBuilding? PassiveTradeBuilding { get; set; }
        public List<int>? SellingMoney { get; set; }
        public List<int>? BuyingMoney { get; set; }

    }

    public class PassiveTradeBuilding
    {
        public long? ObjectID { get; set; }
    }
}
