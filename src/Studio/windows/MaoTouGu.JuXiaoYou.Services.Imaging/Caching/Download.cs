namespace MaoTouGu.JuXiaoYou.Services.Imaging.Caching
{
    internal class Download
    {
        
        public required string Id     { get; init; }
        public required string Dir    { get; init; }
        public required string Output { get; init; }

        public required TaskCompletionSource File { get; init; }
    }
}