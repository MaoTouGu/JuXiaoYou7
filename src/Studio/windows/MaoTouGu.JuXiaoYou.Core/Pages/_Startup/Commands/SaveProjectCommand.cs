// ----------------------------------------------------------
//            文件：SaveProjectCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 13:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class SaveProjectCommand(ShareProjectViewModel target) : ContextCommand<ShareProjectViewModel>(target)
    {
        public override void Execute(object parameter)
        {
            var r = Interop.SaveFileAsync(ExtFilters.Project, ExtFilters.ProjectExt, Context.Project.Name);

            if (!r.IsFinished)
            {
                return;
            }
            
            //
            //
            JSON.ToFile(r.Value, Context.Project);
        }
    }
}