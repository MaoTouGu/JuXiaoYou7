namespace MaoTouGu.Shells.Core
{
    public interface IAppModelInitialized
    {
        void Attach(Window window);
        void Attach(Window window, DialogHost host);
        void Attach(Window window, ContentHost host);

        void Detach(Window window);
    }
}