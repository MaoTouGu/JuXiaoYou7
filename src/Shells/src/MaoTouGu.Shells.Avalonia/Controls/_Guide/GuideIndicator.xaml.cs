namespace MaoTouGu.Shells.Controls
{
    public partial class GuideIndicator : UserControl
    {
        public GuideIndicator()
        {
            InitializeComponent();
        }

        
        private void Button_NextStep(object sender, RoutedEventArgs e)
        {
            if (DataContext is not GuideObject { Window: MTGWindow window })
            {
                return;
            }
            
            window.CloseGuideInternal();
        }
    }
}