namespace MaoTouGu.Shells.AppConfigs
{
    public interface IAppConfigBuilder : IViewConfigBuilder, IThemeConfigBuilder
    {
        /// <summary>
        /// 配置默认的目录结构。
        /// </summary>
        /// <remarks>默认为程序根目录。</remarks>
        ISettingConfigBuilder UseDefaultDir();
        
        /// <summary>
        /// 配置日志所在的目录。
        /// </summary>
        IAppConfigBuilder UseLogsDir(string path);
        
        /// <summary>
        /// 配置设置文件所在的目录。
        /// </summary>
        ISettingConfigBuilder UseSettingDir(string path);

        /// <summary>
        /// 完成应用的基础配置。
        /// </summary>
        /// <param name="appModel">用于管理当前应用程序生命周期的App模型。</param>
        /// <returns>返回一个<see cref="IAppConfig"/>对象实例，用于完成配置。</returns>
        AppConfig Build(IAppModel appModel);
    }
}