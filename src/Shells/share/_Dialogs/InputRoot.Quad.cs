
namespace MaoTouGu.Shells.Core
{
    public sealed class QuadOptionRoot : DialogBase
    {
        internal readonly TaskCompletionSource<QuadOption> _TCSource;

        public QuadOptionRoot(string title,
            string description, 
            string op1Text,
            string op2Text,
            string op3Text, 
            string okButtonText = null,
            string noButtonText = null)
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
            Option3ButtonText = op3Text;
            OkButtonText      = okButtonText;
            NoButtonText      = noButtonText;
            Title             = title;
            Description       = description;

            _TCSource = new TaskCompletionSource<QuadOption>();
            
            UseOption1Command = new DelegateCommand(DoUseOption1Command);
            UseOption2Command = new DelegateCommand(DoUseOption2Command);
            UseOption3Command = new DelegateCommand(DoUseOption3Command); 
        }

        protected override bool CanFinish() => true;

        protected override void OnCancel()
        {
            Result = QuadOption.Cancel;
            Complete();
        }
        
        private void DoUseOption1Command()
        {
            Result = QuadOption.Option1;
            Complete();
        }

        private void DoUseOption2Command()
        {
            Result = QuadOption.Option2;
            Complete();
        }

        private void DoUseOption3Command()
        {
            Result = QuadOption.Option3;
            Complete();
        }

        protected override void Finish()
        {
            _TCSource.SetResult(Result);
        }

        public QuadOption Result { get; private set; }
        
        public string Description       { get; }
        public string Option1ButtonText { get; }
        public string Option2ButtonText { get; }
        public string Option3ButtonText { get; }
        
        public ICommandEX UseOption1Command { get; }
        public ICommandEX UseOption2Command { get; }
        public ICommandEX UseOption3Command { get; }

        public Task<QuadOption> Awaitable => _TCSource.Task;
    }
}