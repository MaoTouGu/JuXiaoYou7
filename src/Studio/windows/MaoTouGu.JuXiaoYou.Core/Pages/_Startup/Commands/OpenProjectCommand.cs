// ----------------------------------------------------------
//            文件：OpenProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 13:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.JuXiaoYou.Pages;
using  MaoTouGu.Studio.Database.Core;

namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class OpenProjectCommand(StartupViewModel target) : ContextCommand<Project, StartupViewModel>(target)
    {

        private Credential GetCredential(Project project)
        {
            return Context.Credential ??
                   project.Credentials.FirstOrDefault(x => x.IsDefault) ??
                   project.Credentials.First();
        }

        private async Task<bool> OpenOnlineProject(Project project)
        {
            if (project.Credentials is null || project.Credentials.Count == 0)
            {
                Context.Warning("警告", "您需要一个账号才能连接此服务器。");
                return false;
            }

            if (!await ServerHealth.IsAlive(project.Url))
            {
                Context.Warning("警告", "此服务器离线或者宕机。");
                return false;
            }

            //
            // 创建Api。
            var api = new RemoteApi(project.Url);


            //
            //
            var credential = GetCredential(project);
            var r          = await api.LoginAsync(credential.Account, credential.Password, false);

            if (!r.IsFinished)
            {
                Context.Warning("警告", r.Reason);
                return false;
            }

            if (GlobalSettings.EnsureAppNotRun(project.Url))
            {
                Context.Warning("警告", "已经打开过这个服务器了，请查看其它的橘小柚应用。");
                return false;
            }

            //
            // 保存Cookie到指定位置。
            GlobalSettings.Api  = api;
            GlobalSettings.Url  = project.Url;
            GlobalSettings.User = r.Value;
            api.UserID          = r.Value.Id;
            return true;
        }

        private async Task OpenLocalProject(Project project)
        {
            //
            // 创建Api。
            var api = new LocalApi(project.Url);
            var r   = await api.LoginAsync(null, null, false);

            if (!r.IsFinished)
            {
                Context.Warning("警告", r.Reason);
                return;
            }

            GlobalSettings.Api  = api;
            GlobalSettings.Url  = project.Url;
            GlobalSettings.User = r.Value;
        }

        protected override async void Execute(Project project)
        {
            //
            //
            if (project.IsOnline)
            {
               if(! await OpenOnlineProject(project))
               {
                   return;
               }
            }
            else
            {
                await OpenLocalProject(project);
            }


            //
            // 设置在线模式。
            GlobalSettings.OnlineMode = project.IsOnline;

            //
            // 创建
            if (!Ioc.IsRegistered<IDatabaseManager>())
            {
                var dbm = DatabaseManager.Create(project);
                Ioc.Use<IDatabaseManager>(dbm);
            }

            //
            // 打开
            if (Ioc.SafeGet<IAppModel>() is {} shell)
            {
                await shell.Navigate<PlaceholdingViewModel>();
            }

            Context.Stop();
        }
    }
}