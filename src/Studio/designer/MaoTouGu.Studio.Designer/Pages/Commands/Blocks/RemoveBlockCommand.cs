// ----------------------------------------------------------
//            文件：RemoveBlockCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 16:52
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class RemoveBlockCommand(DesignViewModel target) : ContextCommand<TypographyBlockVPO, DesignViewModel>(target)
    {

        protected override async void Execute(TypographyBlockVPO target)
        {
            if (!await Context.RemoveThis())
            {
                return;
            }

            if (Context.Dictionary.Remove(target.Id))
            {
                Context.Blocks.Remove(target);

                //
                // 记录到Layer
                Context.Layer.Blocks.Remove(target);
                Context.Layer.BlockIds.Remove(target.Id);

                //
                // 记录到Page
                Context.Page.Blocks.Remove(target.Base);

                //
                //
                Context.SetDirtyState(true);
            }
        }
    }
}