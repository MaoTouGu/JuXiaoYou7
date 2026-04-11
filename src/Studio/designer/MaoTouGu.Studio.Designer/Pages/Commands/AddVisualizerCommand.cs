// ----------------------------------------------------------
//            文件：AddVisualizerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 16:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Plugins;

namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddVisualizerCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        public override async void Execute(object parameter)
        {
            if (Context.Page is null)
            {
                Context.Warning("警告", "请选择一个页面。");
                return;
            }

            if (Context.Page.IsLock)
            {
                Context.Warning("警告", "该页面已锁定，无法添加元素，请先解锁！");
                return;
            }

            if (Context.Layer is null)
            {
                Context.Warning("警告", "请选择一个图层。");
                return;
            }

            if (Context.Layer.IsLock)
            {
                Context.Warning("警告", "该图层已锁定，无法添加元素，请先解锁！");
                return;
            }

            var picker = new GlobalObjectPicker<IVisualizerGenerator>(FeatureManager.Visualizers.Values, nameof(IBlockWideVisualizer.Name));
            var r      = await Context.Object(picker);

            if (!r.IsFinished)
            {
                return;
            }

            var visualizer = r.Value;
            var option     = visualizer.CreateOptions();

            var block = new TypographyWithVisualizer
            {
                Id         = ID.Get(),
                Name       = visualizer.Name,
                Visualizer = visualizer.Id,
                Base64     = option.ToBase64(),
            };

            var vpo = new TypographyVisualizerVPO
            {
                Visualizer = block,
                Moniker    = Context.Moniker,
            };

            Context.Blocks.Add(vpo);

            //
            //
            Context.Layer.Blocks.Add(vpo);
            Context.Layer.Layer.Blocks.Add(block.Id);

            //
            //
            Context.Page.Blocks.Add(block);

            //
            //
            Context.SetDirtyState(true);
        }
    }
}