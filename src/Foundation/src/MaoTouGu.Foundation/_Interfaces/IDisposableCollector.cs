namespace MaoTouGu.Foundation
{
    public interface IDisposableCollector
    {
        void Dispose();
        void Collect(IDisposable disposable);
    }
}