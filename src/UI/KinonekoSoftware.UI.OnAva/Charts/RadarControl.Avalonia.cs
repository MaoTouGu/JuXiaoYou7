using System.Globalization;
namespace KinonekoSoftware.UI.Charts
{
    using Avalonia.Input;
    using Avalonia.Interactivity;
    using Avalonia.Utilities;

    public partial class RadarControl
    {
        public static readonly StyledProperty<ChartSeriesCollection> ValuesProperty;
        public static readonly StyledProperty<ChartAxisCollection>   AxesProperty;
        public static readonly StyledProperty<bool>                  EnableDebuggerProperty;
        public static readonly StyledProperty<ChartPalette>          PaletteProperty;
        public static readonly StyledProperty<ConnectionLineKind>    ConnectionLineKindProperty;

        static RadarControl()
        {
            ValuesProperty             = AvaloniaProperty.Register<RadarControl, ChartSeriesCollection>(nameof(Values));
            AxesProperty               = AvaloniaProperty.Register<RadarControl, ChartAxisCollection>(nameof(Axes));
            EnableDebuggerProperty     = AvaloniaProperty.Register<RadarControl, bool>(nameof(EnableDebugger));
            PaletteProperty            = AvaloniaProperty.Register<RadarControl, ChartPalette>(nameof(Palette));
            ConnectionLineKindProperty = AvaloniaProperty.Register<RadarControl, ConnectionLineKind>(nameof(ConnectionLineKind));
            ValuesProperty.Changed
                          .Subscribe(x =>
                          {
                              var rc = (RadarControl)x.Sender;
                              OnValueChanged(rc, x.OldValue.GetValueOrDefault(), x.NewValue.GetValueOrDefault());
                          });
            ConnectionLineKindProperty.Changed.Subscribe(OnRenderChanged);
            PaletteProperty.Changed.Subscribe(OnRenderChanged);
            EnableDebuggerProperty.Changed.Subscribe(OnRenderChanged);
        }
        
        private static void DrawingString(DrawingContext dc, Point point, IBrush brush, string text, FontFamily font, double fontSize)
        {
            var t  = new Typeface(font, FontStyle.Normal, FontWeight.Normal, FontStretch.Normal);
            var fs = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, t, fontSize, brush);
            
            point = new Point(point.X - (fs.Width / 2), point.Y - (fs.Height / 2));
            
            dc.DrawText(fs, point);
        }

        private static void OnRenderChanged(AvaloniaPropertyChangedEventArgs x)
        {
            var rc = (RadarControl)x.Sender;
            rc.InvalidateVisual();
        }
        
        private static void OnValueChanged(RadarControl rc, IDataSourceTracker oldValue,  IDataSourceTracker newValue)
        {
            if (newValue is not null)
            {
                newValue.DataChanged += rc.OnValueChanged;
            }
            
            if (oldValue is not null)
            {
                oldValue.DataChanged -= rc.OnValueChanged;
            }
            
            rc.OnValueChanged(rc, EventArgs.Empty);
        }
        
        

        protected void OnValueChanged(object sender, EventArgs e)
        {
            InvalidateVisual();
        }

        public ConnectionLineKind ConnectionLineKind
        {
            get => GetValue(ConnectionLineKindProperty);
            set => SetValue(ConnectionLineKindProperty, value);
        }

        public ChartPalette Palette
        {
            get => GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
        }

        public ChartSeriesCollection Values
        {
            get => GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }


        public bool EnableDebugger
        {
            get => GetValue(EnableDebuggerProperty);
            set => SetValue(EnableDebuggerProperty, value);
        }

        public ChartAxisCollection Axes
        {
            get => GetValue(AxesProperty);
            set => SetValue(AxesProperty, value);
        }

        public override void Render(DrawingContext context)
        {

            //
            // 准备数据
            Prepare();
            
            //
            // 渲染数据
            Draw(context);

            base.Render(context);
        }

        private void DrawAxis(DrawingContext dc, AxisData[] data, ChartAxisCollection axes)
        {
            var foreground = Foreground ?? Xaml.FindResource("Brush.Foreground.Level1") as SolidColorBrush;
            var center     = _data.CenterPoint;
            var fs         = FontSize;
            
            for (var i = 0; i < data.Length; i++)
            {
                var axis   = data[i];
                var r      = axis.Radius + 2 * fs;
                var radian = Angle2Radian(axis.Angle);
                var p = CreatePoint(
                                    x: r * Math.Cos(radian) + center.X,
                                    y: r * Math.Sin(radian) + center.Y);

                //
                //
                DrawingString(dc, p, foreground, axes[i].Name, FontFamily, FontSize);
            }
        }

        protected bool IsHover => IsPointerOver;
    }
}