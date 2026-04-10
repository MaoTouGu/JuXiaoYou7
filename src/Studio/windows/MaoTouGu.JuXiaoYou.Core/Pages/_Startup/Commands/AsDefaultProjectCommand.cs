// ----------------------------------------------------------
//            文件：AsDefaultProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 13:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class AsDefaultProjectCommand(StartupViewModel target) : ContextCommand<Project, StartupViewModel>(target)
    {
        protected override async void Execute(Project project)
        {

            var projSetting = GlobalSettings.ProjectSettings;

            if (projSetting.DefaultProject == project.Id)
            {
                Context.Warning("警告", "已经是默认项目了，不用再次设置。");
                return;
            }

            projSetting.DefaultProject = project.Id;
            Context.UpdateDefaultProject();

            //
            //
            GlobalSettings.SaveProjectSettings();
        }
    }
}