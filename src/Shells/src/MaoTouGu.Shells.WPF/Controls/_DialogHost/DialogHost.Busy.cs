using System.Windows.Threading;

#pragma warning disable CA1816
namespace MaoTouGu.Shells.Controls
{
    partial class DialogHost : IBusyStateRecipient
    {

        public static readonly DependencyProperty IsBusyProperty;
        public static readonly DependencyProperty    BusyTextProperty;

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, Boxing.Box(value));
        }

        public string BusyText
        {
            get => (string)GetValue(BusyTextProperty);
            set => SetValue(BusyTextProperty, value);
        }

        public void Enter()
        {
            GUI.RunOnUIThread(() =>
                              {
                                  IsBusy = true;
                              });
        }

        public void Leave()
        {
            GUI.RunOnUIThread(() =>
                              {
                                  IsBusy = false;
                              });
        }


        public IDispatcherTimer GetTimer(int time, Action callback) => new DispatcherTimerImpl(time, callback);

        public void SetBusyText(string text)
        {
            BusyText = text;
        }

        public bool IsDeterminateState()
        {
            return PART_ProgressBar.Dispatcher
                                   .Invoke(() => PART_ProgressBar.IsIndeterminate);
        }
        
        public void ChangeToIndeterminateState()
        {
            GUI.RunOnUIThread(() => PART_ProgressBar.IsIndeterminate = true);
        }
        
        public void ChangeToDeterminateState()
        {
            GUI.RunOnUIThread(() => PART_ProgressBar.IsIndeterminate = false);
        }
        
        public void ReportProgress(int percent)
        {
            GUI.RunOnUIThread(() => PART_ProgressBar.Value = percent);
        }
        
        public void ReportOperationCount(int count)
        {
            GUI.RunOnUIThread(() => PART_ProgressBar.Maximum = count);
        }
        
        public void ShouldLongTimeTaskShutdown()
        {
            Leave();
        }
    }
}
#pragma warning restore CA1816