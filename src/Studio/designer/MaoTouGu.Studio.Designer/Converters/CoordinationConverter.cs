// ----------------------------------------------------------
//            文件：CoordinationConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 12:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Globalization;
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Converters
{
    public class CoordinationConverter : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value is double d ? d : 0d;

            return (Math.Floor(v / 20d)) * 20;
        }

        public static CoordinationConverter Instance { get; } = new CoordinationConverter();
    }
}