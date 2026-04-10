namespace MaoTouGu.Shells
{
    public interface IViewBundleStateProvider
    {
        IEnumerable<ViewBundleState> Provide();
    }
}