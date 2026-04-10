using System.Collections.ObjectModel;

namespace MaoTouGu.Foundation.Collections
{
    public static class ForEachExt
    {
        
        //-------------------------------------------------------------
        //
        //          ForEach
        //
        //-------------------------------------------------------------


        #region ForEach

        
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> handler)
        {
            if (collection is null)
            {
                return;
            }

            if (handler is null)
            {
                return;
            }

            foreach (var item in collection)
            {
                handler(item);
            }
        }
        
        public static void ForEach<T>(this IReadOnlyList<T> collection, Action<T, int> handler)
        {
            if (collection is null)
            {
                return;
            }

            if (handler is null)
            {
                return;
            }

            for (var i =0; i< collection.Count;i++)
            {
                var item = collection[i];
                handler(item, i);
            }
        }

        #endregion

        
        public static void Clear<T>(this Collection<T> collection, Action<T> handler)
        {
            if (collection is null)
            {
                return;
            }

            if (handler is null)
            {
                return;
            }

            for (var i = 0; i < collection.Count;)
            {
                var item = collection[i];

                handler(item);
                collection.RemoveAt(i);
            }
        }
        
        public static void Clear<T>(this List<T> collection, Action<T> handler)
        {
            if (collection is null)
            {
                return;
            }

            if (handler is null)
            {
                return;
            }

            for (var i = 0; i < collection.Count;)
            {
                var item = collection[i];

                handler(item);
                collection.RemoveAt(i);
            }
        }
        
        public static void Clear<T>(this IList<T> collection, Action<T> handler)
        {
            if (collection is null)
            {
                return;
            }

            if (handler is null)
            {
                return;
            }

            for (var i = 0; i < collection.Count;)
            {
                var item = collection[i];

                handler(item);
                collection.RemoveAt(i);
            }
        }
        
        public static void Clear<T>(this ViewList<T> collection, Action<T> handler)
        {
            if (collection is null)
            {
                return;
            }

            if (handler is null)
            {
                return;
            }

            for (var i = 0; i < collection.Count;)
            {
                var item = collection[i];

                handler(item);
                collection.RemoveAt(i);
            }
        }

    }
}