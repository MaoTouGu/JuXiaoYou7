namespace MaoTouGu.Foundation
{
    public static class DisposableExtension
    {
        private class DelegateDisposable : IDisposable
        {
            private readonly Action Handler;
            public DelegateDisposable(Action handler)
            {
                Handler = handler;
            }

            public void Dispose()
            {
                Handler?.Invoke();
            }

        }
        public static T DisposeWith<T>(this T instance, IDisposableCollector collection) where T : class, IDisposable
        {
            if (instance is not null)
            {
                collection.Collect(instance);
            }
    
            return instance;
        }
        
        public static IDisposable Create(Action handler)
        {
            return new DelegateDisposable(handler);
        }
    }
}