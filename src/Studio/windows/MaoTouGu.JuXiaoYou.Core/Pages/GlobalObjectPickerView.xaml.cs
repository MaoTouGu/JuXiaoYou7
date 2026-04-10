namespace MaoTouGu.JuXiaoYou.Pages
{

    [Associate(View = typeof(GlobalObjectPickerView), ViewModel = typeof(GlobalObjectPicker<>))]
    public partial class GlobalObjectPickerView : ForestDialog
    {
        public GlobalObjectPickerView()
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            Items.ItemTemplate  = DataTemplateBuilder.BuildTextBlockTemplate(((IGlobalObjectPicker)DataContext).PropertyName, false);
            Items.SelectedIndex = 0;
        }
    }
}