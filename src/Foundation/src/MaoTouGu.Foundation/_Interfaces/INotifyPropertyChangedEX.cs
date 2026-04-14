using System.ComponentModel;

namespace MaoTouGu.Foundation
{
    public interface INotifyPropertyChangedEX : INotifyPropertyChanged
    {
        void RaisePropertyChanged(string name);
    }
}