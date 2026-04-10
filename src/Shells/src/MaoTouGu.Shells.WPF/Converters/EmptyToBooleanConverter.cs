// ----------------------------------------------------------
//            文件：EmptyToBooleanConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月11日 23:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Collections;

namespace MaoTouGu.Shells.Converters
{
    public class EmptyToBooleanConverter(bool emptyWasTrue) : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<object, object> expr = emptyWasTrue ? EmptyWasTrue : NotEmptyWasTrue;

            return expr(value);
        }

        static object EmptyWasTrue(object value)
        {
            if (value is not IEnumerable iterator)
            {
                //
                // 不是IEnumerable接口就是Empty，所以为true
                return Boxing.True;
            }

            if (value is ICollection collection)
            {
                return Boxing.Box(collection.Count == 0);
            }

            try
            {
                var       iterator2 = iterator.GetEnumerator();
                using var iterator3 = iterator2 as IDisposable;

                return Boxing.Box(!iterator2.MoveNext());
            }
            catch
            {
                return Boxing.True;
            }
        }
        
        static object NotEmptyWasTrue(object value)
        {
            if (value is not IEnumerable iterator)
            {
                //
                // 不是IEnumerable接口就是Empty，所以为true
                return Boxing.False;
            }

            if (value is ICollection collection)
            {
                return Boxing.Box(collection.Count != 0);
            }

            try
            {
                var       iterator2 = iterator.GetEnumerator();
                using var iterator3 = iterator2 as IDisposable;

                return Boxing.Box(iterator2.MoveNext());
            }
            catch
            {
                return Boxing.False;
            }
        }
    }
}