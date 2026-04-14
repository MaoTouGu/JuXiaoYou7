// ----------------------------------------------------------
//            文件：AddImageCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 12:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddImageCommand(DesignViewModel target) : VisualizerCommand(target)
    {
        
        public override async void Execute(object parameter)
        {
            if (!Verify())
            {
                return;
            }
            
            var picker = new GlobalObjectPicker<IVisualizerGenerator>(FeatureManager.Visualizers.Values, nameof(IBlockWideVisualizer.Name));
            var r      = await Context.Object(picker);

            if (!r.IsFinished)
            {
                return;
            }

            //
            //
            var visualizer = r.Value;
            
            //
            //
            GenerateVisualizer(visualizer);


            //
            //
        }
    }
}