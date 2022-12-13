namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class UpgradeList
    {
        public byte[]? UpgradeGUIDs { get; set; } //Probably int[], but unknown, so byte[] as catch-all
        public Empty? UpgradeSets { get; set; }
        public byte[]? ElectricityUpgrades { get; set; }
        public bool? HasElectricity { get; set; }
    }
}
