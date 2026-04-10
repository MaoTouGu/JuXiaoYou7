namespace MaoTouGu.JuXiaoYou.Domain.Geography.Pages
{

    [Associate(View = typeof(GeometryPrototypingView), ViewModel = typeof(GeometryPrototypingViewModel))]
    public partial class GeometryPrototypingView : ForestPage
    {
        public GeometryPrototypingView()
        {
            InitializeComponent();
        }
    }
}