// ----------------------------------------------------------
//            文件：GlobalSettings.AppMode.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.JuXiaoYou
{
    partial class GlobalSettings
    {
        public static void SetArgs(object[] args)
        {
            if (args is null || args.Length <= 0)
            {
                AppMode = AppMode.None;
                return;
            }

            var maybeAppMode = args[0]?.ToString();

            if (string.IsNullOrEmpty(maybeAppMode))
            {
                Args    = args;
                AppMode = AppMode.None;
                return;
            }

            if (Enum.TryParse(typeof(AppMode), maybeAppMode, true, out var mode))
            {
                AppMode = (AppMode)mode;
                Args    = args;
            }
        }

        public static object[] Args    { get; private set; }
        public static AppMode  AppMode { get; private set; }
    }
}