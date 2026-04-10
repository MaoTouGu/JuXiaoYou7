// ----------------------------------------------------------
//            文件：NTSPointHelper.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月16日 16:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Domain.Geography.Services
{
    using NetTopologySuite;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.IO;
    using NTSPoint = NetTopologySuite.Geometries.Point;
    using Geometry = System.Windows.Media.Geometry;
    
    public static class NTSPointHelper
    {
        
        public static string Write(List<NTSPoint> points)
        {
            var writer = new WKTWriter(2);
            return writer.Write(new MultiPoint(points.ToArray()));
        }

        public static List<NTSPoint> Read(string serialized)
        {
            var reader     = new WKTReader(NtsGeometryServices.Instance);
            var multiPoint = reader.Read(serialized);

            return multiPoint.Coordinates.Select(x => new NTSPoint(x.X, x.Y)).ToList();
        }
        
        public static Result<Geometry> ReadFrom(string serialized, PageBase pageCore)
        {
            var reader     = new WKTReader(NtsGeometryServices.Instance);
            var multiPoint = reader.Read(serialized);
            var points     = multiPoint.Coordinates.ToList();
            
            
            return ImageToGeometryService.Recognize(points, pageCore, multiPoint.EnvelopeInternal);
        }
    }
}