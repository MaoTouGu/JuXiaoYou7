namespace MaoTouGu.Shells.Core
{
    public class ParameterAsyncOperation : ObservableAsyncOperation
    {
        public override Task Run()
        {
            if (Expression is null)
            {
                IsCompleted = true;
                return Task.Delay(50);
            }

            return Task.Run(Execute);
        }

        void Execute()
        {
            Expression(Parameters);
            IsCompleted = true;
        }

        public Action<List<object>> Expression { get; init; }
        public List<object>         Parameters { get; init; }
    }
}