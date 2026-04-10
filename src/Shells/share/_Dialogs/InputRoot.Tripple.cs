
namespace MaoTouGu.Shells.Core
{
    public sealed class TripleOptionRoot : DialogBase
    {
        internal readonly TaskCompletionSource<TripleOption> _TCSource;
        
        public TripleOptionRoot(string title, string description, string op1Text, string op2Text, string okButtonText = null, string noButtonText = null)
        {
            if (string.IsNullOrEmpty(okButtonText))
            {
                okButtonText = I18N.GetEnum(ButtonText.Ok);
            }
            
            
            if (string.IsNullOrEmpty(noButtonText))
            {
                noButtonText = I18N.GetEnum(ButtonText.Cancel);
            }
            
            Option1ButtonText = op1Text;
            Option2ButtonText = op2Text;
            OkButtonText      = okButtonText;
            NoButtonText      = noButtonText;
            Title             = title;
            Description       = description;

            _TCSource = new TaskCompletionSource<TripleOption>();

            UseOption1Command = new DelegateCommand(DoUseOption1Command);
            UseOption2Command = new DelegateCommand(DoUseOption2Command);
        }

        protected override bool CanFinish() => true;

        protected override void OnCancel()
        {
            Result = TripleOption.Cancel;
            Complete();
        }
        
        private void DoUseOption1Command()
        {
            Result = TripleOption.Option1;
            Complete();
        }

        private void DoUseOption2Command()
        {
            Result = TripleOption.Option2;
            Complete();
        }

        protected override void Finish()
        {
            _TCSource.SetResult(Result);
        }

        public TripleOption Result { get; private set; }
        
        public ICommandEX UseOption1Command { get; }
        public ICommandEX UseOption2Command { get; }

        public string Description       { get; }
        public string Option1ButtonText { get; }
        public string Option2ButtonText { get; }

        public Task<TripleOption> Awaitable => _TCSource.Task;
    }
}