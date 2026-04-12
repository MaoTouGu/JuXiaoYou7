// ----------------------------------------------------------
//            文件：ImageVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 23:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public class ImageVisualizer : MetadataSourceVisualizerOptions<ImagePresenter>
    {
        protected override IVisualizerOptions Clone(string base64) => JSON2.FromBase64<ImageVisualizer>(base64);
        
        public override string Id   => "364A493AFE7240058260629076D92ACA";
        public override string Name => "图片呈现";
    }
}