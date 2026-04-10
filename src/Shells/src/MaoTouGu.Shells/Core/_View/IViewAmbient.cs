namespace MaoTouGu.Shells.Core
{
    public interface IViewAmbient
    {
        
        void InstallView(ViewBundleState state);

        void InstallView(Type vType, Type vmType);

        void InstallView(IViewBundleStateProvider provider);
    }
}