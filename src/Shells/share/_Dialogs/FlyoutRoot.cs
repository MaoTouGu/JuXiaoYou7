namespace MaoTouGu.Shells.Core
{
    /// <summary>
    /// <see cref="FlyoutRoot"/> 表示一个弹出层。
    /// </summary>
    public abstract class FlyoutRoot : DialogBase
    {
        private readonly TaskCompletionSource _TCSource;


        protected FlyoutRoot()
        {
            _TCSource       = new TaskCompletionSource();
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        protected override bool CanFinish() => true;

        protected override void Finish()
        {
            _TCSource.SetResult();
        }

        protected override void OnCancel()
        {
            _TCSource.SetResult();
        }

        public Task Awaitable => _TCSource.Task;
    }
}