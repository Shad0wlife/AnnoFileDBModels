namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class SessionCameraManager
    {
        public SessionCameraManager()
        {
            EditorSavedCameraStates = Enumerable.Repeat(false, 10).Select(dummy => new EditorSavedCameraState()).ToList();
        }

        public List<EditorSavedCameraState> EditorSavedCameraStates { get; set; }
    }

    public class EditorSavedCameraState
    {
        public EditorSavedCameraState()
        {
            View = new View();
            Projection = new Projection();
        }

        public View View { get; set; }
        public Projection Projection { get; set; }
    }

    public class View
    {
        public View()
        {
            From = new float[] { 100f, 100f, 0f };
            Up = new float[] { 0f, 1f, 0f };
            Direction = new float[] { -0.70710677f, -0.70710677f, 0f };
        }

        public float[] From { get; set; }
        public float[] Up { get; set; }
        public float[] Direction { get; set; }
    }

    public class Projection
    {
        public Projection()
        {
            Flags = 1;
            NearClip = 0.5f;
        }

        public int Flags { get; set; }
        public float NearClip { get; set; }
    }
}
