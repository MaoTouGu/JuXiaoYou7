namespace MaoTouGu.Shells.Core
{
    public class ConditionalAsyncOperation : ObservableAsyncOperation
    {
        void Execute()
        {
            CanNext     = Expression();
            IsCompleted = true;
        }
        
        public override Task Run()
        {
            if (Expression is null)
            {
                CanNext     = false;
                IsCompleted = true;
                return Task.Delay(50);
            }

            return Task.Run(Execute);
        }
        
        public bool       CanNext    { get; protected set; }
        public Action     Callback   { get; init; }
        public Func<bool> Expression { get; init; }
    }
}