namespace KinonekoSoftware.UI.Charts
{
#if WINDOWS
    using System.Windows.Media;


    public partial class StackedProgressControl
    {
        public static readonly DependencyProperty PaletteProperty;
        public static readonly DependencyProperty IsVerticalProperty;
        public static readonly DependencyProperty ValuesProperty;

        static StackedProgressControl()
        {
            PaletteProperty = DependencyProperty.Register(
                nameof(Palette),
                typeof(ChartPalette),
                typeof(StackedProgressControl),
                new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsRender));
            IsVerticalProperty = DependencyProperty.Register(
                nameof(IsVertical),
                typeof(bool),
                typeof(StackedProgressControl),
                new FrameworkPropertyMetadata(Boxing.False, FrameworkPropertyMetadataOptions.AffectsRender));
            ValuesProperty = DependencyProperty.Register(
                nameof(Values),
                typeof(ChartSeries),
                typeof(StackedProgressControl),
                new PropertyMetadata(default(ChartSeries), OnValueChanged));
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rc = (StackedProgressControl)d;

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

        protected void OnValueChanged(object sender, EventArgs e)
        {
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //
            // 准备数据
            Prepare();

            // var guide = new GuidelineSet();
            //
            // for (var i = 0; i < _data.Sizes.Length; i++)
            // {
            //     var part = _data.Sizes[i];
            //     if (i == 0)
            //     {
            //         guide.GuidelinesX.Add(part);
            //         guide.GuidelinesY.Add(part);
            //     }
            //     guide.GuidelinesX.Add(part.End.X);
            //     guide.GuidelinesY.Add(part.End.Y);
            // }
            //
            // drawingContext.PushGuidelineSet(guide);
            //
            // 渲染数据
            Draw(drawingContext);
            // drawingContext.Pop();


            base.OnRender(drawingContext);
        }

        public ChartSeries Values
        {
            get => (ChartSeries)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }


        public ChartPalette Palette
        {
            get => (ChartPalette)GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
        }


        public bool IsVertical
        {
            get => (bool)GetValue(IsVerticalProperty);
            set => SetValue(IsVerticalProperty, Boxing.Box(value));
        }
    }

#endif
}