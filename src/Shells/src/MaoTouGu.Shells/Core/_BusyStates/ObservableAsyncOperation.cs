namespace MaoTouGu.Shells.Core
{
    public abstract class ObservableAsyncOperation
    {
        private string _text;

        protected void SetBusyText(string text)
        {
            _text             = text;
            IsBusyTextChanged = true;
        }

        /// <summary>
        /// 执行任务。
        /// </summary>
        /// <returns>返回一个Task。</returns>
        public abstract Task Run();

        public void FinishBusyTextChanged()
        {
            IsBusyTextChanged = false;
        }

        /// <summary>
        /// 当前任务是否完成。
        /// </summary>
        public bool IsCompleted { get; protected set; }

        public string Text
        {
            get => _text;
            init => _text = value;
        }

        public bool IsBusyTextChanged { get; private set; }

    }
}