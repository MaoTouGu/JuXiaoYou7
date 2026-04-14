// ----------------------------------------------------------
//            文件：RectangleVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 22:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public class RectangleVisualizer : MetadataSourceVisualizerOptions<RectanglePresenter>
    {
        protected override IVisualizerOptions Clone(string base64) => JSON2.FromBase64<RectangleVisualizer>(base64);
        
        public override string Id   => "028015C76C9047ACA8F9ECF7F7FC4B0B";
        public override string Name => "矩形色块";

        public override int MinHeight => 20;
        public override int MinWidth => 20;
    }
}