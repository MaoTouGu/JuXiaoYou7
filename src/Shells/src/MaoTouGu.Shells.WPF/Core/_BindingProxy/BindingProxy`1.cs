using System.Windows;

namespace MaoTouGu.Shells
{
    public abstract class BindingProxy<T> : Freezable
    {
        public static readonly DependencyProperty ViewModelProperty
            = DependencyProperty.Register(
                                          nameof(ViewModel),
                                          typeof(T),
                                          typeof(BindingProxy<T>),
                                          new PropertyMetadata(default(T)));

        public T ViewModel
        {
            get => (T)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}