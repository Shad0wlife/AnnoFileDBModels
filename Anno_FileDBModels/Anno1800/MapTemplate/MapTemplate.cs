using FileDBSerializing.ObjectSerializer;
using System.Diagnostics.CodeAnalysis;

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
        [SuppressMessage("Style", "IDE1006:Naming Styles")]
        public RandomlyPlacedThirdPartyValue? value { get; set; }
    }

    public class RandomlyPlacedThirdPartyValue
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles")]
        public short? id { get; set; }
    }
}
