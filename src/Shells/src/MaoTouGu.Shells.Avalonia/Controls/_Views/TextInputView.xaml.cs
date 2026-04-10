using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace MaoTouGu.Shells.Controls
{

    [Associate(View = typeof(TextInputView), ViewModel = typeof(TextInputRoot))]
    public partial class TextInputView : ForestDialog
    {
        public TextInputView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }
        
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not TextInputRoot ir)
            {
                return;
            }

            //
            // 允许多行
            if (ir.IsMultiline)
            {
                Input.MinHeight     = 100;
                Input.AcceptsReturn = true;
                Input.AcceptsTab    = true;
            }
            else
            {
                Input.AcceptsReturn = false;
                Input.AcceptsTab    = false;
            }
        }

        private void Input_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key != Key.Enter)
            {
                return;
            }

            if (DataContext is not TextInputRoot ir)
            {
                return;
            }

            if (ir.IsMultiline)
            {
                return;
            }

            ir.Text = Input.Text;
            ir.CompleteCommand.Execute(null);
        }
    }
}