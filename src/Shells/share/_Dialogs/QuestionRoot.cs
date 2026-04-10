namespace MaoTouGu.Shells.Core
{
    public class QuestionRoot : DialogBase
    {
        private readonly TaskCompletionSource<bool> _TCSource;

        [DebuggerHidden]
        public QuestionRoot()
        {
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        public QuestionRoot(
            NotifyType type,
            string title,
            string description,
            string okButtonText,
            string noButtonText)
        {
            Type = type;

            OkButtonText    = okButtonText;
            NoButtonText    = noButtonText;
            Title           = title;
            Description     = description;
            _TCSource       = new TaskCompletionSource<bool>();
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        protected override void Finish()
        {
            _TCSource.SetResult(true);
        }

        protected override void OnCancel()
        {
            _TCSource.SetResult(false);
        }

        public string Description { get; }

        public Task<bool> Awaitable => _TCSource.Task;

        public NotifyType Type { get; }
    }
}