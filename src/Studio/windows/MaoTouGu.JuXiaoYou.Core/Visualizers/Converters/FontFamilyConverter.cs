// ----------------------------------------------------------
//            文件：FontFamilyConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class FontFamilyConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new FontFamily(value?.ToString());
        }
    }
}