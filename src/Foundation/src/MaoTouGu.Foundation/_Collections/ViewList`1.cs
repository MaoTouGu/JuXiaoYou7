using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MaoTouGu.Foundation.Collections
{
    public class ViewList<T>: Collection<T>, IObservableCollection<T>
    {
        private SimpleMonitor _monitor; // Lazily allocated only when a subclass calls BlockReentrancy() or during serialization. Do not rename (binary serialization)

        [NonSerialized]
        private int _blockReentrancyCount;

        /// <summary>
        /// Initializes a new instance of ViewList that is empty and has default initial capacity.
        /// </summary>
        public ViewList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ViewList class that contains
        /// elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <remarks>
        /// The elements are copied onto the ViewList in the
        /// same order they are read by the enumerator of the collection.
        /// </remarks>
        /// <exception cref="ArgumentNullException"> collection is a null reference </exception>
        public ViewList(IEnumerable<T> collection) : base(new List<T>(collection ??  throw new ArgumentNullException(nameof(collection))))
        {
        }

        /// <summary>
        /// Initializes a new instance of the ViewList class
        /// that contains elements copied from the specified list
        /// </summary>
        /// <param name="list">The list whose elements are copied to the new list.</param>
        /// <param name="useParam">是否使用指定的list作为参数.</param>
        /// <remarks>
        /// The elements are copied onto the ViewList in the
        /// same order they are read by the enumerator of the list.
        /// </remarks>
        /// <exception cref="ArgumentNullException"> list is a null reference </exception>
        public ViewList(IList<T> list, bool useParam = false) : base(GetList(list, useParam))
        {
        }      
        
        private static IList<T> GetList(IList<T> list, bool useParam)
        {
            if (useParam)
            {
                return list ?? new List<T>();
            }

            if (list is null)
            {
                return new List<T>();
            }

            return new List<T>(list);
        }

        /// <summary>
        /// Move item at oldIndex to newIndex.
        /// </summary>
        public void Move(int oldIndex, int newIndex) => MoveItem(oldIndex, newIndex);

        private void AfterSort()
        {
            OnCollectionReset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Sort()
        {
            if (Items is List<T> internalCollection)
            {
                internalCollection.Sort();
            }
            else
            {
                var sorted = Items.ToList();
                
                //
                //
                sorted.Sort();

                //
                //
                Items.AddMany(sorted, true);
            }
            
            AfterSort();
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        public void Sort(Comparison<T> comparison)
        {
            if (Items is List<T> internalCollection)
            {
                internalCollection.Sort(comparison);
            }
            else
            {
                var sorted = Items.ToList();
                
                //
                //
                sorted.Sort(comparison);

                //
                //
                Items.AddMany(sorted, true);
            }
            
            AfterSort();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            if (Items is List<T> internalCollection)
            {
                internalCollection.Sort(index, count, comparer);
            }
            else
            {
                var sorted = Items.ToList();
                
                //
                //
                sorted.Sort(index, count, comparer);

                //
                //
                Items.AddMany(sorted, true);
            }
            
            AfterSort();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Sort(IComparer<T> comparer)
        {
            if (Items is List<T> internalCollection)
            {
                internalCollection.Sort(comparer);
            }
            else
            {
                var sorted = Items.ToList();
                
                //
                //
                sorted.Sort(comparer);

                //
                //
                Items.AddMany(sorted, true);
            }
            
            AfterSort();
        }


        /// <summary>
        /// PropertyChanged event (per <see cref="INotifyPropertyChanged" />).
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => PropertyChanged += value;
            remove => PropertyChanged -= value;
        }

        /// <summary>
        /// Occurs when the collection changes, either by adding or removing an item.
        /// </summary>
        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Called by base class Collection&lt;T&gt; when the list is being cleared;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void ClearItems()
        {
            CheckReentrancy();
            base.ClearItems();
            OnCountPropertyChanged();
            OnIndexerPropertyChanged();
            OnCollectionReset();
        }

        /// <summary>
        /// Called by base class Collection&lt;T&gt; when an item is removed from list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void RemoveItem(int index)
        {
            CheckReentrancy();
            T removedItem = this[index];

            base.RemoveItem(index);

            OnCountPropertyChanged();
            OnIndexerPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, index);
        }

        /// <summary>
        /// Called by base class Collection&lt;T&gt; when an item is added to list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void InsertItem(int index, T item)
        {
            CheckReentrancy();
            base.InsertItem(index, item);

            OnCountPropertyChanged();
            OnIndexerPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        /// <summary>
        /// Called by base class Collection&lt;T&gt; when an item is set in list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected override void SetItem(int index, T item)
        {
            CheckReentrancy();
            T originalItem = this[index];
            base.SetItem(index, item);

            OnIndexerPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, originalItem, item, index);
        }

        /// <summary>
        /// Called by base class ViewList&lt;T&gt; when an item is to be moved within the list;
        /// raises a CollectionChanged event to any listeners.
        /// </summary>
        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            CheckReentrancy();

            T removedItem = this[oldIndex];

            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, removedItem);

            OnIndexerPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Move, removedItem, newIndex, oldIndex);
        }

        /// <summary>
        /// Raises a PropertyChanged event (per <see cref="INotifyPropertyChanged" />).
        /// </summary>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// PropertyChanged event (per <see cref="INotifyPropertyChanged" />).
        /// </summary>
        [field: NonSerialized]
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise CollectionChanged event to any listeners.
        /// Properties/methods modifying this ViewList will raise
        /// a collection changed event through this virtual method.
        /// </summary>
        /// <remarks>
        /// When overriding this method, either call its base implementation
        /// or call <see cref="BlockReentrancy"/> to guard against reentrant collection changes.
        /// </remarks>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null)
            {
                // Not calling BlockReentrancy() here to avoid the SimpleMonitor allocation.
                _blockReentrancyCount++;
                try
                {
                    handler(this, e);
                }
                finally
                {
                    _blockReentrancyCount--;
                }
            }
        }

        /// <summary>
        /// Disallow reentrant attempts to change this collection. E.g. an event handler
        /// of the CollectionChanged event is not allowed to make changes to this collection.
        /// </summary>
        /// <remarks>
        /// typical usage is to wrap e.g. a OnCollectionChanged call with a using() scope:
        /// <code>
        ///         using (BlockReentrancy())
        ///         {
        ///             CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, item, index));
        ///         }
        /// </code>
        /// </remarks>
        protected IDisposable BlockReentrancy()
        {
            _blockReentrancyCount++;
            return EnsureMonitorInitialized();
        }

        /// <summary> Check and assert for reentrant attempts to change this collection. </summary>
        /// <exception cref="InvalidOperationException"> raised when changing the collection
        /// while another collection change is still being notified to other listeners </exception>
        protected void CheckReentrancy()
        {
            if (_blockReentrancyCount > 0)
            {
                // we can allow changes if there's only one listener - the BadRequest
                // only arises if reentrant changes make the original event args
                // invalid for later listeners.  This keeps existing code working
                // (e.g. Selector.SelectedItems).
                if (CollectionChanged?.GetInvocationList().Length > 1)
                    throw new InvalidOperationException("ViewListReentrancyNotAllowed");
            }
        }

        /// <summary>
        /// Helper to raise a PropertyChanged event for the Count property
        /// </summary>
        private void OnCountPropertyChanged() => OnPropertyChanged(CollectionExt.CountPropertyChanged);

        /// <summary>
        /// Helper to raise a PropertyChanged event for the Indexer property
        /// </summary>
        private void OnIndexerPropertyChanged() => OnPropertyChanged(CollectionExt.IndexerPropertyChanged);

        /// <summary>
        /// Helper to raise CollectionChanged event to any listeners
        /// </summary>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        /// <summary>
        /// Helper to raise CollectionChanged event to any listeners
        /// </summary>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        /// <summary>
        /// Helper to raise CollectionChanged event to any listeners
        /// </summary>
        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        /// <summary>
        /// Helper to raise CollectionChanged event with action == Reset to any listeners
        /// </summary>
        protected void OnCollectionReset() => OnCollectionChanged(CollectionExt.ResetCollectionChanged);

        private SimpleMonitor EnsureMonitorInitialized() => _monitor = new SimpleMonitor(this);

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            EnsureMonitorInitialized();
            _monitor!._busyCount = _blockReentrancyCount;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_monitor != null)
            {
                _blockReentrancyCount = _monitor._busyCount;
                _monitor._collection = this;
            }
        }

        // this class helps prevent reentrant calls
        [Serializable]
        [TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
        private sealed class SimpleMonitor : IDisposable
        {
            internal int _busyCount; // Only used during (de)serialization to maintain compatibility with desktop. Do not rename (binary serialization)

            [NonSerialized]
            internal ViewList<T> _collection;

            public SimpleMonitor(ViewList<T> collection)
            {
                Debug.Assert(collection != null);
                _collection = collection;
            }

            public void Dispose() => _collection._blockReentrancyCount--;
        }
    }
}