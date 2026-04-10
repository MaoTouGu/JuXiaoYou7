using System.Windows;

namespace MaoTouGu.Shells
{
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }


        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                                          nameof(ViewModel),
                                          typeof(object),
                                          typeof(BindingProxy),
                                          new PropertyMetadata(default(object)));

        public object ViewModel
        {
            get => GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }

}