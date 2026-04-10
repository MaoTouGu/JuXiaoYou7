namespace MaoTouGu.Shells.Core
{
    public interface IDialogService
    {
        void OpenDialog(DialogBase target, IAppModel appModel);
        void CloseDialog(DialogBase target);
    }
}