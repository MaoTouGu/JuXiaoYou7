// ----------------------------------------------------------
//            文件：NullToBooleanConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月11日 23:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Converters
{
    public class NullToBooleanConverter(bool nullWasTrue) : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (nullWasTrue)
            {
                return Boxing.Box(value is null);
            }

            return Boxing.Box(value is not null);
        }
    }
}