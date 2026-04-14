namespace MaoTouGu.JuXiaoYou.Indexing
{

    [Associate(View = typeof(CustomFilterView), ViewModel = typeof(CustomFilterViewModel))]
    public partial class CustomFilterView : ForestDialog
    {
        public CustomFilterView()
        {
            InitializeComponent();
        }

        private async void Button_Add(object sender, RoutedEventArgs e)
        {
            var dc = ViewModel<CustomFilterViewModel>();
            var r  = await dc.SingleLine("添加标签", "例如：种族");

            if (!r.IsFinished)
            {
                return;
            }

            ((KeywordIntersectionFilter)dc.Filter).Keywords.Add(r.Value);
            dc.Success("提示", "添加成功");
        }

        private async void Button_Remove(object sender, RoutedEventArgs e)
        {
            var dc = ViewModel<CustomFilterViewModel>();

            if (sender is Button { DataContext: string keyword })
            {

                ((KeywordIntersectionFilter)dc.Filter).Keywords.Add(keyword);
                dc.Success("提示", "添加成功");
            }
        }
    }
}