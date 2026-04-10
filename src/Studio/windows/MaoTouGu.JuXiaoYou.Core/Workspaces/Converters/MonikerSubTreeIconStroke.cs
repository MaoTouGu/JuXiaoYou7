// ----------------------------------------------------------
//            文件：MonikerSubTreeIconStroke.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月07日 16:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public class MonikerSubTreeIconStroke : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // if (value is MonikerSubTreeIntent intent)
            // {
            //     return intent switch
            //     {
            //         MonikerSubTreeIntent.Deleted  => Xaml.FindResource("Brush.Error300"),
            //         MonikerSubTreeIntent.Favorite => Xaml.FindResource("Brush.HighlightB4"),
            //         _                             => Xaml.FindResource("Brush.Success300"),
            //     };
            // }

            return null;
        }

        public static MonikerSubTreeIconStroke Instance { get; } = new MonikerSubTreeIconStroke();

    }
}