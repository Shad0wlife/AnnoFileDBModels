using FileDBSerializing.ObjectSerializer;

namespace Anno_FileDBModels.Anno1800.MapTemplate
{
    public class MapTemplate
    {
        public int[]? Size { get; set; }
        public int[]? PlayableArea { get; set; }
        public List<RandomlyPlacedThirdParty>? RandomlyPlacedThirdParties { get; set; }
        public int? ElementCount { get; set; }

        [FlatArray]
        public List<TemplateElement>? TemplateElement { get; set; }
    }

    public class MapTemplateDocument
    {
        public MapTemplate? MapTemplate { get; set; }
    }

    public class RandomlyPlacedThirdParty
    {
        public RandomlyPlacedThirdPartyValue? value { get; set; }
    }

    public class RandomlyPlacedThirdPartyValue
    {
        public short? id { get; set; }
    }
}
