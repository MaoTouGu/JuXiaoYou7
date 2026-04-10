

namespace KinonekoSoftware.UI.Charts
{
#if WINDOWS
    using Avalonia.Media;
    using Avalonia.Shapes;

    partial class ChartControl
    {

        public static readonly Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);
        public static readonly DependencyPropertyKey SelectionPropertyKey;
        public static readonly DependencyProperty    SelectionProperty;

        static ChartControl()
        {
            SelectionPropertyKey = DependencyProperty.RegisterReadOnly(
                nameof(Selection),
                typeof(ChartControlSelection),
                typeof(ChartControl),
                new UIPropertyMetadata(default(ChartControlSelection), OnSelectionChanged));
            SelectionProperty = SelectionPropertyKey.DependencyProperty;
        }
        
        private static void OnSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }



        public static partial PathGeometry CreatePathGeometry(Point[] points)
        {
            var figure = new PathFigure(points[0], points
                                                   .Skip(1)
                                                   .Select(x => new LineSegment(x, true)), true);
            return new PathGeometry
            {
                Figures = new PathFigureCollection
                {
                    figure,
                },
            };
        }

        

        public static partial PathGeometry CreateTriangleGeometry(Point center, Point p1, Point p2)
        {
            var figure = new PathFigure(center,
                new PolyLineSegment[]
                {
                    new PolyLineSegment(new []{ p1,p2}, true),
                }, true);
            return new PathGeometry
            {
                Figures = new PathFigureCollection
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
            w  = double.IsInfinity(ActualWidth) ? 0d : ActualWidth;
            h = double.IsInfinity(ActualHeight) ? 0d : ActualHeight;
        }

        /// <summary>
        /// 计算半径
        /// </summary>
        /// <param name="amend">外边框的修正，为了避免轮廓线条溢出</param>
        /// <param name="diameter">计算得到的直径</param>
        /// <param name="radius">计算得到的半径</param>
        /// <param name="centerPoint">计算得到的中心点</param>
        protected partial void CalculateRadius(int amend, out double diameter, out double radius, out Point centerPoint)
        {
            var w = double.IsInfinity(ActualWidth) ? 0 : ActualWidth ;
            var h = double.IsInfinity(ActualHeight) ? 0 : ActualHeight;

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
            var w = double.IsInfinity(control.ActualWidth) ? 0 : control.ActualWidth ;
            var h = double.IsInfinity(control.ActualHeight) ? 0 : control.ActualHeight;

            diameter    =  Math.Min(w, h);
            radius      =  diameter / 2d;
            centerPoint =  new Point(radius, radius);
            radius      -= amend;
        }


        public static partial Point CreatePoint(double x, double y)
        {
            return new Point(x, y);
        }
        
        public ChartControlSelection Selection
        {
            get => (ChartControlSelection)GetValue(SelectionPropertyKey.DependencyProperty);
            protected set => SetValue(SelectionPropertyKey, value);
        }
    }
#endif
}