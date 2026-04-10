// ----------------------------------------------------------
//            文件：CreateProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 22:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class CreateProjectCommand(StartupViewModel target) : ContextCommand<StartupViewModel>(target)
    {
        public override async void Execute(object parameter)
        {
            var r = await Context.Object<Project, NewProjectViewModel>();

            if (!r.IsFinished)
            {
                return;
            }

            var proj        = r.Value;
            var projSetting = GlobalSettings.ProjectSettings;
            

            //
            // 如果没有默认项目，则设置默认项目。
            if (projSetting.Projects.Count == 0)
            {
                projSetting.DefaultProject = proj.Id;
            }

            //
            // 添加
            Context.Projects.Add(proj);
            projSetting.Projects.Add(proj);

            //
            // 选择。
            Context.Project = proj;
                
            //
            //
            Context.UpdateDefaultProject();
            GlobalSettings.SaveProjectSettings();
        }
    }
}