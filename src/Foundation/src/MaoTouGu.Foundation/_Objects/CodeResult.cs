namespace MaoTouGu.Foundation
{
    public class CodeResult
    {
        public int  Code       { get; init; }
        public bool IsFinished { get; init; }

        public static CodeResult Failed(int code) => new CodeResult
        {
            IsFinished = false,
            Code       = code,
        };

        public static CodeResult Success(int val) => new CodeResult
        {
            IsFinished = true,
            Code       = val,
        };
    }
}