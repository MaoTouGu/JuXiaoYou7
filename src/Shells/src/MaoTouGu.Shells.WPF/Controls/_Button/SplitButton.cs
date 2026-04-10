using Button = System.Windows.Controls.Button;
using ComboBox = System.Windows.Controls.ComboBox;

namespace MaoTouGu.Shells.Controls
{
    public class SplitButton : ComboBox, ICommandSource
    {

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                                        nameof(Command),
                                        typeof(ICommand),
                                        typeof(SplitButton),
                                        new PropertyMetadata(default(ICommand)));


        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                                        nameof(CommandParameter),
                                        typeof(object),
                                        typeof(SplitButton),
                                        new PropertyMetadata(default(object)));


        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(
                                        nameof(CommandTarget),
                                        typeof(IInputElement),
                                        typeof(SplitButton),
                                        new PropertyMetadata(default(IInputElement)));
        
        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton),
                                                     new FrameworkPropertyMetadata(typeof(SplitButton)));
        }
        
        public IInputElement CommandTarget
        {
            get => (IInputElement)GetValue(CommandTargetProperty);
            set => SetValue(CommandTargetProperty, value);
        }

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}