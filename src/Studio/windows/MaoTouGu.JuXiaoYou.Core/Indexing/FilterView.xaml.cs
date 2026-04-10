namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(FilterView), ViewModel = typeof(FilterViewModel))]
    public partial class FilterView : ForestPage
    {
        public FilterView()
        {
            InitializeComponent();
        }

        private void Menu_Quick(object sender, RoutedEventArgs e)
        {
            var dc = ViewModel<FilterViewModel>();

            if (dc.Moniker is null)
            {
                return;
            }

            ((WorkspaceViewModel)dc.Parent).Open(new MonikerTransitViewModel(dc.Moniker, dc.Parent));
        }
        
        private async void Menu_AddSetting(object sender, RoutedEventArgs e)
        {
            var dc = ViewModel<FilterViewModel>();

            if (dc.Moniker is null)
            {
                return;
            }
            var r1 = await dc.SingleLine("Key", "Key");

            if (!r1.IsFinished)
            {
                return;
            }

            var r2 = await dc.SingleLine("Value", "Value");

            if (!r2.IsFinished)
            {
                return;
            }

            var settings = dc.Moniker.Settings;

            settings[r1.Value] = r2.Value;
        }
        
        private void Menu_AddGravatar(object sender, RoutedEventArgs e)
        {
            var dc = ViewModel<FilterViewModel>();

            dc.SetGravatar.Execute(dc.Moniker);
        }
    }
}