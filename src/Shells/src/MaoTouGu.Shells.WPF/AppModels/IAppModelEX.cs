namespace MaoTouGu.Shells.AppModels
{
    public interface IAppModelEX : IAppModel
    {
        void Attach(Window mainWindow, DialogHost dialogHost, ContentHost contentHost);
    }
}