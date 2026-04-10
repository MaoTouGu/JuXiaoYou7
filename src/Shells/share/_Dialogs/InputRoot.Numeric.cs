namespace MaoTouGu.Shells.Core
{
    public class NumericInputRoot : DialogBase
    {
        private readonly TaskCompletionSource<int> _TCS;

        private int _value;
        
        public NumericInputRoot() : this(0, 0, 10)
        {
            _TCS            = new TaskCompletionSource<int>();
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        private NumericInputRoot(int val, int min, int max)
        {
            _TCS            = new TaskCompletionSource<int>();
            Awaitable       = _TCS.Task;
            Minimum         = min;
            Maximum         = max;
            Value           = val;
            CompleteCommand = new DelegateCommand(Complete, CanFinish);
            CancelCommand   = new DelegateCommand(Cancel);
        }

        protected override bool CanFinish() => true;

        protected override void Finish()
        {
            _TCS.SetResult(Value);
        }

        public int Minimum { get; }
        public int Maximum { get; }


        public int Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }

        internal Task<int> Awaitable { get; }
    }
}