namespace MaoTouGu.Foundation
{
    public sealed class Result
    {
        public string    Reason     { get; init; }
        public Exception Exception  { get; init; }
        public bool      IsFinished { get; init; }

        public static readonly Result Failure = new Result
        {
            IsFinished = false,
        };

        public static Result Failed(string reason) => new Result
        {
            IsFinished = false,
            Reason     = reason,
        };

        public static Result Failed(Exception reason) => new Result
        {
            IsFinished = false,
            Exception  = reason,
        };

        public static Result Success() => new Result
        {
            IsFinished = true,
        };
        public static Result Success(string reason) => new Result
        {
            IsFinished = true,
            Reason     = reason,
        };
    }
}