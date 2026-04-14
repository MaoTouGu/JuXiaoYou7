// ----------------------------------------------------------
//            文件：RemovePageCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 16:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class RemovePageCommand(DesignViewModel target) : ContextCommand<TypographyPage, DesignViewModel>(target)
    {
        protected override async void Execute(TypographyPage target)
        {
            if (target.IsLock)
            {
                Context.Warning("警告", "页面已被锁定，无法删除。");
                return;
            }

            if (!await Context.RemoveThis())
            {
                return;
            }

            Context.Pages.Remove(target);
            Context.Template.Pages.Remove(target);
            Context.SetDirtyState(true);

            if (Context.Page == target)
            {
                var index = Context.Pages.IndexOf(target);

                if (index < Context.Pages.Count)
                {
                    Context.Page = Context.Pages.LastOrDefault();
                }
                else
                {
                    Context.Page = Context.Pages[index];
                }
            }
        }
    }
}