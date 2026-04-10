namespace KinonekoSoftware.UX
{
    public class RarityRibbon : ColorRibbon
    {
        public static readonly DependencyProperty LevelProperty;
        public static readonly DependencyProperty RarityStarColorProperty;

        public static readonly object[] L = new object[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
        };
        
        static RarityRibbon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RarityRibbon), new FrameworkPropertyMetadata(typeof(RarityRibbon)));
            LevelProperty = DependencyProperty.Register(
                nameof(Level),
                typeof(int),
                typeof(RarityRibbon),
                new PropertyMetadata(default(int), PropertyChangedCallback, CoerceLevelValueCallback));
            
            RarityStarColorProperty = DependencyProperty.Register(
                nameof(RarityStarColor),
                typeof(Brush),
                typeof(RarityRibbon),
                new PropertyMetadata(default(Brush)));
        }
        
        private static object CoerceLevelValueCallback(DependencyObject d, object value)
        {
            return L[Math.Clamp((int)value, 1, 12) - 1];
        }
        
        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }


        public Brush RarityStarColor
        {
            get => (Brush)GetValue(RarityStarColorProperty);
            set => SetValue(RarityStarColorProperty, value);
        }
        public int Level
        {
            get => (int)GetValue(LevelProperty);
            set => SetValue(LevelProperty, value);
        }
    }
}