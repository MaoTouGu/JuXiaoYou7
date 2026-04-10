namespace MaoTouGu.Shells.Languages
{
    public interface ILanguageProvider : IDisposable
    {
        void Provide(IDictionary<string, string> dictionary);
    }
}