namespace MaoTouGu.Foundation
{
    public sealed class WebResult<T>
    {
        public string    Reason     { get; init; }
        public bool      IsFinished { get; init; }
        public T         Value      { get; init; }

        public static readonly WebResult<T> Failure = new WebResult<T>
        {
            IsFinished = false,
        };

        public static WebResult<T> Failed(string reason) => new WebResult<T>
        {
            IsFinished = false,
            Reason     = reason,
        };

        public static WebResult<T> Success(T val) => new WebResult<T>
        {
            IsFinished = true,
            Value      = val,
        };
    }
}