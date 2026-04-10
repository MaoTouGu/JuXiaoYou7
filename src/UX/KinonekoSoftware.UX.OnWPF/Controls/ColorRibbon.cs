namespace KinonekoSoftware.UX
{
    public class ColorRibbon : Control
    {
        static ColorRibbon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorRibbon), new FrameworkPropertyMetadata(typeof(ColorRibbon)));
        }
        
        public static readonly DependencyProperty MaskedBrushProperty = DependencyProperty.Register(
            nameof(MaskedBrush),
            typeof(Brush),
            typeof(ColorRibbon),
            new PropertyMetadata(default(Brush)));


        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(ColorRibbon),
            new PropertyMetadata(default(string)));


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(ColorRibbon),
            new PropertyMetadata(default(string)));


        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(ColorRibbon),
            new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Brush MaskedBrush
        {
            get => (Brush)GetValue(MaskedBrushProperty);
            set => SetValue(MaskedBrushProperty, value);
        }
    }
}