using KinonekoSoftware.UI;


namespace KinonekoSoftware.UI.Controls
{

    public sealed class Iconify : Control
    {

        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty IsFilledProperty;
        public static readonly DependencyProperty IconSizeProperty;
        public static readonly DependencyProperty IconModeProperty;
        public static readonly DependencyProperty StrokeThicknessProperty;
        
        static Iconify()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Iconify), new FrameworkPropertyMetadata(typeof(Iconify)));
            IconProperty = DependencyProperty.Register(
                nameof(Icon),
                typeof(Geometry),
                typeof(Iconify),
                new PropertyMetadata(default(Geometry)));
            IsFilledProperty = DependencyProperty.Register(
                nameof(IsFilled),
                typeof(bool),
                typeof(Iconify),
                new PropertyMetadata(default(bool)));
            IconSizeProperty = DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(Iconify),
                new PropertyMetadata(default(double)));
            IconModeProperty = DependencyProperty.Register(
                nameof(IconMode),
                typeof(IconMode),
                typeof(Iconify),
                new PropertyMetadata(default(IconMode)));
            StrokeThicknessProperty = DependencyProperty.Register(
                nameof(StrokeThickness),
                typeof(int),
                typeof(Iconify),
                new PropertyMetadata(1));
        }


        public IconMode IconMode
        {
            get => (IconMode)GetValue(IconModeProperty);
            set => SetValue(IconModeProperty, value);
        }

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public bool IsFilled
        {
            get => (bool)GetValue(IsFilledProperty);
            set => SetValue(IsFilledProperty, Boxing.Box(value));
        }

        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public int StrokeThickness
        {
            get => (int)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
    }
}