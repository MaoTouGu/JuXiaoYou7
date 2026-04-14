// ----------------------------------------------------------
//            文件：RemoveLayerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 16:58
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class RemoveLayerCommand(DesignViewModel target) : ContextCommand<TypographyLayerVPO, DesignViewModel>(target)
    {
        protected override async void Execute(TypographyLayerVPO target)
        {
            if (!await Context.RemoveThis())
            {
                return;
            }

            if (Context.Layers.Remove(target))
            {
                foreach (var block in target.Blocks
                                            .Where(block => Context.Dictionary.Remove(block.Id)))
                {
                    //
                    // 记录到Page
                    Context.Page.Blocks.Remove(block.Base);

                    //
                    //
                    Context.SetDirtyState(true);
                }
                
                if (Context.Layer == target)
                {
                    var index = Context.Layers.IndexOf(target);

                    if (index < Context.Layers.Count)
                    {
                        Context.Layer = Context.Layers.LastOrDefault();
                    }
                    else
                    {
                        Context.Layer = Context.Layers[index];
                    }
                }
            }
        }
    }
}