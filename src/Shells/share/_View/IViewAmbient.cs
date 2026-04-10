namespace MaoTouGu.Shells.Core
{
    public interface IViewAmbient2 : IViewAmbient
    {

        public void InstallView<TView, TViewModel>() where TView : FrameworkElement where TViewModel : ViewModelBase
        {
            InstallView(typeof(TView), typeof(TViewModel));
        }
    }
}