namespace MaoTouGu.Shells.AppModels
{
    public class SpaModel : AppModelBase, IAppModelEX
    {
        private readonly Stack<PageBase> _undoStack;
        private readonly Stack<PageBase> _redoStack;
        
        private DialogHost  _dialogHost;
        private ContentHost _contentHost;
        private Window      _mainWindow;

        public SpaModel()
        {
            _undoStack = new Stack<PageBase>();
            _redoStack = new Stack<PageBase>();
        }

        public void Attach(Window mainWindow, DialogHost dialogHost, ContentHost contentHost)
        {
            _mainWindow  = mainWindow;
            _dialogHost  = dialogHost;
            _contentHost = contentHost;
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

        public void Navigate<T>() where T : PageBase => Navigate(ClassStatic.CreateInstance<T>()); 

        public sealed override void Navigate(PageBase page)
        {
            if (page is null)
            {
                return;
            }
            
            _contentHost?.ViewModel = page;
            _undoStack.Push(page);
            _redoStack.Clear();

            //
            //
            Page = page;
        }
        
        public sealed override IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target) => _dialogHost;
        public sealed override IDialogService GetDialogHost(ViewModelBase target) => _dialogHost;

        public PageBase Page
        {
            get;
            set => SetValue(ref field, value);
        }
    }
}