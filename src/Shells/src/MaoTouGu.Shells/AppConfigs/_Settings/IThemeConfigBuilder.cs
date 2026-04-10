namespace MaoTouGu.Shells.AppConfigs
{
    public interface IThemeConfigBuilder
    {
        /// <summary>
        /// 配置应用主题首选项。
        /// </summary>
        /// <param name="theme">要设置的应用主题。</param>
        IViewConfigBuilder UseTheme(AppTheme theme);
    }
    
    public interface IThemeConfigBuilder<T> where T : class, new()
    {
        /// <summary>
        /// 配置应用主题首选项。
        /// </summary>
        /// <param name="callback">回调。</param>
        IViewConfigBuilder UseTheme(Func<T, AppTheme> callback);
    }
}