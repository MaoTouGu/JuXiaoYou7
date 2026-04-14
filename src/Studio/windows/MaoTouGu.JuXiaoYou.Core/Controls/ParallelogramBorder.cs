// ----------------------------------------------------------
//            文件：ParallelogramBorder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月13日 18:25
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using System.Windows.Controls;
using System.Windows.Media;

namespace MaoTouGu.JuXiaoYou.Controls
{
    public sealed class ParallelogramBorder : Border
    {


        public static readonly DependencyProperty IsObtuseAngleProperty =
            DependencyProperty.Register(
                                        nameof(IsObtuseAngle),
                                        typeof(bool),
                                        typeof(ParallelogramBorder),
                                        new FrameworkPropertyMetadata(Boxing.False, FrameworkPropertyMetadataOptions.AffectsRender));

        protected override void OnRender(DrawingContext dc)
        {
            var w = ActualWidth;
            var h = ActualHeight;
            var p = Padding;
            var b = BorderThickness;


            var thickness = (b.Bottom + b.Left + b.Top + b.Bottom) / 4f;
            var pen       = new Pen(BorderBrush, thickness);
            var geo       = new StreamGeometry();

            using (var ctx = geo.Open())
            {
                Point lt;
                Point lb;
                Point rt;
                Point rb;

                if (IsObtuseAngle)
                {
                    lt = new Point(0, 0);
                    lb = new Point(p.Left, h);
                    rt = new Point(w - p.Right, 0);
                    rb = new Point(w, h);
                }
                else
                {
                    lt = new Point(p.Left, 0);
                    lb = new Point(0, h);
                    rt = new Point(w, 0);
                    rb = new Point(w - p.Right, h);
                }

                ctx.BeginFigure(lt, true, true);
                ctx.LineTo(rt, true, false);
                ctx.LineTo(rb, true, false);
                ctx.LineTo(lb, true, false);
                ctx.Close();
            }


            dc.DrawGeometry(Background, pen, geo);
        }


        public bool IsObtuseAngle
        {
            get => (bool)GetValue(IsObtuseAngleProperty);
            set => SetValue(IsObtuseAngleProperty, Boxing.Box(value));
        }
    }
}