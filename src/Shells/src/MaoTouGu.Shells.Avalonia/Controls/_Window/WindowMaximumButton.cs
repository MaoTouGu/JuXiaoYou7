using Button = System.Windows.Controls.Button;

namespace MaoTouGu.Shells.Controls
{
    public class WindowMaximumButton : Button
    {
        public static readonly DependencyProperty WindowStateProperty;
        
        static WindowMaximumButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                                                     typeof(WindowMaximumButton), 
                                                     new FrameworkPropertyMetadata(typeof(WindowMaximumButton)));
            
            WindowStateProperty = DependencyProperty.Register(
                                                              nameof(WindowState),
                                                              typeof(WindowState),
                                                              typeof(WindowMaximumButton),
                                                              new PropertyMetadata(default(WindowState)));
        }
        
        private MTGWindow _window;
        private bool      _tryAgain;

        public WindowMaximumButton()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _window ??= Xaml.FindVisualParent<MTGWindow>(this);
            
            //
            //
            if (_window is null)
            {
                return;
            }

            WindowState           = _window.WindowState;
            _window.MaximumButton = this;
        }

        protected override void OnClick()
        {
            //
            //
            if (_window is null)
            {
                if (_tryAgain)
                {
                    return;
                }
                
                _window   ??= Xaml.FindVisualParent<MTGWindow>(this);
                _tryAgain =   true;
            }

            if (_window is null)
            {
                return;
            }
            
            if (_window.ResizeMode == ResizeMode.NoResize)
            {
                return;
            }


            _window.WindowState = _window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            base.OnClick();
        }
        

        public WindowState WindowState
        {
            get => (WindowState)GetValue(WindowStateProperty);
            set => SetValue(WindowStateProperty, value);
        }
    }
}