using NLog;
using Control = System.Windows.Controls.Control;

namespace MaoTouGu.Shells.Controls
{
    public partial class ContentHost : Control
    {
        public static readonly DependencyProperty    ViewModelProperty;
        public static readonly DependencyProperty    ContentProperty;
        public static readonly DependencyPropertyKey ContentPropertyKey;
        public static readonly DependencyProperty    IsInfrastructureProperty;

        private static readonly ILogger _Logger;

        static ContentHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentHost), new FrameworkPropertyMetadata(typeof(ContentHost)));
            ViewModelProperty = DependencyProperty.Register(
                                                            nameof(ViewModel),
                                                            typeof(object),
                                                            typeof(ContentHost),
                                                            new PropertyMetadata(null, OnViewModelChanged));

            ContentPropertyKey = DependencyProperty.RegisterReadOnly(
                                                                     nameof(Content),
                                                                     typeof(object),
                                                                     typeof(ContentHost),
                                                                     new PropertyMetadata(default(object)));
            IsInfrastructureProperty = DependencyProperty.Register(
                                                                   nameof(IsInfrastructure),
                                                                   typeof(bool),
                                                                   typeof(ContentHost),
                                                                   new PropertyMetadata(Boxing.True));

            ContentProperty = ContentPropertyKey.DependencyProperty;
            _Logger         = LogManager.GetLogger(nameof(ContentHost));
        }

        private Window _associatedWindow;


        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            //
            // d必须为ContentControl及其派生类型。
            if (d is not ContentHost contentControl)
            {
                throw new InvalidCastException("ViewLocator不支持为非ContentHost类型的控件，提供VM关联服务！");
            }


            if (e.NewValue is PageBase target)
            {
                try
                {
                    OnViewModelChangedImpl(contentControl, target);
                }
                catch(Exception ex)
                {
                    _Logger.Warn(ex.Message);
                }
            }
            else
            {
                d.ClearValue(ContentPropertyKey);
            }
        }

        static void OnViewModelChangedImpl(ContentHost contentControl, PageBase target)
        {

            if (!Ioc.IsRegistered<IViewLocator>())
            {
                contentControl.ClearValue(ContentPropertyKey);
                return;
            }

            var service = Ioc.Get<IAppModel>();
            var v       = (UserControl)service.GetViewCache(target);

            if (v is null)
            {
                v = (UserControl)ViewService.Instance.GetView(target);
                service.SetViewCache(target, v);
            }

            contentControl.Associate(service, target);
            contentControl.SetValue(ContentPropertyKey, v);
        }

        public ContentHost()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (IsInfrastructure)
            {
                _associatedWindow = Xaml.FindVisualParent<Window>(this);

                if (_associatedWindow is not null)
                {
                    ((IAppModelInitialized)Ioc.Get<IAppModel>()).Attach(_associatedWindow, this);
                }
            }

            Loaded -= OnLoaded;
        }

        private void Associate(IAppModel service, ViewModelBase target)
        {
            _associatedWindow ??= Xaml.FindVisualParent<Window>(this);

            service.SetWindow(target, _associatedWindow);
        }


        public bool IsInfrastructure
        {
            get => (bool)GetValue(IsInfrastructureProperty);
            set => SetValue(IsInfrastructureProperty, value);
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            private set => SetValue(ContentPropertyKey.DependencyProperty, value);
        }

        public object ViewModel
        {
            get => GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}