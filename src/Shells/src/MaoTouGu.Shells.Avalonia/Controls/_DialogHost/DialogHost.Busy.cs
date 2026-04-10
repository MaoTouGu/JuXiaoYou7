using System.Windows.Threading;

#pragma warning disable CA1816
namespace MaoTouGu.Shells.Controls
{
    partial class DialogHost : IBusyStateRecipient
    {

        public static readonly DependencyPropertyKey IsBusyProperty;
        public static readonly DependencyProperty    BusyTextProperty;
        
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty.DependencyProperty);
            set => SetValue(IsBusyProperty, Boxing.Box(value));
        }

        public string BusyText
        {
            get => (string)GetValue(BusyTextProperty);
            set => SetValue(BusyTextProperty, value);
        }
        
        public void Enter() => IsBusy = true;
        public void Leave()=> IsBusy = false;
        
        
        public IDispatcherTimer GetTimer(int time, Action callback) => new DispatcherTimerImpl(time, callback);
        
        public void SetBusyText(string text)
        {
            BusyText = text;
        }
        public void ChangeToIndeterminateState() => PART_ProgressBar.IsIndeterminate = true;
        public void ChangeToDeterminateState() => PART_ProgressBar.IsIndeterminate = false;
        public void ReportProgress(int percent) => PART_ProgressBar.Value = percent;
        public void ReportOperationCount(int count) => PART_ProgressBar.Maximum = count;
        public void ShouldLongTimeTaskShutdown()
        {
            
        }
    }
}
#pragma warning restore CA1816