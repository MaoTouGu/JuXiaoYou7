namespace MaoTouGu.Shells.Core
{
    public sealed class TextInputRoot : ObjectRoot<string>
    {
        private string _text;
        private string _description;

        [DebuggerHidden]
        public TextInputRoot()
        {
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        public TextInputRoot(string title, string description, string value, bool multiple)
        {
            Description = description;
            Text        = value;
            Title       = title;
            IsMultiline = multiple;

            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }


        protected override bool CanFinish() => !string.IsNullOrEmpty(_text);

        protected override string OnFinish(bool edit)
        {
            return _text;
        }

        public bool IsMultiline { get; }



        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value);
        }


        public string Text
        {
            get => _text;
            set => TryFinishAndSetValue(ref _text, value);
        }
    }
}