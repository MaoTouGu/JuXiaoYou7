// ----------------------------------------------------------
//            文件：WithStarPresenter.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public partial class WithStarPresenter : UserControl
    {
        public WithStarPresenter()
        {
            InitializeComponent();
        }
    }

    public sealed class NumericSettingConverter : OneWayConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return int.TryParse(str, out var n) ? n : 0;
            }

            return 0;
        }

        public static NumericSettingConverter Instance { get; } = new NumericSettingConverter();
    }
}