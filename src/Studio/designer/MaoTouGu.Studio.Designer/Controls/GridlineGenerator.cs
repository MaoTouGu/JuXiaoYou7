// ----------------------------------------------------------
//            文件：GridlineGenerator.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 03:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using Geometry = NetTopologySuite.Geometries.Geometry;

namespace MaoTouGu.JuXiaoYou.Controls
{
    public class GridlineGenerator : MarkupExtension
    {
        public GridlineGenerator(){}
        public GridlineGenerator(SolidColorBrush brush) => Brush = brush;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetGeometryDrawing(Brush);
        }

        
        public static Drawing GetGeometryDrawing(SolidColorBrush brush)
        {
            var geo = new DrawingVisual();

            using (var ctx = geo.RenderOpen())
            {
                var p1 = new Pen(brush, 0.25);
                var p3 = new Pen(brush, 0.75);
                
                ctx.DrawLine(p3, new Point(0, 0), new Point(200, 0));
                ctx.DrawLine(p3, new Point(200, 0), new Point(200, 200));
                ctx.DrawLine(p3, new Point(200, 200), new Point(0, 200));
                ctx.DrawLine(p3, new Point(0, 200), new Point(0, 0));
                
                
                //
                // Column
                for (var i = 0; i < 200; i += 20)
                {
                    ctx.DrawLine(p1, new Point(i, 0), new Point(i, 200));
                }
                
                //
                // Row
                for (var i = 0; i < 200; i += 20)
                {
                    ctx.DrawLine(p1, new Point(0, i), new Point(200, i));
                }
            }
            
            return geo.Drawing;
        }

        public static Drawing Gray { get; } = GetGeometryDrawing(Brushes.Gray);
        public SolidColorBrush Brush { get; init; }
    }
}