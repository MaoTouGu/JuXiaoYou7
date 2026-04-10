// ----------------------------------------------------------
//            文件：QuickModeHelper.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 00:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Pages;

namespace MaoTouGu.JuXiaoYou.Common.Helpers
{
    public class QuickModeHelper
    {
        public static void Work(StartupViewModel context)
        {
            var dp = GlobalSettings.ProjectSettings
                                   .DefaultProject;

            if (string.IsNullOrEmpty(dp))
            {
                //
                // 没有默认项目。
                return;
            }
        }
    }
}