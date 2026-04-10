
namespace MaoTouGu.Shells.Controls
{
    [TemplatePart(Name = Name_PART_Content)]
    public abstract partial class MTGWindow : Window
    {
        internal WindowCloseButton   CloseButton;
        internal WindowMaximumButton MaximumButton;
        internal WindowMinimumButton MinimumButton;

        static MTGWindow()
        {
            WindowStateProperty.AddOwner(typeof(MTGWindow), new FrameworkPropertyMetadata(OnWindowStateChanged));
        }

        internal const string Name_PART_Content = "PART_Content";

        internal ContentPresenter PART_Content;
        
        protected MTGWindow()
        {
            _guideElements = new List<FrameworkElement>(8);
            _guideWizards  = new List<GuideObject>(8);
            //
            // Event
            Loaded   += OnLoaded;
            Unloaded += OnUnloaded;

            //
            //
            Initialize();

            WindowStyle = IsFullscreen ? WindowStyle.None : WindowStyle.SingleBorderWindow;
        }

        #region OnUnloaded / OnLoaded

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            //
            //
            Loaded   -= OnLoaded;
            Unloaded -= OnUnloaded;

            //
            //
            OnUnloadedInternal();
            OnUnloaded();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //
            //
            OnLoadedInternal();
            OnLoaded();
        }

        protected virtual void OnWindowClosing()
        {
        }

        internal virtual void OnLoadedInternal()
        {
        }

        internal virtual void OnUnloadedInternal()
        {

        }

        protected virtual void OnLoaded()
        {

        }

        protected virtual void OnUnloaded()
        {

        }

        #endregion

        public T ViewModel<T>() where T : class
        {
            return DataContext as T;
        }

        #region Initialize

        private void Initialize()
        {
            OnApplyStyle();
        }

        protected virtual void OnApplyStyle()
        {
            Style ??= Application.Current.Resources[nameof(MTGWindow)] as Style;
        }

        #endregion

        #region SystemCommands

        internal async Task OnWindowClose()
        {
            if (await CanClose())
            {
                OnWindowClosing();
                Close();
            }
        }

        protected virtual Task<bool> CanClose() => Task.FromResult(true);

        private void OnWindowMinimum(object sender, ExecutedRoutedEventArgs e)
        {
            if (ResizeMode == ResizeMode.NoResize)
            {
                return;
            }

            WindowState = WindowState.Minimized;
        }

        private void OnWindowRestore(object sender, ExecutedRoutedEventArgs e)
        {
            if (ResizeMode == ResizeMode.NoResize)
            {
                return;
            }

            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        #endregion SystemCommands

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            //
            //
            PART_Content = GetTemplateChild(Name_PART_Content) as ContentPresenter;
        }


        public static bool IsFullscreen { get; set; }
    }
}