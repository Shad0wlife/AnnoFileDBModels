using System.Linq;

namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SessionCameraManager
    {
        public SessionCameraManager()
        {

        }

        public SessionCameraManager(bool createDefault)
        {
            EditorSavedCameraStates = Enumerable.Repeat(false, 10).Select(dummy => new EditorSavedCameraState(createDefault)).ToList();
        }

        public List<EditorSavedCameraState>? EditorSavedCameraStates { get; set; }
    }

    public class EditorSavedCameraState
    {
        public EditorSavedCameraState()
        {

        }

        public EditorSavedCameraState(bool createDefault)
        {
            View = new View(createDefault);
            Projection = new Projection(createDefault);
        }

        public View? View { get; set; }
        public Projection? Projection { get; set; }
    }

    public class View
    {
        public View()
        {

        }

        public View(bool createDefault)
        {
            From = new float[] { 100f, 100f, 0f };
            Up = new float[] { 0f, 1f, 0f };
            Direction = new float[] { -0.70710677f, -0.70710677f, 0f };
        }

        public float[]? From { get; set; }
        public float[]? At { get; set; }
        public float[]? Up { get; set; }
        public float[]? Direction { get; set; }
    }

    public class Projection
    {
        public Projection()
        {

        }

        public Projection(bool createDefault)
        {
            Flags = 1;
            NearClip = 0.5f;
        }

        public int? Flags { get; set; }

        public float? FOV { get; set; }

        public float? NearClip { get; set; }
        public float? FarClip { get; set; }
        public int? AspectRatio { get; set; }
    }
}
