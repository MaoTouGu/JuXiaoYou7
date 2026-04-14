// ----------------------------------------------------------
//            文件：VisualizerControl.Throttle.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 14:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Controls
{
    partial class VisualizerControl
    {
        private static readonly Timer _ThrottleTimer;

        private static readonly ConcurrentQueue<VisualizerControl> _throttleRequests;

        static VisualizerControl()
        {
            _ThrottleTimer    = new Timer(OnSampling, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
            _throttleRequests = new ConcurrentQueue<VisualizerControl>();

            Ioc.Get<IAppModel>()
               .Collect(_ThrottleTimer);
        }



        private static void OnSampling(object state)
        {
            //
            // _ThrottleTimer会定时的检查队列，如果队列中包含
            while (_throttleRequests.TryDequeue(out var control))
            {
                if (control._optionEvent is not null)
                {
                    var throttleEvent = control._optionEvent;

                    //
                    //
                    var control2 = control;
                    GUI.RunOnUIThread(() =>
                                      {
                                          control2.OptionChangedOverride(throttleEvent.Moniker, throttleEvent.Options);
                                          control2._optionEvent = null;
                                      });

                    //
                    //
                    if (throttleEvent.VPO is not null)
                    {
                        throttleEvent.VPO
                                     .Instance
                                     .Base64 = throttleEvent.Options.ToBase64();
                    }
                }


                if (control._structureEvent is not null)
                {
                    var throttleEvent = control._structureEvent;
                    var control2      = control;
                    GUI.RunOnUIThread(() =>
                                      {
                                          if (control2._structureEvent is not null)
                                          {
                                              control2.StructureChangedOverride(throttleEvent.Moniker, throttleEvent.Options);
                                              control2._structureEvent = null;
                                          }
                                      });
                }
                //
                //

            }
        }

        private sealed class ThrottleEvent
        {
            public Moniker                 Moniker;
            public IVisualizerOptions      Options;
            public TypographyVisualizerVPO VPO;
        }
    }
}