using KinonekoSoftware.Foundation;

namespace KinonekoSoftware.UI.Charts
{
    using Avalonia.Media;


    public partial class StackedProgressControl
    {
        
        public static readonly StyledProperty<ChartSeries>  ValuesProperty;
        public static readonly StyledProperty<ChartPalette> PaletteProperty;
        public static readonly StyledProperty<bool>      IsVerticalProperty;

        static StackedProgressControl()
        {
            ValuesProperty          = AvaloniaProperty.Register<StackedProgressControl, ChartSeries>(nameof(Values));
            PaletteProperty         = AvaloniaProperty.Register<StackedProgressControl, ChartPalette>(nameof(Palette));
            IsVerticalProperty      = AvaloniaProperty.Register<StackedProgressControl, bool>(nameof(IsVertical));
            PaletteProperty.Changed.Subscribe(OnRenderChanged);
            ValuesProperty.Changed.Subscribe(OnRenderChanged);
            IsVerticalProperty.Changed.Subscribe(OnRenderChanged);
        }

        private static void OnRenderChanged(AvaloniaPropertyChangedEventArgs x)
        {
            var rc = (StackedProgressControl)x.Sender;
            rc.InvalidateVisual();
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


        
        public ChartSeries Values
        {
            get => GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }
        
        
        public bool IsVertical
        {
            get => GetValue(IsVerticalProperty);
            set => SetValue(IsVerticalProperty, value);
        }

        public ChartPalette Palette
        {
            get => GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
        }
    }

}