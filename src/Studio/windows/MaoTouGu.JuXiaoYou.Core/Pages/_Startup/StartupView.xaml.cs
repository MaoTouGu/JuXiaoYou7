namespace MaoTouGu.JuXiaoYou.Pages
{

    [Associate(View = typeof(StartupView), ViewModel = typeof(StartupViewModel))]
    public partial class StartupView : ForestPage
    {
        public StartupView()
        {
            InitializeComponent();
        }

        // protected override FlyoutObject BuildFlyoutObject(string hint)
        // {
        //     if (hint == "Tutorial")
        //     {
        //         return new FlyoutObject
        //         {
        //             Title     = "test",
        //             Placement = Placement.Left,
        //             Content   = hint,
        //         };
        //     }
        //     return base.BuildFlyoutObject(hint);
        // }
    }
}