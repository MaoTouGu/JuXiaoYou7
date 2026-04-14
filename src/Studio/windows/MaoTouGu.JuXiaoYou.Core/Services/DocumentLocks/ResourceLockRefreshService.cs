// ----------------------------------------------------------
//            文件：ResourceLockRefreshService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 10:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Services
{
    public sealed class ResourceLockRefreshService : Lifetime
    {
        private Timer _Timer;


        protected override void OnStart()
        {
            _Timer = new Timer(OnCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        protected override void OnStop()
        {
            _Timer.Dispose();
        }

        static async void OnCallback(object state)
        {
            Debug.WriteLine($"Service :{DateTime.Now} -> Refresh Resource Lock");

            var context  = ((IViewModelProvider)Ioc.Get<IAppModel>()).GetContextList();
            var iterator = context.OfType<InstancePage>();
            var api      = Ioc.SafeGet<IResourceLockApiContract>();

            if (api is null)
            {
                return;
            }

            foreach (var page in iterator)
            {
                if (!page.IsOwned || (DateTime.Now - page.Modified).TotalMinutes < 5)
                {
                    await api.RefreshLockAsync(page.InstanceID);
                    page.Modified = DateTime.Now;

                    Debug.WriteLine($"Refresh Lock -> {page.GetType().Name} = {page.InstanceID}");
                }
            }
        }
    }
}