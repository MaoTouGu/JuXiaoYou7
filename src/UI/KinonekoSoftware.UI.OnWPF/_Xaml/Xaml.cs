namespace KinonekoSoftware.UI
{
    public static partial class Xaml
    {
        private static readonly Point     Zero;
        private static readonly Thickness Margin128;

        static Xaml()
        {
            Zero      = new Point(0, 0);
            Margin128 = new Thickness(128, 32, 128, 32);
        }


        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.RegisterAttached(
                                                "Description",
                                                typeof(string),
                                                typeof(Xaml),
                                                new PropertyMetadata(default(string)));

        public static void SetDescription(DependencyObject element, string value)
        {
            element.SetValue(DescriptionProperty, value);
        }

        public static string GetDescription(DependencyObject element)
        {
            return (string)element.GetValue(DescriptionProperty);
        }
    }
}