// ----------------------------------------------------------
//            文件：RemoveExt.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月26日 17:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Foundation.Collections
{
    public static class RemoveExt
    {
        /// <summary>
        /// </summary>
        /// <param name="collection">指定目标集合</param>
        /// <param name="expression">要添加的数据源</param>
        /// <typeparam name="T">具体的数据类型</typeparam>
        public static T Remove<T>(this IList<T> collection, Predicate<T> expression)
        {
            if (collection is null ||
                expression is null)
            {
                return default;
            }


            for (var i = 0; i < collection.Count;)
            {
                var item = collection[i];

                if (expression(item))
                {
                    collection.RemoveAt(i);
                    return item;
                }


                i++;
            }


            return default;
        }

        public static T Remove<T, R1>(this IList<T> collection) where R1 : T
        {
            if (collection is null)
            {
                return default;
            }

            return Remove<T>(collection, Expression);
            bool Expression(T x) => x is R1;
        }

        public static T Remove<T, R1, R2>(this IList<T> collection)
            where R1 : T
            where R2 : T
        {
            if (collection is null)
            {
                return default;
            }

            return Remove<T>(collection, Expression);
            bool Expression(T x) => x is R1 or R2;
        }

        public static T Remove<T, R1, R2, R3>(this IList<T> collection)
            where R1 : T
            where R2 : T
            where R3 : T
        {
            if (collection is null)
            {
                return default;
            }

            return Remove<T>(collection, Expression);

            bool Expression(T x) => x is R1 or R2 or R3;
        }

        public static T Remove<T, R1, R2, R3, R4>(this IList<T> collection)
            where R1 : T
            where R2 : T
            where R3 : T
            where R4 : T
        {
            if (collection is null)
            {
                return default;
            }
            return Remove<T>(collection, Expression);

            bool Expression(T x) => x is R1 or R2 or R3 or R4;
        }

        public static T Remove<T, R1, R2, R3, R4, R5>(this IList<T> collection)
            where R1 : T
            where R2 : T
            where R3 : T
            where R4 : T
            where R5 : T
        {
            if (collection is null)
            {
                return default;
            }

            return Remove<T>(collection, Expression);

            bool Expression(T x) => x is R1 or R2 or R3 or R4 or R5;
        }
    }
}