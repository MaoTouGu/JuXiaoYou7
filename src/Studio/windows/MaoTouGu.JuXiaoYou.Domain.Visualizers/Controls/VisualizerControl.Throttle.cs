// ----------------------------------------------------------
//            文件：VisualizerControl.Throttle.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 14:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    partial class VisualizerControl
    {
        private static readonly Timer _ThrottleTimer;

        private static readonly ConcurrentQueue<VisualizerControl>    _throttleRequests;
        private static readonly ConcurrentDictionary<int, int>        _requestTable;

        static VisualizerControl()
        {
            _ThrottleTimer    = new Timer(OnSampling, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
            _requestTable     = new ConcurrentDictionary<int, int>();
            _throttleRequests = new ConcurrentQueue<VisualizerControl>();

            Ioc.Get<IAppModel>()
               .Collect(_ThrottleTimer);
        }

        private ThrottleEvent _throttleEvent = new ();


        private static void OnSampling(object state)
        {
            //
            // _ThrottleTimer会定时的检查队列，如果队列中包含
            while (_throttleRequests.TryDequeue(out var control))
            {
                var throttleEvent = control._throttleEvent;

                //
                //
                var result = control;
                GUI.RunOnUIThread(() =>
                                  {
                                      result.OnBuildExpression(throttleEvent.Moniker, throttleEvent.Options);
                                  });

                //
                //
                if (throttleEvent.VPO is not null)
                {
                    throttleEvent.VPO
                                 .Instance
                                 .Base64 = throttleEvent.Options.ToBase64();
                }

                _requestTable.TryRemove(control.GetHashCode(), out _);
            }
        }

        private struct ThrottleEvent
        {
            public Moniker                 Moniker;
            public IVisualizerOptions      Options;
            public TypographyVisualizerVPO VPO;
        }
    }
}