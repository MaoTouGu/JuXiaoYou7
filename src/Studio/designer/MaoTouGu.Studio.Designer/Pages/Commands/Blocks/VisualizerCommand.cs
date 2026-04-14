// ----------------------------------------------------------
//            文件：VisualizerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 12:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    public abstract class VisualizerCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        protected bool Verify()
        {

            if (Context.Page is null)
            {
                Context.Warning("警告", "请选择一个页面。");
                return false;
            }

            if (Context.Page.IsLock)
            {
                Context.Warning("警告", "该页面已锁定，无法添加元素，请先解锁！");
                return false;
            }

            if (Context.Layer is null)
            {
                Context.Warning("警告", "请选择一个图层。");
                return false;
            }

            if (Context.Layer.IsLock)
            {
                Context.Warning("警告", "该图层已锁定，无法添加元素，请先解锁！");
                return false;
            }

            return true;
        }

        protected void GenerateVisualizer(IVisualizerGenerator visualizer)
        {
            var option = visualizer.CreateOptions();

            var block = new TypographyWithVisualizer
            {
                Id         = ID.Get(),
                Name       = visualizer.Name,
                Visualizer = visualizer.Id,
                Base64     = option.ToBase64(),
            };

            var instance = TypographyBlockVPO.GetInstance(block, Context.Moniker);

            //
            //
            instance.Options = option;
            instance.Width   = option.MinWidth;
            instance.Height  = option.MinHeight;;
            //
            //
            if (Context.Dictionary.TryAdd(instance.Id, instance))
            {
                //
                // Block级别的操作
                Context.Blocks.Add(instance);

                //
                // 记录到Layer
                Context.Layer.Blocks.Add(instance);
                Context.Layer.BlockIds.Add(block.Id);

                //
                // 记录到Page
                Context.Page.Blocks.Add(block);

                //
                //
                Context.SetDirtyState(true);
            }
        }
        
        protected void AppendVisualizer(TypographyBlock block)
        {
            var instance = TypographyBlockVPO.GetInstance(block, Context.Moniker);

            //
            //
            if (Context.Dictionary.TryAdd(instance.Id, instance))
            {
                //
                // Block级别的操作
                Context.Blocks.Add(instance);

                //
                // 记录到Layer
                Context.Layer.Blocks.Add(instance);
                Context.Layer.BlockIds.Add(block.Id);

                //
                // 记录到Page
                Context.Page.Blocks.Add(block);

                //
                //
                Context.SetDirtyState(true);
            }
        }
    }
}