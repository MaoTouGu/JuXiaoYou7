namespace MaoTouGu.Shells.AppModels
{
    public sealed class MultipleWindowContext : ObservableObject
    {
        private readonly Stack<PageBase>    _undoStack;
        private readonly Stack<PageBase>    _redoStack;
        private readonly ViewList<PageBase> _tabs;

        private PageBase _page;
        private bool     _isMultiple;

        public MultipleWindowContext()
        {
            _undoStack = new();
            _redoStack = new();
            _tabs      = new();
            Redo       = new DelegateCommand(TryRedo, CanRedo);
            Undo       = new DelegateCommand(TryUndo, CanUndo);
        }

        public void NotifyHasTabs()
        {
            IsMultiple = _tabs.Count > 1;
        }

        public void SetPage(PageBase page, bool journal = true)
        {
            Page = page;

            if (journal)
            {
                _undoStack.Push(page);
                _redoStack.Clear();
            }
        }

        public bool CanUndo() => _undoStack.Count > 0;
        public bool CanRedo() => _redoStack.Count > 0;


        public void TryUndo()
        {
            _redoStack.Push(Page);
            Page = _undoStack.Pop();
        }

        public void TryRedo()
        {
            _undoStack.Push(Page);
            Page = _redoStack.Pop();
        }

        public ICommandEX Redo { get; }
        public ICommandEX Undo { get; }

        public ViewList<PageBase> Tabs => _tabs;


        public PageBase Page
        {
            get => _page;
            set => SetValue(ref _page, value);
        }


        public bool IsMultiple
        {
            get => _isMultiple;
            set => SetValue(ref _isMultiple, value);
        }

        public Window      Window      { get; init; }
        public DialogHost  DialogHost  { get; init; }
        public ContentHost ContentHost { get; init; }
        public bool        IsActivate  { get; set; }
    }
}