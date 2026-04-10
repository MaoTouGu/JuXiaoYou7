namespace MaoTouGu.Shells.Controls
{
    partial class Flyout
    {
        public static readonly DependencyProperty HintProperty;
        public static readonly DependencyProperty IndexProperty;
        public static readonly DependencyProperty AllowMultipleProperty;
        public static readonly DependencyProperty PlacementProperty;

        static Flyout()
        {
            HintProperty          = DependencyProperty.RegisterAttached("ShadowHint", typeof(string), typeof(Flyout), new PropertyMetadata(default(string)));
            IndexProperty         = DependencyProperty.RegisterAttached("Index", typeof(int), typeof(Flyout), new PropertyMetadata(default(int)));
            PlacementProperty     = DependencyProperty.RegisterAttached("Placement", typeof(Placement), typeof(Flyout), new PropertyMetadata(default(Placement)));
            AllowMultipleProperty = DependencyProperty.RegisterAttached("ShadowAllowMultiple", typeof(bool), typeof(Flyout), new PropertyMetadata(Boxing.False));
        }
    }
}