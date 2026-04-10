using System.Diagnostics;

namespace MaoTouGu.Shells.Core
{
    public sealed  class NotifyRoot : DialogBase
    {
        private readonly TaskCompletionSource _TCSource;

        [DebuggerHidden]
        public NotifyRoot()
        {
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }
        
        public NotifyRoot(NotifyType type, string title, string description, string okButtonText, string noButtonText)
        {
            OkButtonText = okButtonText;
            NoButtonText = noButtonText;
            Type         = type;
            Title        = title;
            Description  = description;

            _TCSource = new TaskCompletionSource();
            
            
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }
        
        protected override bool CanFinish() =>  true;

        protected override void Finish()
        {
            _TCSource.SetResult();
        }

        protected override void OnCancel()
        {
            _TCSource.SetResult();
        }

        public Task Awaitable => _TCSource.Task;
        
        public string     Description { get; }
        public NotifyType Type        { get; }
    }
}