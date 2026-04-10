using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using NetTopologySuite.Index.Strtree;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Geometry = System.Windows.Media.Geometry;
using SixLaborsImage = SixLabors.ImageSharp.Image;
using NTSPoint = NetTopologySuite.Geometries.Point;
using wpfPoint = System.Windows.Point;

namespace MaoTouGu.JuXiaoYou.Domain.Geography.Services
{
    public static class ImageToGeometryService
    {
        private static List<NTSPoint> SortByNearestNeighbor(List<NTSPoint> points)
        {
            var sorted    = new List<NTSPoint>();
            var remaining = new List<NTSPoint>(points);

            //
            // 使用极点来实现排序。
            var current = remaining.OrderByDescending(p => p.Y).ThenBy(p => p.X).First();
            sorted.Add(current);
            remaining.Remove(current);

            while (remaining.Count > 0)
            {
                var next = remaining.OrderBy(p => current.Distance(p)).First();
                sorted.Add(next);
                remaining.Remove(next);
                current = next;
            }

            return sorted;
        }
        
        private static List<Coordinate> SortByNearestNeighbor(List<Coordinate> points)
        {
            var sorted    = new List<Coordinate>();
            var remaining = new List<Coordinate>(points);

            //
            // 使用极点来实现排序。
            var current = remaining.OrderByDescending(p => p.Y).ThenBy(p => p.X).First();
            sorted.Add(current);
            remaining.Remove(current);

            while (remaining.Count > 0)
            {
                var next = remaining.OrderBy(p => current.Distance(p)).First();
                sorted.Add(next);
                remaining.Remove(next);
                current = next;
            }

            return sorted;
        }
        
        private static List<NTSPoint> ClusterNTSPoints(List<NTSPoint> points, double tolerance)
        {
            var tree     = new STRtree<NTSPoint>();
            var visited  = new HashSet<NTSPoint>();
            var clusters = new List<NTSPoint>();

            // 构建空间索引
            foreach (var point in points)
            {
                tree.Insert(point.EnvelopeInternal, point);
            }

            // 查询聚类
            foreach (var point in points)
            {
                if (visited.Contains(point)) continue;

                // 创建搜索范围
                var env = new Envelope(
                                       point.X - tolerance,
                                       point.X + tolerance,
                                       point.Y - tolerance,
                                       point.Y + tolerance);

                // 查询范围内的点
                var candidates = tree.Query(env);
                var cluster    = new List<NTSPoint>();

                // 精确筛选
                foreach (var candidate in candidates)
                {
                    if (point.Distance(candidate) <= tolerance && !visited.Contains(candidate))
                    {
                        cluster.Add(candidate);
                        visited.Add(candidate);
                    }
                }

                // 计算聚类中心
                if (cluster.Count > 0)
                {
                    var centerX = cluster.Average(p => p.X);
                    var centerY = cluster.Average(p => p.Y);
                    clusters.Add(new NTSPoint(new Coordinate(centerX, centerY)));
                }
            }

            return clusters;
        }
        
        private static List<Coordinate> ClusterNTSPoints(List<Coordinate> points, double tolerance, Envelope envelope)
        {
            var tree     = new STRtree<Coordinate>();
            var visited  = new HashSet<Coordinate>();
            var clusters = new List<Coordinate>();

            // 构建空间索引
            foreach (var point in points)
            {
                tree.Insert(envelope, point);
            }

            // 查询聚类
            foreach (var point in points)
            {
                if (visited.Contains(point)) continue;

                // 创建搜索范围
                var env = new Envelope(
                                       point.X - tolerance,
                                       point.X + tolerance,
                                       point.Y - tolerance,
                                       point.Y + tolerance);

                // 查询范围内的点
                var candidates = tree.Query(env);
                var cluster    = new List<Coordinate>();

                // 精确筛选
                foreach (var candidate in candidates)
                {
                    if (point.Distance(candidate) <= tolerance && !visited.Contains(candidate))
                    {
                        cluster.Add(candidate);
                        visited.Add(candidate);
                    }
                }

                // 计算聚类中心
                if (cluster.Count > 0)
                {
                    var centerX = cluster.Average(p => p.X);
                    var centerY = cluster.Average(p => p.Y);
                    clusters.Add(new Coordinate(centerX, centerY));
                }
            }

            return clusters;
        }

        private static void ReduceZeroPoint(List<NTSPoint> newPoints)
        {
            var zero = newPoints.Where(x => x.X == 0 || x.Y == 0)
                                .TakeWhile((_, i) => i < 5)
                                .ToArray();
            if (zero.Length < 5)
            {
                foreach (var zp in zero)
                {
                    newPoints.Remove(zp);
                }
            }
        }
        
        private static void ReduceZeroPoint(List<Coordinate> newPoints)
        {
            var zero = newPoints.Where(x => x.X == 0 || x.Y == 0)
                                .TakeWhile((_, i) => i < 5)
                                .ToArray();
            if (zero.Length < 5)
            {
                foreach (var zp in zero)
                {
                    newPoints.Remove(zp);
                }
            }
        }
        
        private static StreamGeometry BuildGeometryFromCoordinates(List<NTSPoint> coords)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                if (coords.Count > 0)
                {
                    var start = new wpfPoint(coords[0].X, coords[0].Y);
                    ctx.BeginFigure(start, isFilled: true, isClosed: true);

                    for (var i = 1; i < coords.Count; i++)
                    {
                        var pt = new wpfPoint(coords[i].X, coords[i].Y);
                        ctx.LineTo(pt, isStroked: true, isSmoothJoin: false);
                    }
                }
            }

