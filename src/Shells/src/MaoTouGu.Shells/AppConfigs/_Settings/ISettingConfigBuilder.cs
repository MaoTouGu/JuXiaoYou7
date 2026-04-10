namespace MaoTouGu.Shells.AppConfigs
{
    public interface ISettingConfigBuilder
    {
        /// <summary>
        /// 使用指定的应用设置配置方案。
        /// </summary>
        IAppConfigBuilder<T> UseSetting<T>(Action<ISettingProvider<T>> callback) where T : class, new();
        
        /// <summary>
        /// 使用指定的应用设置配置方案。
        /// </summary>
        IAppConfigBuilder<T> UseSetting<T>(T setting) where T : class, new();

        /// <summary>
        /// 使用默认的应用设置配置方案。
        /// </summary>
        IAppConfigBuilder UseDefaultSetting();
    }
}