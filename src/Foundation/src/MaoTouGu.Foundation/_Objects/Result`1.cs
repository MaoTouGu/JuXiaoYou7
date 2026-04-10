namespace MaoTouGu.Foundation
{
    public sealed class Result<T>
    {
        public string    Reason     { get; init; }
        public Exception Exception  { get; init; }
        public bool      IsFinished { get; init; }
        public T         Value      { get; init; }

        public static readonly Result<T> Failure = new Result<T>
        {
            IsFinished = false,
        };

        public static Result<T> Failed(string reason) => new Result<T>
        {
            IsFinished = false,
            Reason     = reason,
        };

        public static Result<T> Failed(Exception reason) => new Result<T>
        {
            IsFinished = false,
            Exception  = reason,
        };
        
        public static Result<T> Success(T val) => new Result<T>
        {
            IsFinished = true,
            Value      = val,
        };
        public static Result<T> Success(T val, string reason) => new Result<T>
        {
            IsFinished = true,
            Value      = val,
            Reason     = reason,
        };
    }
}