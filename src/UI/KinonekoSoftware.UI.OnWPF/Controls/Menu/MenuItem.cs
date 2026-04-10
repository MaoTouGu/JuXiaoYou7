using KinonekoSoftware.UI;


namespace KinonekoSoftware.UI.Controls
{
    
    
    public class MenuItem : System.Windows.Controls.MenuItem
    {
        public new static readonly DependencyProperty IconProperty;
        public static readonly     DependencyProperty IsFilledProperty;
        public static readonly     DependencyProperty IconSizeProperty;
        public static readonly     DependencyProperty IconModeProperty;
        public static readonly     DependencyProperty CornerRadiusProperty;
        public static readonly     DependencyProperty StrokeThicknessProperty;

        static MenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem), new FrameworkPropertyMetadata(typeof(MenuItem)));
            IconProperty = DependencyProperty.Register(
                nameof(Icon),
                typeof(Geometry),
                typeof(MenuItem),
                new PropertyMetadata(default(Geometry)));
            IsFilledProperty = DependencyProperty.Register(
                nameof(IsFilled),
                typeof(bool),
                typeof(MenuItem),
                new PropertyMetadata(default(bool)));
            IconSizeProperty = DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(MenuItem),
                new PropertyMetadata(default(double)));
            IconModeProperty = DependencyProperty.Register(
                nameof(IconMode),
                typeof(IconMode),
                typeof(MenuItem),
                new PropertyMetadata(default(IconMode)));
            
            
            CornerRadiusProperty = DependencyProperty.Register(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(MenuItem),
                new PropertyMetadata(default(CornerRadius)));
            
            StrokeThicknessProperty = DependencyProperty.Register(
                nameof(StrokeThickness),
                typeof(int),
                typeof(MenuItem),
                new PropertyMetadata(1));
        }



        protected override bool IsItemItsOwnContainerOverride(object item) => item is FrameworkElement;

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MenuItem();
        }

        //------------------------------------------------
        //
        //  Properties
        //
        //------------------------------------------------

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

        public new Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        
        public int StrokeThickness
        {
            get => (int)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
    }
}