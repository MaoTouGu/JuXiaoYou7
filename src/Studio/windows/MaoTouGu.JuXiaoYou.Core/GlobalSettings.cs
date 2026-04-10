// ----------------------------------------------------------
//            文件：GlobalSettings.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 18:58
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.JuXiaoYou
{
    public static partial class GlobalSettings
    {
        private static void ConfigDir()
        {
            var config = (AppConfig)Ioc.Get<IAppConfig>();
            var di     = new DirectoryInfo(config.DirOfLogs);
            var parent = di.Parent;

            if (!Directory.Exists(parent?.FullName))
            {
                return;
            }

            CacheDir     = DirectoryExt.Combine(parent.FullName, "Caches");
            DownloadDir  = DirectoryExt.Combine(parent.FullName, "Downloads");
            CrashDumpDir = DirectoryExt.Combine(parent.FullName, "CrashDump");
            DataDumpDir  = DirectoryExt.Combine(parent.FullName, "DataDump");
        }

        private static string GetFileName(string fileName)
        {
            var dir = Ioc.Get<IAppConfig>().DirOfSettings;
            return Path.Combine(dir, fileName);
        }

        public static void Load()
        {
            AppSettings = Ioc.Get<AppSettings>();

            ConfigDir();
            LoadFlyoutSettings();
            LoadProjectSettings();
        }

        public static void Save()
        {
            SaveFlyoutSettings();

        }


        public static string DataDumpDir  { get; private set; }
        public static string CrashDumpDir { get; private set; }
        public static string CacheDir     { get; private set; }
        public static string DownloadDir  { get; private set; }

        private static string FileNameOfProjectSettings => GetFileName(FileName_Server);
        private static string FileNameOfFlyoutSettings  => GetFileName(FileName_Flyout);


        public static string  Url        { get; set; }
        public static User    User       { get; set; }
        public static IWebApi Api        { get; set; }
        public static bool    OnlineMode { get; set; }

        public static string UserID => User?.Id;

        public static AppSettings AppSettings { get; set; }
    }
}