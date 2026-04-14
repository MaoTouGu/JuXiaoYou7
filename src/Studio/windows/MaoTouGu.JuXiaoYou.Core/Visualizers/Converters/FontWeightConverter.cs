// ----------------------------------------------------------
//            文件：FontWeightConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 12:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Commons
{
    public class FontWeightConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value is int i ? i : 0;

            return v switch
            {
                1 => FontWeights.Light,
                2 => FontWeights.Normal,
                3 => FontWeights.Bold,
                4 => FontWeights.Black,
                _ => FontWeights.Thin,
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not FontWeight w)
            {
                return 0;
            }

            if (w == FontWeights.Thin)
            {
                return 1;
            }
            if (w == FontWeights.Normal)
            {
                return 2;
            }
            if (w == FontWeights.Bold)
            {
                return 3;
            }
            if (w == FontWeights.Black)
            {
                return 4;
            }

            return 0;
        }


    }
}