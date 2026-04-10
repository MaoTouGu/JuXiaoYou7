namespace MaoTouGu.Shells.Core
{
    public interface IViewLocator
    {
        object GetView(ViewModelBase target);
        

        
        int Count { get; }
    }
}