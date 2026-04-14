// ----------------------------------------------------------
//            文件：AddRectangleCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 12:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddRectangleCommand(DesignViewModel target) : VisualizerCommand(target)
    {
        
        public override void Execute(object parameter)
        {
            if (!Verify())
            {
                return;
            }

            //
            //
            var visualizer = new TypographyRectangle
            {
                Id     = ID.Get(),
                Name   = "文本",
                Width  = 100,
                Height = 100,
                Background = "#808080",
                
            };


            AppendVisualizer(visualizer);
        }
    }
}