using Button = System.Windows.Controls.Button;

namespace MaoTouGu.Shells.Controls
{
    public sealed class WindowCloseButton : Button
    {
        private MTGWindow _window;
        
        static WindowCloseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowCloseButton), new FrameworkPropertyMetadata(typeof(WindowCloseButton)));
        }

        protected override async void OnClick()
        {
            _window ??= Xaml.FindVisualParent<MTGWindow>(this);
            
            //
            //
            if (_window is null)
            {
                return;
            }
            
            //
            //
            await _window.OnWindowClose();
            
            base.OnClick();
        }
    }
}