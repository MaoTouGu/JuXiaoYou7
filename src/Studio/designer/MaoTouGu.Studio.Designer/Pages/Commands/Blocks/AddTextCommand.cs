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
            var visualizer = new TypographyText
            {
                Id                  = ID.Get(),
                Name                = "文本",
                Text                = "文本",
                Width               = 100,
                Height              = 40,
                Background          = "#00000000",
                BorderBrush         = "#00000000",
                FontFamily          = "Micorsoft Yahei",
                FontWeight          = 2,
                FontSize            = 14,
                VerticalAlignment   = 3,
                HorizontalAlignment = 3,
            };

            //
            //
            AppendVisualizer(visualizer);
        }
    }
}