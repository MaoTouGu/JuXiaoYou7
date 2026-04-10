// ----------------------------------------------------------
//            文件：DefaultProjectConverter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 02:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class DefaultProjectConverter : OneWayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var setting = GlobalSettings.ProjectSettings;
            
            
            if (string.IsNullOrEmpty(setting.DefaultProject) || 
                value is not Project project)
            {
                return Boxing.False;
            }

            return setting.DefaultProject == project.Id;
        }
    }
}