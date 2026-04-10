namespace MaoTouGu.Shells.Inputs
{
    public sealed class NavigationCommand : _Command
    {
        public override void Execute(object parameter)
        {
            if (parameter is not Type type || !type.IsAssignableTo(typeof(PageBase)))
            {
                return;
            }

            var page = Activator.CreateInstance(type) as PageBase;

            if (page is null)
            {
                return;
            }
            
            Ioc.SafeGet<IAppModel>()
              ?.Navigate(page);
        }

        public static NavigationCommand Instance { get; } = new NavigationCommand();
    }
}