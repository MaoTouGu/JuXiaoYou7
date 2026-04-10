using System.Reflection;

namespace MaoTouGu.Shells.AppConfigs
{
    class AppConfigBuilderImpl<T>(AppConfigBuilder _builder) : IAppConfigBuilder<T> where T : class, new()
    {
        internal void SetLCID(string lcid) => _builder.SetLCID(lcid);
        
        /// <summary>
        /// 配置语言选项。
        /// </summary>
        /// <param name="lcid">区域识别码。</param>
        /// <param name="callback">区域识别码。</param>
        /// <returns>返回一个<see cref="ILanguageOptionBuilder"/>对象实例。</returns>
        public IAppConfigBuilder<T> UseLanguageOptions(string lcid, Action<ILanguageOptionBuilder> callback)
        {
            var provider   = new LanguageOptionBuilderImpl<T>(this);
            var collection = new List<ILanguageProvider>();
            
            provider.SetLCID(lcid);
            provider.SetCollection(collection);
            _builder.SetLanguages(collection);

            callback?.Invoke(provider);

            return this;
        }
        
        /// <summary>
        /// 配置语言选项。
        /// </summary>
        /// <param name="callback">区域识别码。</param>
        /// <returns>返回一个<see cref="ILanguageOptionBuilder"/>对象实例。</returns>
        public IAppConfigBuilder<T> UseLanguageOptions(Action<T, ILanguageOptionBuilder> callback)
        {
            var provider   = new LanguageOptionBuilderImpl<T>(this);
            var collection = new List<ILanguageProvider>();
            
            //
            //
            provider.SetCollection(collection);
            _builder.SetLanguages(collection);
            callback?.Invoke(_builder.Setting as T, provider);
            
            //
            //
            return this;
        }
        
        /// <summary>
        /// 配置应用主题首选项。
        /// </summary>
        /// <param name="callback">回调。</param>
        public IViewConfigBuilder UseTheme(Func<T, AppTheme> callback)
        {
            var theme = callback?.Invoke(_builder.Setting as T) ?? AppTheme.Dark;

            _builder.UseTheme(theme);
            
            return _builder;
        }


        public IAppConfigBuilder UseViews(params Assembly[] assemblies) => _builder.UseViews(assemblies);
        
        public IAppConfigBuilder UseViews(params IViewBundleStateProvider[] providers) => _builder.UseViews(providers);

        public AppConfig Build(IAppModel appModel) => _builder.Build(appModel);
        
        
        internal object Setting => _builder.Setting;
    }
}