            geometry.Freeze();
            return geometry;
        }
        
        private static StreamGeometry BuildGeometryFromCoordinates(List<Coordinate> coords)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                if (coords.Count > 0)
                {
                    var start = new wpfPoint(coords[0].X, coords[0].Y);
                    ctx.BeginFigure(start, isFilled: true, isClosed: true);

                    for (var i = 1; i < coords.Count; i++)
                    {
                        var pt = new wpfPoint(coords[i].X, coords[i].Y);
                        ctx.LineTo(pt, isStroked: true, isSmoothJoin: false);
                    }
                }
            }

            geometry.Freeze();
            return geometry;
        }

        private static Task<List<NTSPoint>> Detect(byte[] buffer)
        {
            return Task.Run(() =>
                            {
                                var image = SixLaborsImage.Load<Argb32>(buffer);
                                var list  = new List<NTSPoint>();
                                image.ProcessPixelRows(t =>
                                                       {
                                                           var last = 128;

                                                           for (var y = 0; y < t.Height; y++)
                                                           {
                                                               var i = t.GetRowSpan(y);

                                                               for (var x = 0; x < t.Width; x++)
                                                               {
                                                                   var pixel = i[x];

                                                                   if (pixel.A != last)
                                                                   {
                                                                       list.Add(new NTSPoint(x, y));
                                                                       last = pixel.A;
                                                                   }
                                                               }
                                                           }
                                                       });

                                return list;
                            });
        }
        
        public static async Task<Result<GeometryRecognitionResult>> Recognize(PageBase target)
        {
            var r = Interop.OpenFileAsync(SR.Image_Png);

            if (!r.IsFinished)
            {
                return Result<GeometryRecognitionResult>.Failure;
            }

            try
            {
                var buffer   = await File.ReadAllBytesAsync(r.Value);
                var original = await Detect(buffer);
                
                if (original.Count < 4)
                {
                    target.Warning(SR.Title_Warning, "识别到的边界点少于3个，无法组成一个平面", 8);
                    return Result<GeometryRecognitionResult>.Failure;
                }
                
                //
                // 试验证明，21这个数字是最佳的缩减tolerance
                var newPoints = ClusterNTSPoints(original, 21);

                //
                // 移除部分0坐标点，这些坐标点很可能是NetTopologySuite组件引入的，
                // 并且进行排序
                newPoints = SortByNearestNeighbor(newPoints);
                ReduceZeroPoint(newPoints);

                var geometry    = BuildGeometryFromCoordinates(newPoints);
                var multiPoints = new MultiPoint(newPoints.ToArray());
                
                return Result<GeometryRecognitionResult>.Success(new GeometryRecognitionResult
                {
                    Geometry = geometry,
                    Points = newPoints,
                    Width = multiPoints.EnvelopeInternal.Width,
                    Height = multiPoints.EnvelopeInternal.Height,
                });
            }
            catch(Exception ex)
            {
                target.Warning(SR.Title_Warning, $"在将图片转化成形状时出现错误，{ex.Message}", 8);
                return Result<GeometryRecognitionResult>.Failure;
                
            }
        }
        
        public static async Task<Result<GeometryRecognitionResult>> Recognize(byte[] buffer, PageBase target)
        {
            try
            {
                var original = await Detect(buffer);
                
                if (original.Count < 4)
                {
                    target.Warning(SR.Title_Warning, "识别到的边界点少于3个，无法组成一个平面", 8);
                    return Result<GeometryRecognitionResult>.Failure;
                }
                
                //
                // 试验证明，21这个数字是最佳的缩减tolerance
                var newPoints = ClusterNTSPoints(original, 21);

                //
                // 移除部分0坐标点，这些坐标点很可能是NetTopologySuite组件引入的，
                // 并且进行排序
                newPoints = SortByNearestNeighbor(newPoints);
                ReduceZeroPoint(newPoints);

                var geometry    = BuildGeometryFromCoordinates(newPoints);
                var multiPoints = new MultiPoint(newPoints.ToArray());
                
                return Result<GeometryRecognitionResult>.Success(new GeometryRecognitionResult
                {
                    Geometry = geometry,
                    Points   = newPoints,
                    Width    = multiPoints.EnvelopeInternal.Width,
                    Height   = multiPoints.EnvelopeInternal.Height,
                });
            }
            catch(Exception ex)
            {
                target.Warning(SR.Title_Warning, $"在将图片转化成形状时出现错误，{ex.Message}", 8);
                return Result<GeometryRecognitionResult>.Failure;
                
            }
        }
        
        public static Result<Geometry> Recognize(List<Coordinate> original, PageBase target, Envelope envelope)
        {
            try
            {
                
                if (original.Count < 4)
                {
                    target.Warning(SR.Title_Warning, "识别到的边界点少于3个，无法组成一个平面", 8);
                    return Result<Geometry>.Failure;
                }
                
                //
                // 试验证明，21这个数字是最佳的缩减tolerance
                var newPoints = ClusterNTSPoints(original, 21, envelope);

                //
                // 移除部分0坐标点，这些坐标点很可能是NetTopologySuite组件引入的，
                // 并且进行排序
                newPoints = SortByNearestNeighbor(newPoints);
                ReduceZeroPoint(newPoints);

                var geometry = BuildGeometryFromCoordinates(newPoints);
                return Result<Geometry>.Success(geometry);
            }
            catch(Exception ex)
            {
                target.Warning(SR.Title_Warning, $"在将图片转化成形状时出现错误，{ex.Message}", 8);
                return Result<Geometry>.Failure;
                
            }
        }

    }
}