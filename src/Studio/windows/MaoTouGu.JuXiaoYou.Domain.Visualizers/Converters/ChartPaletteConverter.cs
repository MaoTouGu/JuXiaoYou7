// ----------------------------------------------------------
//            文件：ChartPaletteConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 18:02
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using KinonekoSoftware.UI.Charts;
using MaoTouGu.JuXiaoYou.Visualizers.Blocks;

namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public interface IChartPaletteSource
    {
        string GetPalette();
    }
    
    public class ChartPaletteConverter : OneWayConverter
    {
        private static readonly ChartPalette Default = ChartPalette.Create("#808080", "#808080");

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IChartPaletteSource rv)
            {
                ChartPalette.Create("#808080", rv.GetPalette());
            }

            return Default;
        }

        public static readonly ChartPaletteConverter Instance = new ChartPaletteConverter();
    }
}