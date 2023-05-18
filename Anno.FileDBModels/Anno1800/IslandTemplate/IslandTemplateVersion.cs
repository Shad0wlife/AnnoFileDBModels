using FileDBSerializing.EncodingAwareStrings;

namespace Anno.FileDBModels.Anno1800.IslandTemplate
{
    public class IslandTemplateVersion
    {
        public List<VersionItem>? VersionList { get; set; }
    }

    public class VersionItem
    {
        public UTF8String? FileTypeName { get; set; }
        public int? FileVersion { get; set; }
    }
}