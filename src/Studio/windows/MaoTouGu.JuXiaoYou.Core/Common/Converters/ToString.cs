// ----------------------------------------------------------
//            文件：ToString.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 21:20
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Common.Converters
{
    public class ToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value?.ToString();
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value?.ToString();
    }
}