namespace MaoTouGu.Shells.AppConfigs
{
    sealed class SettingConfigBuilderImpl(AppConfigBuilder _builder) : ISettingConfigBuilder
    {
        public IAppConfigBuilder<T> UseSetting<T>(Action<ISettingProvider<T>> callback) where T : class, new()
        {
            var provider = new SettingProviderImpl<T>(_builder);
            
            callback?.Invoke(provider);
            
            return new AppConfigBuilderImpl<T>(_builder);
        }

        public IAppConfigBuilder<T> UseSetting<T>(T setting) where T : class, new()
        {
            
            //
            // 设置AppSetting。
            _builder.SetSetting(setting);
            
            return new AppConfigBuilderImpl<T>(_builder);
        }

        public IAppConfigBuilder UseDefaultSetting()
        {
            _builder.SetSetting(null);
            return _builder;
        }
    }
}