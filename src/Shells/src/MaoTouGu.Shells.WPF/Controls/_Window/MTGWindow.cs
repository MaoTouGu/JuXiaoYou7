
using DryIoc;

namespace MaoTouGu.Shells.Controls
{
    [TemplatePart(Name = Name_PART_Content)]
    public abstract partial class MTGWindow : Window
    {
        // internal WindowCloseButton   CloseButton;
        // internal WindowMinimumButton MinimumButton;
        //
        internal WindowMaximumButton MaximumButton;

        static MTGWindow()
        {
            WindowStateProperty.AddOwner(typeof(MTGWindow), new FrameworkPropertyMetadata(OnWindowStateChanged));
        }

        internal const string Name_PART_Content = "PART_Content";

        internal ContentPresenter PART_Content;
        
        protected MTGWindow()
        {
            // _flyoutObjects  = new List<FlyoutObject>(8);
            
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
            if (Ioc.SafeGet<IAppModel>() is AppModelBase appModel)
            {
                appModel.Close(this);
            }

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
            var model = AppModel;
            
            //
            //
            ((IAppModelInitialized)model).Attach(this);
        }

        protected virtual void OnLoaded()
        {

        }

        protected virtual void OnUnloaded()
        {
            ((IAppModelInitialized)AppModel).Detach(this);
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