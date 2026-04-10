namespace MaoTouGu.Shells.Core
{
    public interface IWorkspaceAmbient
    {
        
        object GetViewCache(ViewModelBase target);

        void SetWindow(ViewModelBase target, object window);
        void SetViewCache(ViewModelBase target, object view, ViewModelBase parent = null);
        void UnsetViewCache(ViewModelBase target);
    }
}