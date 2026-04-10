using System.Windows.Threading;
using NLog;
using ProgressBar = System.Windows.Controls.ProgressBar;

namespace MaoTouGu.Shells.Controls
{
    public sealed partial class DialogHost : ContentControl
    {

        public static readonly DependencyProperty DialogProperty;
        public static readonly DependencyProperty IsOpenedProperty;
        public static readonly RoutedEvent        NotificationOpeningEvent;
        public static readonly RoutedEvent        NotificationClosingEvent;
        public static readonly RoutedEvent        NotificationChangedEvent;


        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));
            IsOpenedProperty = DependencyProperty.Register(nameof(IsOpened),
                                                           typeof(bool),
                                                           typeof(DialogHost),
                                                           new PropertyMetadata(Boxing.False));
            BusyTextProperty = DependencyProperty.Register(
                                                           nameof(BusyText),
                                                           typeof(string),
                                                           typeof(DialogHost),
                                                           new PropertyMetadata(default(string)));
            IsBusyProperty = DependencyProperty.RegisterReadOnly(
                                                                 nameof(IsBusy),
                                                                 typeof(bool),
                                                                 typeof(DialogHost),
                                                                 new PropertyMetadata(Boxing.False));

            DialogProperty = DependencyProperty.Register(nameof(Dialog),
                                                         typeof(object),
                                                         typeof(DialogHost),
                                                         new PropertyMetadata(default(object)));

            NotificationOpeningEvent = EventManager.RegisterRoutedEvent(
                                                                        nameof(NotificationOpening),
                                                                        RoutingStrategy.Bubble,
                                                                        typeof(RoutedEventHandler),
                                                                        typeof(DialogHost));

            NotificationClosingEvent = EventManager.RegisterRoutedEvent(
                                                                        nameof(NotificationClosing),
                                                                        RoutingStrategy.Bubble,
                                                                        typeof(RoutedEventHandler),
                                                                        typeof(DialogHost));
            NotificationChangedEvent = EventManager.RegisterRoutedEvent(
                                                                        nameof(NotificationChanged),
                                                                        RoutingStrategy.Bubble,
                                                                        typeof(RoutedEventHandler),
                                                                        typeof(DialogHost));
        }

        private readonly Stack<DialogBase> _stack;
        private readonly Lazy<ILogger>     _lazyLogger;

        private ContentPresenter PART_MSG;
        private Border           PART_MsgMask;
        private ProgressBar      PART_ProgressBar;

        public DialogHost()
        {
            _lazyLogger = new Lazy<ILogger>(() => LogManager.GetLogger("DialogHost"));
            _stack      = new Stack<DialogBase>();
            _Queue      = new Queue<Notification>();
            _Timer      = new DispatcherTimer(TimeSpan.FromMilliseconds(250), DispatcherPriority.Render, OnNotificationProc, Dispatcher);
        }

        public override void OnApplyTemplate()
        {
            PART_MSG            = GetTemplateChild("MSG") as ContentPresenter;
            PART_MsgMask        = GetTemplateChild("PART_MsgMask") as Border;
            PART_ProgressBar    = GetTemplateChild("ProgressBar") as ProgressBar;
            base.OnApplyTemplate();
        }

        /// <summary>
        /// 实际对话框内容
        /// </summary>
        public object Dialog
        {
            get => GetValue(DialogProperty);
            private set => SetValue(DialogProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, Boxing.Box(value));
        }
    }
}