using MaoTouGu.Shells.Core;
using Control = System.Windows.Controls.Control;

namespace MaoTouGu.Shells.Controls
{
    public class NotificationControl : Control
    {
        static NotificationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationControl), new FrameworkPropertyMetadata(typeof(NotificationControl)));
        }

        private Border    PART_Border;
        private TextBlock PART_Title;
        private TextBlock PART_Description;
        
        public NotificationControl()
        {
            DataContextChanged += OnDataContextChanged;
        }
        
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not Notification notification)
            {
                return;
            }

            if (PART_Border is null || PART_Title is null || PART_Description is null)
            {
                return;
            }
            Set(notification);
        }

        void Set(Notification notification)
        {

            PART_Title.Text         = notification.Title;
            PART_Description.Text   = notification.Content;
            PART_Border.Background  = Xaml.ToBrush(notification.Background);
            PART_Border.BorderBrush = Xaml.ToBrush(notification.Color);
        }

        public override void OnApplyTemplate()
        {
            PART_Title       = GetTemplateChild("Title") as TextBlock;
            PART_Description = GetTemplateChild("Description") as TextBlock;
            PART_Border      = GetTemplateChild("PART_Border") as Border;

            if (DataContext is Notification notification)
            {
                Set(notification);
            }

            base.OnApplyTemplate();
        }
    }
}