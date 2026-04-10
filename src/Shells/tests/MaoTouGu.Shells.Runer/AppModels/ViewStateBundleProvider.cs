namespace MaoTouGu.Shells.Runer.AppModels
{
    public sealed class MockupViewModel : PageBase
    {
        
    }
    
    public class ViewStateBundleProvider : IViewBundleStateProvider
    {

        public IEnumerable<ViewBundleState> Provide() => new []
        {
            new ViewBundleState(typeof(MainWindow), typeof(MockupViewModel)),
        };
    }
}