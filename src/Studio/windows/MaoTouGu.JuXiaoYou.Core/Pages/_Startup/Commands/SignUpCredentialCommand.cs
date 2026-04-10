// ----------------------------------------------------------
//            文件：SignUpCredentialCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 15:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class SignUpCredentialCommand(StartupViewModel target) : ContextCommand<Project, StartupViewModel>(target)
    {
        protected override async void Execute( Project project)
        {

            var r = await Context.Object<Credential>(new SignUpCredentialViewModel(project));

            if (!r.IsFinished)
            {
                return;
            }


            //
            // 添加。
            Context.Project
                   .Credentials
                   .Add(r.Value);

            //
            //
            Context.Credentials.Add(r.Value);

            //
            //
            Context.Credential = r.Value;

            //
            //
            GlobalSettings.SaveProjectSettings();
        }
    }
}