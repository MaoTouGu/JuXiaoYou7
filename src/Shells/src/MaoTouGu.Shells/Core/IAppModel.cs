using MaoTouGu.Foundation;

namespace MaoTouGu.Shells.Core
{
    /// <summary>
    /// <see cref="IAppModel"/> 接口用于表示一个应用模型。
    /// </summary>
    public interface IAppModel : ILifetime, IFlyoutAmbient, IWorkspaceAmbient
    {
        Task<bool> Navigate(PageBase page);
        Task<bool> Navigate(PageBase page, params object[] args);
        Task<bool> Navigate<T>() where T : PageBase;
        Task<bool> Navigate<T>(params object[] args) where T : PageBase;
        
        IBusyStateRecipient GetBusyStateRecipient(ViewModelBase target);

        void Notify(Notification notification);
        
        IDialogService GetDialogHost(ViewModelBase target);
        
        IFlyoutService GetFlyoutService(ViewModelBase target);
    }
}