namespace MaoTouGu.Shells.AppConfigs
{
    public interface IAppConfig
    {
        string DirOfLogs     { get; }
        string DirOfSettings { get; }


        string LCID { get; }

        IReadOnlyList<ILanguageProvider> Languages { get; set; }
    }
}