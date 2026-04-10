// ----------------------------------------------------------
//            文件：ViewTable.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 02:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MaoTouGu.Foundation.Collections
{

    /// <summary>
    /// 表示一个可观察的字典，当添加、移除、替换或清空项时提供更改通知。
    /// 实现 IDictionary&lt;TKey, TValue&gt;、INotifyCollectionChanged 和 INotifyPropertyChanged。
    /// </summary>
    /// <typeparam name="TKey">字典中键的类型。</typeparam>
    /// <typeparam name="TValue">字典中值的类型。</typeparam>
    public class ViewTable<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged, ICollection, IDictionary
    {
        private readonly Dictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// 初始化 ViewTable 类的新实例，该实例为空且具有默认的初始容量。
        /// </summary>
        public ViewTable()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// 初始化 ViewTable 类的新实例，该实例为空且具有指定的初始容量。
        /// </summary>
        /// <param name="capacity">字典可包含的初始元素数。</param>
        public ViewTable(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// 初始化 ViewTable 类的新实例，该实例使用指定的相等比较器。
        /// </summary>
        /// <param name="comparer">比较键时要使用的相等比较器。</param>
        public ViewTable(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// 初始化 ViewTable 类的新实例，该实例包含从指定字典复制的元素，并使用指定的相等比较器。
        /// </summary>
        /// <param name="dictionary">要将其元素复制到新字典的字典。</param>
        /// <param name="comparer">比较键时要使用的相等比较器。</param>
        public ViewTable(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
        }

        /// <summary>
        /// 初始化 ViewTable 类的新实例，该实例包含从指定字典复制的元素。
        /// </summary>
        /// <param name="dictionary">要将其元素复制到新字典的字典。</param>
        public ViewTable(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary);
        }

        // IDictionary<TKey, TValue> 成员

        /// <summary>
        /// 获取包含字典中的键的集合。
        /// </summary>
        public ICollection<TKey> Keys => _dictionary.Keys;


        /// <summary>
        /// 获取包含字典中的值的集合。
        /// </summary>
        public ICollection<TValue> Values => _dictionary.Values;

        /// <summary>
        /// 获取或设置具有指定键的元素。
        /// </summary>
        /// <param name="key">要获取或设置的元素的键。</param>
        /// <returns>具有指定键的元素。</returns>
        public TValue this[TKey key]
        {
            get
            {
                if (key is string str)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        return default;
                    }
                }

                if (key is null)
                {
                    return default;
                }
                
                return _dictionary.GetValueOrDefault(key);
            }
            set
            {
                var exists   = _dictionary.ContainsKey(key);
                var oldValue = exists ? _dictionary[key] : default;

                _dictionary[key] = value;

                if (exists)
                {
                    // 替换操作
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                                                             NotifyCollectionChangedAction.Replace,
                                                                             new KeyValuePair<TKey, TValue>(key, value),
                                                                             new KeyValuePair<TKey, TValue>(key, oldValue),
                                                                             -1));
                    OnPropertyChanged("Item[]");
                }
                else
                {
                    // 添加操作
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                                                             NotifyCollectionChangedAction.Add,
                                                                             new KeyValuePair<TKey, TValue>(key, value),
                                                                             -1));
                    OnPropertyChanged(nameof(Count));
                    OnPropertyChanged("Item[]");
                }
            }
        }

        /// <summary>
        /// 获取字典中实际包含的元素数。
        /// </summary>
        public int Count => _dictionary.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        /// <summary>
        /// 将指定的键和值添加到字典中。
        /// </summary>
        /// <param name="key">要添加的元素的键。</param>
        /// <param name="value">要添加的元素的值。</param>
        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                                                     NotifyCollectionChangedAction.Add,
                                                                     new KeyValuePair<TKey, TValue>(key, value),
                                                                     -1));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
        }

        /// <summary>
        /// 确定字典是否包含指定键。
        /// </summary>
        /// <param name="key">要在字典中定位的键。</param>
        /// <returns>如果字典包含具有指定键的元素，则为 true；否则为 false。</returns>
        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        /// <summary>
        /// 从字典中移除具有指定键的元素。
        /// </summary>
        /// <param name="key">要移除的元素的键。</param>
        /// <returns>如果成功移除元素，则为 true；否则为 false。如果在字典中没有找到 key，也返回 false。</returns>
        public bool Remove(TKey key)
        {
            if (_dictionary.TryGetValue(key, out var value))
            {
                var removed = _dictionary.Remove(key);
                if (removed)
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                                                             NotifyCollectionChangedAction.Remove,
                                                                             new KeyValuePair<TKey, TValue>(key, value),
                                                                             -1));
                    OnPropertyChanged(nameof(Count));
                    OnPropertyChanged("Item[]");
                }
                return removed;
            }
            return false;
        }

        /// <summary>
        /// 尝试获取与指定键关联的值。
        /// </summary>
        /// <param name="key">要获取的值的键。</param>
        /// <param name="value">当此方法返回时，如果找到键，则包含与键关联的值；否则包含 default 值。</param>
        /// <returns>如果字典包含具有指定键的元素，则为 true；否则为 false。</returns>
        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        // ICollection<KeyValuePair<TKey, TValue>> 成员

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            if (_dictionary.Count == 0)
                return;

            _dictionary.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged("Item[]");
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.TryGetValue(item.Key, out var value) && EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (((ICollection<KeyValuePair<TKey, TValue>>)this).Contains(item))
            {
                return Remove(item.Key);
            }
            return false;
        }

        // IEnumerable<KeyValuePair<TKey, TValue>> 成员

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        // IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();

        // INotifyCollectionChanged 成员

        /// <summary>
        /// 当集合更改时发生。
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// 引发 CollectionChanged 事件。
        /// </summary>
        /// <param name="e">事件数据。</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        // INotifyPropertyChanged 成员

        /// <summary>
        /// 当属性值更改时发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 引发 PropertyChanged 事件。
        /// </summary>
        /// <param name="propertyName">更改的属性名称。</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 显式接口实现 (非泛型 IDictionary)

        bool IDictionary.       IsFixedSize => false;
        bool IDictionary.       IsReadOnly  => false;
        ICollection IDictionary.Keys        => _dictionary.Keys;
        ICollection IDictionary.Values      => _dictionary.Values;

        object IDictionary.this[object key]
        {
            get
            {
                if (key is TKey tKey)
                    return this[tKey];
                return null;
            }
            set
            {
                if (key is TKey tKey && value is TValue tValue)
                    this[tKey] = tValue;
                else
                    throw new ArgumentException("键或值的类型不正确");
            }
        }

        void IDictionary.Add(object key, object value)
        {
            if (key is TKey tKey && value is TValue tValue)
                Add(tKey, tValue);
            else
                throw new ArgumentException("键或值的类型不正确");
        }

        void IDictionary.Clear()
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this).Clear();
        }

        bool IDictionary.Contains(object key)
        {
            if (key is TKey tKey)
                return ContainsKey(tKey);
            return false;
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)_dictionary).GetEnumerator();
        }

        void IDictionary.Remove(object key)
        {
            if (key is TKey tKey)
                Remove(tKey);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_dictionary).CopyTo(array, index);
        }

        int ICollection.   Count          => _dictionary.Count;
        bool ICollection.  IsSynchronized => false;
        object ICollection.SyncRoot       => ((ICollection)_dictionary).SyncRoot;

        #endregion
        
        #region IReadOnlyDictionary

        
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _dictionary.Values;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _dictionary.Keys;

        #endregion
    }
}