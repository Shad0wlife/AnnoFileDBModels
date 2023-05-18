using Anno.FileDBModels.Anno1800.Shared.Grids;

namespace Anno.FileDBModels.Anno1800.IslandTemplate
{
    public class IslandTemplateDocument
    {
        public IslandTemplateDocument()
        {

        }

        public IslandTemplateDocument(int size)
        {
            MapSize = new int[] { size, size };
            int activeGridSize = size / 8;
            double activeGridDiv = activeGridSize switch
            {
                <= 32 => 32.0,
                <= 64 => 64.0,
                <= 96 => 96.0,
                _ => 0.0
            };

            //This is a bunch of magic number stuff
            int activeGridArraySize = (int)(activeGridSize * activeGridSize / 8 * (activeGridDiv / activeGridSize));

            ActiveMapGrid = new BitGrid(activeGridSize, activeGridSize, activeGridArraySize);

        }

        public int[]? MapSize { get; set; }

        public BitGrid? ActiveMapGrid { get; set; }

        public int[]? ActiveMapRect { get; set; }

        public ObjectMetaInfo? ObjectMetaInfo { get; set; }

        public IslandTemplateVersion? Version { get; set; }

        public MapSource? MapSource { get; set; }
    }
}
