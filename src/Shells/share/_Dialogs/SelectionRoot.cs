using System.Collections;

namespace MaoTouGu.Shells.Core
{
    public abstract class SelectionRoot<T> : ObjectRoot<List<T>>
    {
        private T      _selected;
        private object _selectionItems;

        protected SelectionRoot(IEnumerable<T> collection)
        {
            Collection = new ViewList<T>(collection);
            Selected   = Collection.FirstOrDefault();
            
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        protected override bool CanFinish() => SelectionItems is IList collection && collection.Cast<T>().Any();

        protected override List<T> OnFinish(bool edit)
        {

            return ((IList)SelectionItems).OfType<T>().ToList();
        }


         
         public object SelectionItems
         {
             get => _selectionItems;
             set => TryFinishAndSetValue(ref _selectionItems, value);
         }
         
        public T Selected
        {
            get => _selected;
            set => TryFinishAndSetValue(ref _selected, value);
        }

        public ViewList<T> Collection { get; }

    }
}