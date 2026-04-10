using NTSPoint = NetTopologySuite.Geometries.Point;

namespace MaoTouGu.JuXiaoYou.Domain.Geography.Services
{
    public class GeometryRecognitionResult
    {
        public Geometry       Geometry { get; init; }
        public List<NTSPoint> Points   { get; init; }

        public double Height { get; init; }
        public double Width  { get; init; }
    }
}