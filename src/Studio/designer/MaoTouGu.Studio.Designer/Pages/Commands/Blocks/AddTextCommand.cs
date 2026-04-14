// ----------------------------------------------------------
//            文件：AddTextCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 11:43
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddTextCommand(DesignViewModel target) : VisualizerCommand(target)
    {
        public override void Execute(object parameter)
        {
            if (!Verify())
            {
                return;
            }

            //
            //
            var visualizer = new TypographyText();
            
            //
            //
            AppendVisualizer(visualizer);
        }
    }
}