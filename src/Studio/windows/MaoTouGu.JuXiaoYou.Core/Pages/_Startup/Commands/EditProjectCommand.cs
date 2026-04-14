// ----------------------------------------------------------
//            文件：EditProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 03:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class EditProjectCommand(StartupViewModel target) : ContextCommand<Project, StartupViewModel>(target)
    {
        protected override async void Execute(Project project)
        {

            var r = await Context.Object<Project>(new NewProjectViewModel(project));

            if (!r.IsFinished)
            {
                return;
            }

            //
            //
            GlobalSettings.SaveProjectSettings();
        }
    }
}