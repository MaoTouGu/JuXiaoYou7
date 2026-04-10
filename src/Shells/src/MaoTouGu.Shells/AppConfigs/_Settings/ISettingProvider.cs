namespace MaoTouGu.Shells.AppConfigs
{
    public interface ISettingProvider<T> where T : class, new()
    {
        void FromJson(string text);
        void FromFile(string path, Func<T> factory);
        void Use(T instance);
    }
}