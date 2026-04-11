// ----------------------------------------------------------
//            文件：ICollection.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 22:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Foundation.Collections
{
    partial class ViewTable3<TKey, TValue>
    {
        
        // void ICollection.CopyTo(Array array, int index)
        // {
        //     ((ICollection)_dictionary).CopyTo(array, index);
        // }
        //
        // int ICollection.Count => _dictionary.Count;
        //
        // bool ICollection.IsSynchronized => false;
        //
        // object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;
        //
        //
        //
        // #region ICollection<KeyValuePair<TKey,TValue>> Members
        //
        // void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        // {
        //     TryAddWithNotification(item);
        // }
        //
        // void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        // {
        //     ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Clear();
        //     NotifyObserversOfChange();
        // }
        //
        // bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        // {
        //     return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);
        // }
        //
        // void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        // {
        //     ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
        // }
        //
        // int ICollection<KeyValuePair<TKey, TValue>>.Count
        // {
        //     get
        //     {
        //         return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Count;
        //     }
        // }
        //
        // bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        // {
        //     get
        //     {
        //         return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).IsReadOnly;
        //     }
        // }
        //
        // bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        // {
        //     TValue temp;
        //     return TryRemoveWithNotification(item.Key, out temp);
        // }
        //
        // #endregion
    }
}