using FileDBSerializing.EncodingAwareStrings;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SessionSettings
    {
        public SessionSettings()
        {

        }

        /// <summary>
        /// Session Settings for a Map
        /// </summary>
        /// <param name="ambientName"></param>
        /// <param name="mapSize"></param>
        /// <param name="playableSize"></param>
        public SessionSettings(string ambientName, int mapSize, int playableSize)
        {
            GlobalAmbientName = ambientName;

            int margin = (mapSize - playableSize) / 2;
            PlayableArea = new int[] { margin, margin, playableSize + margin, playableSize + margin };
        }

        /// <summary>
        /// Session Settings for an Island
        /// </summary>
        /// <param name="ambientName"></param>
        /// <param name="islandSize"></param>
        /// <param name="playableSize"></param>
        /// <param name="vegetationSetName"></param>
        public SessionSettings(string ambientName, int islandSize, int playableSize, string vegetationSetName)
        {
            GlobalAmbientName = ambientName;

            int margin = (islandSize - playableSize) / 2;
            PlayableArea = new int[] { margin, margin, playableSize + margin, playableSize + margin };

            VegetationPropSetName = vegetationSetName;
        }

        public UnicodeString? GlobalAmbientName { get; set; }
        public UnicodeString? NorthAmbientName { get; set; }
        public UnicodeString? SouthAmbientName { get; set; }

        public int? TimeOfDay { get; set; }

        public int[]? PlayableArea { get; set; }
        public int? EditorSessionGUID { get; set; }
        public bool? HasFogOfWarGrid { get; set; }

        public bool? HasAirGrid { get; set; }

        public UTF8String? VegetationPropSetName { get; set; }
    }
}
