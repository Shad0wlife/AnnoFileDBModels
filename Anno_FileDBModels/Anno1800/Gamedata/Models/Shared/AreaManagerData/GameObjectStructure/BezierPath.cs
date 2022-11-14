namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.AreaManagerData.GameObjectStructure
{
    public class BezierPath
    {
        public BezierPathElement? Path { get; set; }
    }

    public class BezierPathElement
    {
        public float? TangentScale { get; set; }
        public float[]? Minimum { get; set; }
        public float[]? Maximum { get; set; }
        public List<BezierCurveNode>? BezierCurve { get; set; }
        public int? PathColor { get; set; }
    }

    public class BezierCurveNode
    {
        public float[]? p { get; set; }
        public float[]? i { get; set; }
        public float[]? o { get; set; }
        public float[]? d { get; set; }
        public float? w { get; set; }
        public float[]? u0 { get; set; }

    }
}