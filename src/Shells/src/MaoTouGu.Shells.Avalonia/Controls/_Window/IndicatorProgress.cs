using ProgressBar = System.Windows.Controls.ProgressBar;

namespace MaoTouGu.Shells.Controls
{
    public class IndicatorProgress : ProgressBar
    {
        static IndicatorProgress()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicatorProgress), new FrameworkPropertyMetadata(typeof(IndicatorProgress)));
        }
    }
}