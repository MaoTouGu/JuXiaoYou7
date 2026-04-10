// ----------------------------------------------------------
//            文件：PasswordAssist.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 18:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells
{
    public static class PasswordAssist
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
                                                                                            "Text",
                                                                                            typeof(string),
                                                                                            typeof(PasswordAssist),
                                                                                            new PropertyMetadata("!@_AS#@", OnTextChanged));
        
        sealed class PasswordBehavior : Behavior<PasswordBox>
        {
            protected override void OnAttached()
            {
                AssociatedObject.PasswordChanged += OnPasswordChanged;
                base.OnAttached();
            }

            protected override void OnDetaching()
            {
                AssociatedObject.PasswordChanged -= OnPasswordChanged;
                base.OnDetaching();
            }

            public void Connect()
            {
                AssociatedObject.PasswordChanged += OnPasswordChanged;
            }
            
            public void Disconnect()
            {
                AssociatedObject.PasswordChanged -= OnPasswordChanged;
            }
            
            private static void OnPasswordChanged(object sender, RoutedEventArgs e)
            {
                var d = (PasswordBox)sender;
            
                //
                //
                d.SetValue(TextProperty, d.Password);
            }
        }
        

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (PasswordBox)d;

            var collection = Interaction.GetBehaviors(sender);
            var behavior   = collection.OfType<PasswordBehavior>().FirstOrDefault();
            
            if (behavior is null)
            {
                sender.Password = e.NewValue?.ToString();
                behavior        = new PasswordBehavior();
                collection.Add(behavior);
            }
            else if (e.NewValue?.ToString() != sender.Password)
            {
                behavior.Disconnect();
                sender.Password = e.NewValue?.ToString();
                behavior.Connect();
            }
        }

        public static void SetText(PasswordBox element, string value)
        {
            element.SetValue(TextProperty, value);
        }

        public static string GetText(PasswordBox element)
        {
            return (string)element.GetValue(TextProperty);
        }
    }
}