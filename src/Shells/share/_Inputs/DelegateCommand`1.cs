namespace MaoTouGu.Shells.Inputs
{
    public sealed class DelegateCommand<T> : _Command
    {
        static bool IsNotNull(T obj) => obj is not null;
        
        public DelegateCommand(Action<T> executeHandler) : this(executeHandler, IsNotNull)
        {
            ExecuteHandler = executeHandler ?? throw new ArgumentNullException(nameof(executeHandler));
        }


        public DelegateCommand(Action<T> executeHandler, Predicate<T> canExecuteHandler)
        {
            ExecuteHandler    = executeHandler    ?? throw new ArgumentNullException(nameof(executeHandler));
            CanExecuteHandler = canExecuteHandler ?? throw new ArgumentNullException(nameof(canExecuteHandler));
        }


        public override bool CanExecute(object parameter) => CanExecuteHandler?.Invoke((T)parameter) ?? false;

        public override void Execute(object parameter)
        {

            var v = parameter is T p ? p : default(T);
            ExecuteHandler(v);
        }

        public Action<T>    ExecuteHandler    { get; }
        public Predicate<T> CanExecuteHandler { get; }
    }
}