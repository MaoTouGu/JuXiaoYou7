using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.Studio.Database.Core;

namespace MaoTouGu.JuXiaoYou.AppModels
{
    public abstract class JuXiaoYouPage : PageBase
    {
        protected JuXiaoYouPage() : this(true, true)
        {
        }

        protected JuXiaoYouPage(bool removable = true, bool singleton = false)
        {
            Removable = removable;
            Singleton = singleton;
            Shutdown  = new DelegateCommand(DoShutdown);
        }

        protected T GetService<T>() where T : DataService
        {
            var srv = DatabaseManager.Services.OfType<T>().FirstOrDefault();

            if (srv is null)
            {
                srv = Ioc.GetOrRegister<T>();
                DatabaseManager.Services.Add(srv);
            }

            return srv;
        }

        private async void DoShutdown()
        {
            if (IsDisposed)
            {
                return;
            }

            if (IsChange && !await this.QueryWithDanger("关闭", "数据已更改但未保存，一但关闭数据将会丢失，是否继续关闭该页面？"))
            {
                return;
            }

            Stop();
        }

        public ICommandEX Shutdown { get; }
    }
}