// ----------------------------------------------------------
//            文件：LongTextVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public class LongTextVisualizer : VisualizerOptions<TextSettingView, LongTextPresenter>
    {

        public override IEnumerable<string> GetMetadataSources() => throw new NotImplementedException();
        protected override IVisualizerOptions Clone(string base64) => throw new NotImplementedException();
        public override string Id   { get; }
        public override string Name { get; }
    }
}