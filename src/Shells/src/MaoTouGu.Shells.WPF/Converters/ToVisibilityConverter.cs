// ----------------------------------------------------------
//            文件：ToVisibilityConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月29日 15:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Converters
{
    public class ToVisibilityConverter(Visibility trueVal, Visibility falseVal) : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value is bool n && n;
            return b ? trueVal : falseVal;
        }
    }
}