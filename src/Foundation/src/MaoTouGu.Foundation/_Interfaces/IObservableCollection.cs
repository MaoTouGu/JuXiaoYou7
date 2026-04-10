

namespace MaoTouGu.Foundation
{
    public interface IObservableCollection<T> : IReadOnlyList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        
    }
}