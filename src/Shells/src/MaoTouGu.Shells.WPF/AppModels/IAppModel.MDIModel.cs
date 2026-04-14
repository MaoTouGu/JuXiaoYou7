namespace MaoTouGu.Shells.AppModels
{
    public abstract class MultipleDocumentModel : AppModelBase
    {
        private DialogHost  _dialogHost;
        private ContentHost _contentHost;
        private Window      _mainWindow;
        private PageBase    _page;

        protected MultipleDocumentModel()
        {
            Tabs = new ViewList<PageBase>();
        }

        protected sealed override void OnAppModelControlInitialized(Window window, DialogHost dialogHost, ContentHost contentHost)
        {
            _mainWindow  = window;
            _dialogHost  = dialogHost;
            _contentHost = contentHost;

            _mainWindow.DataContext = this;
        }

        protected sealed override void OnViewDisconnected(PageContext ctx)
        {
            if (ctx.ViewModel is not PageBase p)
            {
                return;
            }

            Tabs.Remove(p);

            if (Page == p)
            {
                Page = Tabs.FirstOrDefault();
            }
        }

        private void NavigateImpl(PageBase page)
        {
            if (!CanNavigateFixed(page, out var theSameOne))
            {
                Page = theSameOne;
            }
            else
            {
                //
                //
                Tabs.Add(page);

                //
                //
                Page = page;
            }
        }

        public sealed override async Task<bool> Navigate(PageBase page, params object[] args)
        {
            if (page is null)
            {
                return false;
            }

            if (await page.Receive(args))
            {
                GUI.RunOnUIThread(() => NavigateImpl(page));
                return true;
            }

            return false;
        }

        public sealed override IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target) => _dialogHost;
        public sealed override IDialogService GetDialogHost(ViewModelBase target) => _dialogHost;

        public ViewList<PageBase> Tabs { get; }


        public PageBase Page
        {
            get => _page;
            set
            {
                SetValue(ref _page, value);
                _contentHost.ViewModel = value;
            }
        }
    }
}