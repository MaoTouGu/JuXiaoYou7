// ----------------------------------------------------------
//            文件：AddCredentialCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 14:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class AddCredentialCommand(StartupViewModel target) : ContextCommand<Project, StartupViewModel>(target)
    {
        protected override async void Execute(Project project)
        {

            var r = await Context.Object<Credential, NewCredentialViewModel>();

            if (!r.IsFinished)
            {
                return;
            }

            if (r.Value.IsDefault && project.Credentials.Any(x => x.IsDefault))
            {
                project.Credentials
                       .ForEach(x => x.IsDefault = false);
            }

            //
            // 添加。
            project.Credentials.Add(r.Value);
            Context.Credentials.Add(r.Value);


            GlobalSettings.SaveProjectSettings();
        }
    }
}