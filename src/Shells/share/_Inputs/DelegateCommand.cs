namespace MaoTouGu.Shells.Inputs
{
    public sealed class DelegateCommand : _Command
    {
        public DelegateCommand(Action executeHandler)
        {
            ExecuteHandler = executeHandler ?? throw new ArgumentNullException(nameof(executeHandler));
        }

        public DelegateCommand(Action executeHandler, Func<bool> canExecuteHandler)
        {
            ExecuteHandler    = executeHandler    ?? throw new ArgumentNullException(nameof(executeHandler));
            CanExecuteHandler = canExecuteHandler ?? throw new ArgumentNullException(nameof(canExecuteHandler));

        }


        public override bool CanExecute(object parameter) => CanExecuteHandler?.Invoke() ?? true;

        public override void Execute(object parameter)
        {
            ExecuteHandler();
        }

        public Action     ExecuteHandler    { get; }
        public Func<bool> CanExecuteHandler { get; }
    }
}