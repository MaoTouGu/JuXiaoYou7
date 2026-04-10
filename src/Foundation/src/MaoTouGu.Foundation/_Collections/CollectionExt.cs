using System.Collections.Specialized;
using System.ComponentModel;

namespace MaoTouGu.Foundation.Collections
{
    public static class CollectionExt
    {
        public static readonly PropertyChangedEventArgs         CountPropertyChanged   = new PropertyChangedEventArgs("Count");
        public static readonly PropertyChangedEventArgs         IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");
        public static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        
    }
}