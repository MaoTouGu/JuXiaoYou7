namespace MaoTouGu.JuXiaoYou.AppModels
{
    public sealed class AppSettings
    {
        public const string FileName            = "JuXiaoYou-V7.Json";
        public const string FileName_Project    = "JuXiaoYou-V7-Project.Json";
        public const string FileName_Credential = "JuXiaoYou-V7-Credentials.Json";
        public const string FileName_Server     = "JuXiaoYou-V7-Server.Json";
        public const string FileName_Flyout     = "JuXiaoYou-V7-Flyout.Json";
        public const string FileName_Design     = "JuXiaoYou-Design.Json";
        
        public static void FromFile(ISettingProvider<AppSettings> provider)
        {
            provider.FromFile(FileName, () => new AppSettings
            {
                LCID           = "zh-CN",
                Theme          = (int)AppTheme.Dark,
                StartupRouting = StartupRouting.Prototype,
            });
        }

        //
        // Application 部分的基础设置。
        #region Basic Settings

        public string LCID  { get; set; }
        public string Url   { get; set; }
        public int    Theme { get; set; }

        #endregion

        //
        // 在线模式 部分设置。
        #region OnlineMode Settings

        public string DataDumpDir  { get; set; }
        public string CrashDumpDir { get; set; }
        public string CacheDir     { get; set; }
        public string DownloadDir  { get; set; }

        #endregion

        //
        // 本地模式 部分设置。

        public StartupRouting StartupRouting { get; set; }
    }
}