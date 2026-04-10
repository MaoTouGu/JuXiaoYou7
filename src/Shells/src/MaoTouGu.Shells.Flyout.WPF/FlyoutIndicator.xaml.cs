

using MaoTouGu.Shells.Behaviors;

namespace MaoTouGu.Shells.Controls
{
    public partial class FlyoutIndicator : UserControl
    {
        public FlyoutIndicator()
        {
            InitializeComponent();
        }

        
        private void Button_NextStep(object sender, RoutedEventArgs e)
        {
            if (DataContext is not FlyoutObject { Window: Window window })
            {
                return;
            }
            
            Interaction.GetBehaviors(window)
                       .OfType<WindowBehavior>()
                       .ForEach(x => x.CloseFlyoutInternal());
        }
    }
}