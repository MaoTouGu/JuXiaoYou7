namespace MaoTouGu.Shells.Core
{
    public interface IBusyStateRecipient
    {
        /// <summary>
        /// 进入繁忙状态。
        /// </summary>
        void Enter();
        
        /// <summary>
        /// 退出繁忙状态。
        /// </summary>
        void Leave();
        
        /// <summary>
        /// 获得DispatcherTimer.
        /// </summary>
        /// <param name="time">间隔时间，单位：毫秒。</param>
        /// <param name="callback">回调。</param>
        /// <returns></returns>
        IDispatcherTimer GetTimer(int time, Action callback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void SetBusyText(string text);
        
        /// <summary>
        /// 
        /// </summary>
        void ChangeToIndeterminateState();
        
        /// <summary>
        /// 改变状态
        /// </summary>
        void ChangeToDeterminateState();
        
        /// <summary>
        /// 报告进度。
        /// </summary>
        /// <param name="percent"></param>
        void ReportProgress(int percent);
        
        /// <summary>
        /// 设置任务最大值。
        /// </summary>
        /// <param name="count"></param>
        void ReportOperationCount(int count);

        bool IsDeterminateState();

        void ShouldLongTimeTaskShutdown();
    }
}