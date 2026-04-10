namespace MaoTouGu.Shells.Core
{
    public sealed class RangeInputRoot : ObjectRoot<int>
    {
        private int _value;

        public RangeInputRoot()
        {
            Minimum = 0;
            Maximum = 10;
            Value   = 1;

            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        public RangeInputRoot(string title, string desc, int min, int max, int value)
        {
            Title       = title;
            Description = desc;
            Minimum     = min;
            Maximum     = max;
            Value       = value;

            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        protected override int OnFinish(bool edit) => Value;

        public string Description { get; }
        public int    Minimum     { get; }
        public int    Maximum     { get; }

      
        public int Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
    }
}