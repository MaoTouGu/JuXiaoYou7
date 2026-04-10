using MaoTouGu.Foundation;
using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.AppConfigs
{
    sealed class SettingProviderImpl<T>(AppConfigBuilder _builder) : ISettingProvider<T> where T : class, new()
    {
        public void FromJson(string text)
        {
            var r = JSON.Deserialize<T>(text);

            _builder.SetSetting(r);
            _builder.SetSettingFileName(null);
        }

        public void FromFile(string path, Func<T> factory)
         {
            var path2 = Path.Combine(_builder.DirOfSettings, path);
            T   instance;

            if (File.Exists(path2))
            {
                instance = JSON.FromFile<T>(path2);
            }
            else
            {
                instance = factory() ?? new T();
                JSON.ToFile(path2, instance);
            }

            _builder.SetSetting(instance);
            _builder.SetSettingFileName(path);
        }

        public void Use(T instance)
        {
            _builder.SetSetting(instance);
            _builder.SetSettingFileName(null);
        }
    }
}