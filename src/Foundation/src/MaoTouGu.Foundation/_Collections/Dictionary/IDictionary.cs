// ----------------------------------------------------------
//            文件：IDictionary.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 22:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Foundation.Collections
{
    partial class ViewTable3<TKey, TValue>
    {
        

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

        int ICollection.Count => _dictionary.Count;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => ((ICollection)_dictionary).SyncRoot;

    }
}