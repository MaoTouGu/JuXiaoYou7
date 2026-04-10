namespace KinonekoSoftware.UI.Charts
{
    using Avalonia.Media;

    partial class ChartControl
    {
        public static readonly Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);
        public static readonly DirectProperty<ChartControl, ChartControlSelection> SelectionProperty
            = AvaloniaProperty.RegisterDirect<ChartControl, ChartControlSelection>(nameof(Selection), x => x.Selection);

        private ChartControlSelection _selection;

        public ChartControlSelection Selection
        {
            get => _selection;
            set => SetAndRaise(SelectionProperty, ref _selection, value);
        }

        public static partial PathGeometry CreatePathGeometry(Point[] points)
        {
            var figure = new PathFigure
            {
                StartPoint = points[0],
                Segments   = new PathSegments(),
            };
            figure.Segments
                  .AddRange(points.Skip(1)
                                  .Select(x => new LineSegment
                                   {
                                       Point = x,

                                   }));
            return new PathGeometry
            {
                Figures = new PathFigures
                {
                    figure,
                },
            };
        }


        public static partial PathGeometry CreateTriangleGeometry(Point center, Point p1, Point p2)
        {
            var figure = new PathFigure
            {
                StartPoint = center,
                Segments   = new PathSegments(),
            };
            figure.Segments.Add(new LineSegment { Point = p1 });
            figure.Segments.Add(new LineSegment { Point = p2 });
            return new PathGeometry
            {
                Figures = new PathFigures
                {
                    figure,
                },
            };
        }


        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="w">外边框的修正，为了避免轮廓线条溢出</param>
        /// <param name="h">计算得到的直径</param>
        protected partial void GetSize(out double w, out double h)
        {
            w = double.IsInfinity(Bounds.Width) ? 0d : Bounds.Width;
            h = double.IsInfinity(Bounds.Height) ? 0d : Bounds.Height;
        }

        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="amend">外边框的修正，为了避免轮廓线条的</param>
        /// <param name="diameter"></param>
        /// <param name="radius"></param>
        /// <param name="centerPoint"></param>
        protected partial void CalculateRadius(int amend, out double diameter, out double radius, out Point centerPoint)
        {

            var w = double.IsInfinity(Bounds.Width) ? 0 : Bounds.Width;
            var h = double.IsInfinity(Bounds.Height) ? 0 : Bounds.Height;

            diameter    =  Math.Min(w, h);
            radius      =  diameter / 2d;
            centerPoint =  new Point(radius, radius);
            radius      -= amend;

        }


        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="amend">外边框的修正，为了避免轮廓线条溢出</param>
        /// <param name="diameter">计算得到的直径</param>
        /// <param name="radius">计算得到的半径</param>
        /// <param name="centerPoint">计算得到的中心点</param>
        public static partial void CalculateRadius(TemplatedControl control, int amend, out double diameter, out double radius, out Point centerPoint)
        {
            var w = double.IsInfinity(control.Bounds.Width) ? 0 : control.Bounds.Width;
            var h = double.IsInfinity(control.Bounds.Height) ? 0 : control.Bounds.Height;

            diameter    =  Math.Min(w, h);
            radius      =  diameter / 2d;
            centerPoint =  new Point(radius, radius);
            radius      -= amend;
        }

        public static partial Point CreatePoint(double x, double y)
        {
            return new Point(x, y);
        }
    }
}