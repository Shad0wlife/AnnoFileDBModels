using FileDBSerializing.EncodingAwareStrings;

namespace Anno.FileDBModels.Anno1800.IslandTemplate
{
    public class MapSource
    {
        public UnicodeString? SourceMap { get; set; }
        public UTF8String? SourceLayerGridUUID { get; set; }
        public UTF8String? SourceLayerGridName { get; set; }
    }
}