using System.Windows.Input;

namespace MaoTouGu.Shells.Inputs
{
    public interface ICommandEX : ICommand
    {
        void RaiseUpdate();
    }
}