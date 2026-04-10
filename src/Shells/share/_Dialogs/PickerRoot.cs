
namespace MaoTouGu.Shells.Core
{
    public abstract class PickerRoot<T> : ObjectRoot<T>
    {
        private T   _selected;
        private int _index;
        
        protected PickerRoot()
        {
            Collection  = new ViewList<T>();
            LastCommand = new DelegateCommand(DoLastCommand);
            NextCommand = new DelegateCommand(DoNextCommand);
        }
        
        protected PickerRoot(IEnumerable<T> collection)
        {
            Collection  = new ViewList<T>(collection);
            Selected    = Collection.FirstOrDefault();
            LastCommand = new DelegateCommand(DoLastCommand);
            NextCommand = new DelegateCommand(DoNextCommand);
        }

        protected virtual void OnSelectedChanged(T oldValue, T newValue)
        {
            
        }

        private void DoLastCommand()
        {
            if (Collection.Count == 0)
            {
                return;
            }
            
            if (_index <= 0)
            {
                _index = Collection.Count - 1;
            }
            else
            {
                _index--;
            }

            Selected = Collection[_index];
        }
        
        private void DoNextCommand()
        {
            if (Collection.Count == 0)
            {
                return;
            }
            
            _index = (_index +1) %Collection.Count;

            Selected = Collection[_index];
        }

        protected override bool CanFinish() => Selected is not null;

        protected override T OnFinish(bool edit) => Selected;

        public T Selected
        {
            get => _selected;
            set
            {
                OnSelectedChanged(_selected, value);
                TryFinishAndSetValue(ref _selected, value);

            }
        }

        public ViewList<T> Collection { get; }
        
        public ICommandEX LastCommand { get; }
        public ICommandEX NextCommand { get; }
    }
}