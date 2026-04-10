// ----------------------------------------------------------
//            文件：AddProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 21:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class AddProjectCommand(StartupViewModel target) : ContextCommand<StartupViewModel>(target)
    {
        public override void Execute(object parameter)
        {
            var r = Interop.OpenFileAsync("企划文件|*.mkProj");

            if (!r.IsFinished)
            {
                return;
            }
            
            //
            // 先反序列化，然后看看项目是否已经存在。
            var proj        = JSON.FromFile<Project>(r.Value);
            var projSetting = GlobalSettings.ProjectSettings;


            //
            // 文件格式有误。
            if (proj is null ||
                string.IsNullOrEmpty(proj.Id) ||
                string.IsNullOrEmpty(proj.Url))
            {
                Context.Warning("警告", "文件格式有误。");
                return;
            }
            
            //
            // 已经添加过了这个企划。
            if (Projects.Any(x => string.Equals(
                                                x.Url, 
                                                proj.Url,
                                                StringComparison.OrdinalIgnoreCase)))
            {
                Context.Warning("警告", "已经添加过了这个企划。");
                return;
            }

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

        public ViewList<Project> Projects => Context.Projects;
    }
}