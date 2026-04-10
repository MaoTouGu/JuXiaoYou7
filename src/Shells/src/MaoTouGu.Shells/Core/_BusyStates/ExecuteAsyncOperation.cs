namespace MaoTouGu.Shells.Core
{
    public class ExecuteAsyncOperation: ObservableAsyncOperation
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
            Expression();
            IsCompleted = true;
        }

        public Action Expression { get; init; }
    }
}