
namespace MaoTouGu.Shells.Core
{
    public abstract class ObjectRoot<T> : DialogBase
    {
        internal readonly TaskCompletionSource<Result<T>> _TCSource;

        protected ObjectRoot()
        {
            _TCSource       = new TaskCompletionSource<Result<T>>();
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        protected sealed override void OnCancel()
        {
            _TCSource.SetResult(Result<T>.Failure);
        }

        protected sealed override void Finish()
        {
            try
            {
                if (CanFinish())
                {
                    Result = Result<T>.Success(OnFinish(IsEditing));
                }
                else
                {
                    Result = Result<T>.Failure;
                }
            }
            catch(Exception ex)
            {
                Result = Result<T>.Failed(ex);
            }

            _TCSource.SetResult(Result);
        }

        protected abstract T OnFinish(bool edit);


        public Result<T> Result    { get; protected set; }
        public bool      IsEditing { get; protected set; }

        public Task<Result<T>> Awaitable => _TCSource.Task;
    }
}