namespace KinonekoSoftware.UI.Controls.Buttons
{
    public sealed class CheckBox : System.Windows.Controls.CheckBox
    {
        public static readonly DependencyProperty HighlightProperty;
        
        static CheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBox), new FrameworkPropertyMetadata(typeof(CheckBox)));
            HighlightProperty =
                DependencyProperty.Register(
                                            nameof(Highlight),
                                            typeof(Brush),
                                            typeof(CheckBox),
                                            new PropertyMetadata(default(Brush)));
        }
        
        [Bindable(true)]
        [Category("Appearance")]
        public Brush Highlight
        {
            get => (Brush)GetValue(HighlightProperty);
            set => SetValue(HighlightProperty, value);
        }
    }
}