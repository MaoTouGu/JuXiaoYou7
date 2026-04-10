namespace MaoTouGu.Foundation.Collections
{
    public static class AddManyExt
    {
        //-------------------------------------------------------------
        //
        //          AddMany
        //
        //-------------------------------------------------------------

        /// <summary>
        /// 为集合提供批量添加功能
        /// </summary>
        /// <param name="collection">指定目标集合</param>
        /// <param name="source">要添加的数据源</param>
        /// <param name="clear">是否清除原有的数据</param>
        /// <typeparam name="T">具体的数据类型</typeparam>
        public static void AddMany<T>(this Stack<T> collection, IEnumerable<T> source, bool clear = false)
        {
            if (collection is null ||
                source is null)
            {
                return;
            }

            if (clear)
            {
                collection.Clear();
            }

            foreach (var item in source)
            {
                collection.Push(item);
            }
        }

        /// <summary>
        /// 为集合提供批量添加功能
        /// </summary>
        /// <param name="collection">指定目标集合</param>
        /// <param name="source">要添加的数据源</param>
        /// <param name="clear">是否清除原有的数据</param>
        /// <typeparam name="T">具体的数据类型</typeparam>
        public static void AddMany<T>(this Queue<T> collection, IEnumerable<T> source, bool clear = false)
        {
            if (collection is null ||
                source is null)
            {
                return;
            }

            if (clear)
            {
                collection.Clear();
            }

            foreach (var item in source)
            {
                collection.Enqueue(item);
            }
        }

        /// <summary>
        /// 为集合提供批量添加功能
        /// </summary>
        /// <param name="collection">指定目标集合</param>
        /// <param name="source">要添加的数据源</param>
        /// <param name="clear">是否清除原有的数据</param>
        /// <typeparam name="T">具体的数据类型</typeparam>
        public static void AddMany<T>(this HashSet<T> collection, IEnumerable<T> source, bool clear = false)
        {
            if (collection is null ||
                source is null)
            {
                return;
            }

            if (clear)
            {
                collection.Clear();
            }

            foreach (var item in source)
            {
                collection.Add(item);
            }
        }
        
        /// <summary>
        /// 为集合提供批量添加功能
        /// </summary>
        /// <param name="dictionary">指定目标集合</param>
        /// <param name="source">要添加的数据源</param>
        /// <param name="clear">是否清除原有的数据</param>
        /// <typeparam name="TKey">字典的键的数据类型。</typeparam>
        /// <typeparam name="TValue">字典的值的数据类型</typeparam>
        public static void AddMany<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> source, bool clear = false)
        {
            if (dictionary is null || source is null)
            {
                return;
            }

            if (clear)
            {
                dictionary.Clear();
            }

            IDictionary<TKey, TValue> d = dictionary;

            foreach (var item in source)
            {
                d.Add(item);
            }
        }
        
        
        /// <summary>
        /// 为集合提供批量添加功能
        /// </summary>
        /// <param name="dictionary">指定目标集合</param>
        /// <param name="source">要添加的数据源</param>
        /// <param name="selector">根据数据返回键的选择器。</param>
        /// <param name="clear">是否清除原有的数据</param>
        /// <typeparam name="TKey">字典的键的数据类型。</typeparam>
        /// <typeparam name="TValue">字典的值的数据类型</typeparam>
        public static void AddMany<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, IEnumerable<TValue> source, Func<TValue, TKey> selector, bool clear = false)
        {
            if (dictionary is null || source is null || selector is null)
            {
                return;
            }

            if (clear)
            {
                dictionary.Clear();
            }

            IDictionary<TKey, TValue> d = dictionary;

            foreach (var item in source)
            {
                var key = selector(item);
                
                d.Add(key, item);
            }
        }

        /// <summary>
        /// 为集合提供批量添加功能
        /// </summary>
        /// <param name="collection">指定目标集合</param>
        /// <param name="source">要添加的数据源</param>
        /// <param name="clear">是否清除原有的数据</param>
        /// <typeparam name="T">具体的数据类型</typeparam>
        public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> source, bool clear = false)
        {
            if (collection is null ||
                source is null)
            {
                return;
            }

            if (clear)
            {
                collection.Clear();
            }

            foreach (var item in source)
            {
                collection.Add(item);
            }
        }

    }
}