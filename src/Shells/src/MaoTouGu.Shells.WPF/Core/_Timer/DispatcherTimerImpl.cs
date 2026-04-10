using System.Windows.Threading;
using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.Core
{
    sealed class DispatcherTimerImpl : IDispatcherTimer
    {
        private readonly DispatcherTimer _Timer;
        private readonly Action          _Callback;

        public DispatcherTimerImpl(int time, Action callback)
        {
            _Callback = callback;
            _Timer    = new DispatcherTimer(TimeSpan.FromMilliseconds(time), DispatcherPriority.Normal,  OnTick, Dispatcher.CurrentDispatcher);
        }

        void OnTick(object sender, EventArgs e)
        {
            _Callback?.Invoke();
        }
            
        public void Start()
        {
            _Timer.Start();
        }
        
        public void Stop()
        {
            _Timer.Stop();
        }
    }
}