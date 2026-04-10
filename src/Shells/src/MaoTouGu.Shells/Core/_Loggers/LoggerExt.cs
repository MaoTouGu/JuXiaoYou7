namespace MaoTouGu.Foundation.Core
{
    public static class LoggerExt
    {
        public static ILogger GetLogger<T>() => LogManager.GetLogger(typeof(T).Name);
        
        public static ILogger GetLogger<T>(T target) => LogManager.GetLogger(target.GetType().Name);
        
        public static ILogger GetLogger(string target) => LogManager.GetLogger(target);
    }
}