namespace MaoTouGu.Shells.Inputs
{
    
    public sealed class NavigationCommand<T>(ViewModelBase target, bool closeThis = false) : _Command where T : PageBase
    {
        public override async void Execute(object parameter)
        {
            await target.Navigate<T>();

            if (closeThis)
            {
                target.Stop();
            }
        }
    }
}