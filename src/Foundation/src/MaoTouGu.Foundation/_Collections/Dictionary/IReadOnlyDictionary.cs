// ----------------------------------------------------------
//            文件：IReadOnlyDictionary.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 22:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Foundation.Collections
{
    partial class ViewTable3<TKey, TValue>
    {

        int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => _dictionary.Count;
        
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _dictionary.Values;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _dictionary.Keys;
    }
}