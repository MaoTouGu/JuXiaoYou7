namespace MaoTouGu.Shells.AppModels
{
    public abstract class SpaModel : AppModelBase, IAppModelEX
    {
        private readonly Stack<PageBase> _undoStack;
        private readonly Stack<PageBase> _redoStack;
        
        private DialogHost  _dialogHost;
        private ContentHost _contentHost;
        private Window      _mainWindow;

        protected SpaModel()
        {
            _undoStack = new Stack<PageBase>();
            _redoStack = new Stack<PageBase>();
        }
        
        #region Attach / Detach

        
        protected sealed override void OnAppModelControlInitialized(Window window, DialogHost dialogHost, ContentHost contentHost)
        {
            Attach(window, dialogHost, contentHost);
        }

        public void Attach(Window mainWindow, DialogHost dialogHost, ContentHost contentHost)
        {
            _mainWindow  = mainWindow;
            _dialogHost  = dialogHost;
            _contentHost = contentHost;
        }

        protected override void DetachOverride(Window window)
        {
            
        }

        #endregion

        public sealed override void Notify(Notification notification)
        {
            _dialogHost.Notify(notification);
        }

        public bool CanUndo() => _undoStack.Count > 0;
        public bool CanRedo() => _redoStack.Count > 0;


        public void Undo()
        {
            _redoStack.Push(Page);
            Page = _undoStack.Pop();
        }
        
        public void Redo()
        {
            _undoStack.Push(Page);
            Page = _redoStack.Pop();
        }
 

        public sealed override async Task<bool> Navigate(PageBase page, params object[] args)
        {
            if (page is null)
            {
                return false;
            }
            
            if (!await page.Receive(args))
            {
                return false;
            }

            if (!CanNavigateFixed(page, out var theSameOne))
            {
                Page = theSameOne;
                return false;
            }
            
            GUI.RunOnUIThread(() =>
                              {
                                  if (_contentHost is not null)
                                  {
                                      _contentHost.ViewModel = page;
                                  }
                                  _undoStack.Push(page);
                                  _redoStack.Clear();

                                  //
                                  //
                                  Page = page;
                              });
            return true;
        }
        
        public sealed override IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target) => _dialogHost;
        public sealed override IDialogService GetDialogHost(ViewModelBase target) => _dialogHost;

        private PageBase _page;

        public PageBase Page
        {
            get => _page;
            private set => SetValue(ref _page, value);
        }
        
    }
}