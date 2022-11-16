using FileDBSerializing.EncodingAwareStrings;
using System.Diagnostics.CodeAnalysis;

namespace Anno_FileDBModels.Anno1800.MapTemplate
{
    public class TemplateElement
    {
        public int? ElementType { get; set; }
        public Element? Element { get; set; }
    }

    public class Element
    {
        // ElementType=0,1,2
        public int[]? Position { get; set; }

        // ElementType=0
        public UnicodeString? MapFilePath { get; set; }
        public byte? Rotation90 { get; set; }
        public UTF8String? IslandLabel { get; set; }
        public int[]? FertilityGuids { get; set; }
        public bool? RandomizeFertilities { get; set; }
        public List<Tuple<long, int>>? MineSlotMapping { get; set; }
        public RandomIslandConfig? RandomIslandConfig { get; set; }

        // ElementType=1
        public short? Size { get; set; }
        public Difficulty? Difficulty { get; set; }
        public Config? Config { get; set; }
    }

    public class RandomIslandConfig
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles")]
        public Config? value { get; set; }
    }

    public class Config
    {
        public IslandType? Type { get; set; }
        public Difficulty? Difficulty { get; set; }
    }

    public class IslandType
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles")]
        public short? id { get; set; }
    }

    public class Difficulty
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles")]
        public short? id { get; set; }
    }
}