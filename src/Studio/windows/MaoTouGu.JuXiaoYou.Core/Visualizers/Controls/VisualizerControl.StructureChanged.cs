// ----------------------------------------------------------
//            文件：VisualizerControl.StructureChanged.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月13日 14:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Controls
{
    partial class VisualizerControl
    {

        private volatile ThrottleEvent _structureEvent = new();

        /// <summary>
        /// 当<see cref="TypographyVisualizerVPO.Options"/>属性发生变化的时候，需要通知所有VisualizerControl变更绑定。
        /// </summary>
        private void OnStructureChanged(object sender, EventArgs e)
        {
            if (Options is not {} o || Moniker is not {} m)
            {
                return;
            }

            //
            // DONE: 需要做事件限流，防止过多的ToBase64调用进而导致CPU浪费。
            //
            // 采用4hz的频率，间隔250ms更新一次。
            if (_structureEvent is null)
            {
                _structureEvent = new ThrottleEvent
                {
                    Moniker = m,
                    Options = o,
                    VPO     = DataContext as TypographyVisualizerVPO,
                };
                    
                _throttleRequests.Enqueue(this);
            }

            //
            // OptionChangedOverride(m, o);
            // if (DataContext is TypographyVisualizerVPO vpo)
            // {
            //     vpo.Instance
            //        .Base64 = o.ToBase64();
            // }
        }

        protected virtual void OnStructureChanged()
        {

        }
    }
}