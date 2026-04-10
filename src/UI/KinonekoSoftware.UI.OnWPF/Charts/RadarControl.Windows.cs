namespace KinonekoSoftware.UI.Charts
{
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class RadarControl
    {

        public static readonly DependencyProperty ValuesProperty
            = DependencyProperty.Register(
                                          nameof(Values),
                                          typeof(ChartSeriesCollection),
                                          typeof(RadarControl),
                                          new PropertyMetadata(default(ChartSeriesCollection), OnValueChanged));


        public static readonly DependencyProperty AxesProperty
            = DependencyProperty.Register(
                                          nameof(Axes),
                                          typeof(ChartAxisCollection),
                                          typeof(RadarControl),
                                          new PropertyMetadata(default(ChartAxisCollection), OnAxesChanged));


        public static readonly DependencyProperty PaletteProperty
            = DependencyProperty.Register(
                                          nameof(Palette),
                                          typeof(ChartPalette),
                                          typeof(RadarControl),
                                          new FrameworkPropertyMetadata(default(ChartPalette), FrameworkPropertyMetadataOptions.AffectsRender));


        public static readonly DependencyProperty ConnectionLineKindProperty
            = DependencyProperty.Register(
                                          nameof(ConnectionLineKind),
                                          typeof(ConnectionLineKind),
                                          typeof(RadarControl),
                                          new FrameworkPropertyMetadata(default(ConnectionLineKind), FrameworkPropertyMetadataOptions.AffectsRender));

        private static void DrawingString(DrawingContext dc, double dpi, Point point, Brush brush, string text, FontFamily font, double fontSize)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            var t  = new Typeface(font, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            var fs = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, t, fontSize, brush, dpi);

            point.X -= (fs.Width  / 2);
            point.Y -= (fs.Height / 2);
            
            dc.DrawText(fs, point);
        }

        private static void OnAxesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rc = (RadarControl)d;
            rc.OnValueChanged(d, EventArgs.Empty);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rc = (RadarControl)d;

            if (e.NewValue is IDataSourceTracker nDST)
            {
                nDST.DataChanged += rc.OnValueChanged;
            }

            if (e.OldValue is IDataSourceTracker oDST)
            {
                oDST.DataChanged -= rc.OnValueChanged;
            }

            rc.OnValueChanged(d, EventArgs.Empty);
        }

        private void DrawAxis(DrawingContext dc, IReadOnlyList<AxisData> data, ChartAxisCollection axes)
        {
            var dpi_scale  = VisualTreeHelper.GetDpi(this);
            var dpi        = Math.Min(dpi_scale.PixelsPerInchX, dpi_scale.PixelsPerInchY);
            var foreground = Foreground ?? Xaml.FindResource("Brush.Foreground.Level1") as SolidColorBrush;
            var center     = _data.CenterPoint;
            var fs         = FontSize;
            
            for (var i = 0; i < data.Count; i++)
            {
                var axis    = data[i];
                var r       = axis.Radius + 2 * fs;
                var radian  = Angle2Radian(axis.Angle);
                var p = CreatePoint(
                                    x: r * Math.Cos(radian) + center.X,
                                    y: r * Math.Sin(radian) + center.Y);
                //
                //
                DrawingString(dc, dpi, p, foreground, axes[i].Name, FontFamily, fs);
            }

        }

        protected void OnValueChanged(object sender, EventArgs e)
        {
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //
            // 准备数据
            Prepare();

            //
            // 渲染数据
            Draw(drawingContext);

            base.OnRender(drawingContext);
        }

        public ChartSeriesCollection Values
        {
            get => (ChartSeriesCollection)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

        public ChartAxisCollection Axes
        {
            get => (ChartAxisCollection)GetValue(AxesProperty);
            set => SetValue(AxesProperty, value);
        }

        public ChartPalette Palette
        {
            get => (ChartPalette)GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
        }


        public ConnectionLineKind ConnectionLineKind
        {
            get => (ConnectionLineKind)GetValue(ConnectionLineKindProperty);
            set => SetValue(ConnectionLineKindProperty, value);
        }

        protected bool IsHover => IsMouseOver;
    }
}