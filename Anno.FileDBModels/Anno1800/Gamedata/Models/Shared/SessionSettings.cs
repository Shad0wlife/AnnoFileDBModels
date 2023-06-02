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
        /// <param name="playableArea"></param>
        public SessionSettings(string ambientName, (int x, int y, int size) playableArea)
        {
            GlobalAmbientName = ambientName;

            PlayableArea = new int[] { playableArea.x, playableArea.y, playableArea.x + playableArea.size, playableArea.y + playableArea.size };
        }

        /// <summary>
        /// Session Settings for an Island
        /// </summary>
        /// <param name="ambientName"></param>
        /// <param name="islandSize"></param>
        /// <param name="vegetationSetName"></param>
        /// <param name="playableArea"></param>
        public SessionSettings(string ambientName, int islandSize, string vegetationSetName, (int x, int y, int size)? playableArea = null)
        {
            GlobalAmbientName = ambientName;

            if(playableArea is not null)
            {
                PlayableArea = new int[] { playableArea.Value.x, playableArea.Value.y, playableArea.Value.x + playableArea.Value.size, playableArea.Value.y + playableArea.Value.size };
            }
            else
            {
                PlayableArea = new int[] { 0, 0, islandSize, islandSize };
            }

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
