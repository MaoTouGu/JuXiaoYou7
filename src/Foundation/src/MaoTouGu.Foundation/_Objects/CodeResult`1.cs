namespace MaoTouGu.Foundation
{
    public class CodeResult<T>
    {
        public int  Code       { get; init; }
        public bool IsFinished { get; init; }
        public T    Value      { get; init; }

        public static readonly CodeResult<T> Failure = new CodeResult<T>
        {
            IsFinished = false,
        };

        public static CodeResult<T> Failed(int code) => new CodeResult<T>
        {
            IsFinished = false,
            Code       = code,
        };

        public static CodeResult<T> Success(T val) => new CodeResult<T>
        {
            IsFinished = true,
            Value      = val,
        };
    }
}