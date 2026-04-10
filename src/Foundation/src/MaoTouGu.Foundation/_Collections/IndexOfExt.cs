
namespace MaoTouGu.Foundation.Collections
{
    public static class IndexOfExt
    {
        public static int IndexOf<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                return -1;
            }

            if (collection is null)
            {
                return -1;
            }

            var i = 0;
            
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }
        
        public static (int, T) FindAndGet<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (predicate is null || collection is null)
            {
                return new ValueTuple<int, T>(-1, default);
            }

            var i = 0;
            
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return new ValueTuple<int, T>(i, item);
                }

                i++;
            }

            return new ValueTuple<int, T>(-1, default);
        }
    }
}