namespace MaoTouGu.Shells.Core
{
    public class BooleanRoot : DialogBase
    {
        protected readonly TaskCompletionSource<bool> TaskCompletionSource;

        
        protected BooleanRoot()
        {
            TaskCompletionSource = new TaskCompletionSource<bool>();
            CompleteCommand      = new DelegateCommand(Complete, CanFinish);
            CancelCommand        = new DelegateCommand(Cancel);
        }

        protected override bool CanFinish() =>  true;

        protected override void Finish()
        {
            TaskCompletionSource.SetResult(true);
        }

        protected override void OnCancel()
        {
            TaskCompletionSource.SetResult(false);
        }
        
        
        internal Task<bool> Awaitable => TaskCompletionSource.Task;
    }
}