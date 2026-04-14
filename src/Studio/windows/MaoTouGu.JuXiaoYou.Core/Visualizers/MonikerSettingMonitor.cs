// ----------------------------------------------------------
//            文件：MonikerSettingMonitor.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 18:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public class MonikerSettingMonitor : Behavior<VisualizerControl>
    {
        //
        // 设定属性发生了变化，通知MonikerSettingMonitor
        //
        // MonikerSettingMonitor再找到对应的数据上下文，转发到实际的Action里面，
        // 然后Action去操作
        protected override void OnAttached()
        {
            Target  = AssociatedObject.DataContext as TypographyBlockVPO;
            Moniker = AssociatedObject.Moniker;
            Options = AssociatedObject.Options;

            if (Moniker is not null)
            {
                Moniker.Settings.PropertyChanged += OnPropertyChanged;
            }

        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AssociatedObject is IVisualizerSettingWorker worker)
            {
                worker.DoWork(Moniker, Options, Target, e.PropertyName);
            }
        }

        protected override void OnDetaching()
        {
            if (Moniker is not null)
            {
                Moniker.PropertyChanged -= OnPropertyChanged;
            }
        }

        public Moniker            Moniker { get; private set; }
        public IVisualizerOptions Options { get; private set; }
        public TypographyBlockVPO Target  { get; private set; }
    }
}