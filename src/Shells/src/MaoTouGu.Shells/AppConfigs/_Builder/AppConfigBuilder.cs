using System.Reflection;
using DryIoc;
using MaoTouGu.Foundation;
using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.AppConfigs
{
    sealed class AppConfigBuilder : IAppConfigBuilder
    {
        private readonly AppConfig _config = new AppConfig();


        internal void SetSetting(object setting)
        {
            _config.Setting = setting;

            Ioc.Container.RegisterInstance(setting.GetType(), setting, IfAlreadyRegistered.Replace);
        }

        internal void SetSettingFileName(string path)
        {
            _config.SettingFileName = path;
        }

        internal void SetLCID(string lcid)
        {
            _config.LCID = lcid;
        }

        internal void SetLanguages(List<ILanguageProvider> collection)
        {
            _config.Languages = collection;
        }


        public IAppConfigBuilder UseViews(params Assembly[] assemblies)
        {
            var vs = Ioc.Get<IViewAmbient>();

            if (assemblies is null)
            {
                return this;
            }

            foreach (var assembly in assemblies)
            {
                if (assembly is null)
                {
                    continue;
                }

                var impls = ClassStatic.FindInterfaceImplementations<IViewBundleStateProvider>(assembly)
                                       .Select(Activator.CreateInstance)
                                       .Cast<IViewBundleStateProvider>();

                foreach (var impl in impls.SelectMany(x => x.Provide()))
                {
                    vs.InstallView(impl);
                }
            }

            return this;
        }

        public IAppConfigBuilder UseViews(params IViewBundleStateProvider[] providers)
        {

            if (providers is null)
            {
                return this;
            }

            var vs = Ioc.Get<IViewAmbient>();

            foreach (var impl in providers.SelectMany(x => x.Provide()))
            {
                vs.InstallView(impl);
            }

            return this;
        }

        /// <summary>
        /// 配置应用主题首选项。
        /// </summary>
        /// <param name="theme">要设置的应用主题。</param>
        public IViewConfigBuilder UseTheme(AppTheme theme)
        {
            _config.Theme = theme;
            return this;
        }

        /// <summary>
        /// 配置默认的目录结构。
        /// </summary>
        /// <remarks>默认为程序根目录。</remarks>
        public ISettingConfigBuilder UseDefaultDir()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            _config.DirOfLogs     = DirectoryExt.GetOrCreate(path);
            _config.DirOfSettings = DirectoryExt.GetOrCreate(path);
            return new SettingConfigBuilderImpl(this);
        }

        /// <summary>
        /// 配置日志所在的目录。
        /// </summary>
        public IAppConfigBuilder UseLogsDir(string path)
        {
            _config.DirOfLogs = DirectoryExt.GetOrCreate(path);
            return this;
        }

        /// <summary>
        /// 配置设置文件所在的目录。
        /// </summary>
        public ISettingConfigBuilder UseSettingDir(string path)
        {
            _config.DirOfSettings = DirectoryExt.GetOrCreate(path);
            return new SettingConfigBuilderImpl(this);
        }

        /// <summary>
        /// 完成应用的基础配置。
        /// </summary>
        /// <param name="appModel">用于管理当前应用程序生命周期的App模型。</param>
        /// <returns>返回一个<see cref="IAppConfig"/>对象实例，用于完成配置。</returns>
        public AppConfig Build(IAppModel appModel)
        {
            _config.Finish();

            Ioc.Use<IAppConfig, AppConfig>(_config);
            Ioc.Use(appModel);

            return _config;
        }

        internal string    DirOfLogs     => _config.DirOfLogs;
        internal string    DirOfSettings => _config.DirOfSettings;
        internal AppConfig AppConfig     => _config;

        public object Setting => _config.Setting;
    }
}