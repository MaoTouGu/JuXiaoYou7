namespace MaoTouGu.Shells.AppConfigs
{
    public interface IAppConfigBuilder<T> : IViewConfigBuilder, IThemeConfigBuilder<T> where T : class, new()
    {
        /// <summary>
        /// 配置语言选项。
        /// </summary>
        /// <param name="lcid">区域识别码。</param>
        /// <param name="callback">区域识别码。</param>
        /// <returns>返回一个<see cref="ILanguageOptionBuilder"/>对象实例。</returns>
        IAppConfigBuilder<T> UseLanguageOptions(string lcid, Action<ILanguageOptionBuilder> callback);

        /// <summary>
        /// 配置语言选项。
        /// </summary>
        /// <param name="callback">区域识别码。</param>
        /// <returns>返回一个<see cref="ILanguageOptionBuilder"/>对象实例。</returns>
        IAppConfigBuilder<T> UseLanguageOptions(Action<T, ILanguageOptionBuilder> callback);


        /// <summary>
        /// 完成应用的基础配置。
        /// </summary>
        /// <param name="appModel">用于管理当前应用程序生命周期的App模型。</param>
        /// <returns>返回一个<see cref="IAppConfig"/>对象实例，用于完成配置。</returns>
        AppConfig Build(IAppModel appModel);
    }
}