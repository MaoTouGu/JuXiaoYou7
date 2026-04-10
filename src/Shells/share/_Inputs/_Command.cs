namespace MaoTouGu.Shells.Inputs
{
    public abstract class _Command : ICommandEX
    {

        public virtual bool CanExecute(object parameter) => true;

        public virtual void Execute(object parameter)
        {
        }

#if AVALONIA
        public event EventHandler CanExecuteChanged;
#else
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
#endif

        public void RaiseUpdate()
        {
            
#if AVALONIA
#else
            CommandManager.InvalidateRequerySuggested();
#endif
        }
    }
}