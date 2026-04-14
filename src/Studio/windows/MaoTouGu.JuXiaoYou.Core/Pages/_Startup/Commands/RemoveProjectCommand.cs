// ----------------------------------------------------------
//            文件：RemoveProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 03:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class RemoveProjectCommand(StartupViewModel target) : ContextCommand<Project, StartupViewModel>(target)
    {
        protected override async void Execute(Project project)
        {

            if (!await Context.RemoveThis())
            {
                return;
            }

            var projSetting = GlobalSettings.ProjectSettings;

            //
            // 添加
            Context.Projects.Remove(project);
            projSetting.Projects.Remove(project);


            //
            //
            if (projSetting.DefaultProject == project.Id &&
                Context.Projects.Count     > 0)
            {
                projSetting.DefaultProject = Context.Projects
                                                    .FirstOrDefault()
                                                   ?.Id;
                Context.UpdateDefaultProject();
            }

            if (Context.Project == project)
            {
                Context.Project = Context.Projects
                                         .FirstOrDefault();
            }

            //
            //
            GlobalSettings.SaveProjectSettings();
        }
    }
}