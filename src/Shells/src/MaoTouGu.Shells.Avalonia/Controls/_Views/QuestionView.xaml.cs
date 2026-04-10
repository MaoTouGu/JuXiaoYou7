

namespace MaoTouGu.Shells.Controls
{
    [Associate(View = typeof(QuestionView), ViewModel = typeof(QuestionRoot))]
    public partial class QuestionView : ForestDialog
    {
        public QuestionView()
        {
            InitializeComponent();
        }
    }
}