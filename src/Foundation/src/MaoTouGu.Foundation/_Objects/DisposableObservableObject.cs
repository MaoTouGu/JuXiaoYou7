
namespace MaoTouGu.Foundation
{
    public abstract class DisposableObservableObject : ObservableObject, ICancelable
    {
        protected virtual void ReleaseUnmanagedResources()
        {
        }

        protected virtual void ReleaseManagedResources()
        {
        }

        protected void Dispose(bool disposing)
        {
            ReleaseManagedResources();
            
            if (disposing)
            {
                ReleaseUnmanagedResources();
                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableObservableObject()
        {
            Dispose(false);
        }
        
        
        public bool IsDisposed { get; private set; }
    }
}