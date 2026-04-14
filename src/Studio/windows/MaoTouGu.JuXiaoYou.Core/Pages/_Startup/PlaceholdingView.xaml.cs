namespace MaoTouGu.JuXiaoYou.Pages
{

    [Associate(View = typeof(PlaceholdingView), ViewModel = typeof(PlaceholdingViewModel))]
    public partial class PlaceholdingView : ForestPage
    {
        public PlaceholdingView()
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (Ioc.Get<IAppModel>() is IShellBase shell)
            {
                shell.Startup();
            }
        }
    }
}