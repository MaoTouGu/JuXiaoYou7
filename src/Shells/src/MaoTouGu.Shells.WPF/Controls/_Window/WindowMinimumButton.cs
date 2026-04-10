
using Button = System.Windows.Controls.Button;

namespace MaoTouGu.Shells.Controls
{
    public class WindowMinimumButton : Button
    {
        private MTGWindow _window;
        
        static WindowMinimumButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowMinimumButton), new FrameworkPropertyMetadata(typeof(WindowMinimumButton)));
        }
        
        protected override void OnClick()
        {
            //
            //
            _window ??= Xaml.FindVisualParent<MTGWindow>(this);
            
            //
            //
            if (_window is null)
            {
                return;
            }
            
            if (_window.ResizeMode == ResizeMode.NoResize)
            {
                return;
            }

            _window.WindowState = _window.WindowState = WindowState.Minimized;
            
            base.OnClick();
        }
    }
}