// ----------------------------------------------------------
//            文件：VisualConnector.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 17:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core
{
    public static class VisualConnector
    {
        public static readonly DependencyProperty ConnectProperty =
            DependencyProperty.RegisterAttached(
                                                "Connect",
                                                typeof(bool),
                                                typeof(VisualConnector),
                                                new PropertyMetadata(default(bool), OnConnectPropertyChanged));

        sealed class VisualConnectorBehavior : Behavior<FrameworkElement>
        {
            protected override void OnAttached()
            {
                Connect();
                AssociatedObject.Loaded += OnLoaded;
            }

            protected override void OnDetaching()
            {
                Connect();
                Disconnect();
                AssociatedObject.Loaded -= OnLoaded;
            }
            
            void Connect()
            {
                if (!AssociatedObject.IsInitialized)
                {
                    return;
                }
                
                if (AssociatedObject.DataContext is IVisualConnector ivc)
                {
                    ivc.Control = AssociatedObject;
                }
            } 
            
            void Disconnect()
            {
                if (AssociatedObject.DataContext is IVisualConnector ivc)
                {
                    ivc.Control = null;
                }
            }
            
            
            private void OnLoaded(object sender, RoutedEventArgs e)
            {
                Connect();
            }
        }

        private static void OnConnectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var collection = Interaction.GetBehaviors(d);

            if (collection.FirstOrDefault(x => x is VisualConnectorBehavior) is not null)
            {
                return;
            }

            collection.Add(new VisualConnectorBehavior());
        }

        public static void SetConnect(DependencyObject element, bool value)
        {
            element.SetValue(ConnectProperty, value);
        }

        public static bool GetConnect(DependencyObject element)
        {
            return (bool)element.GetValue(ConnectProperty);
        }
    }
}