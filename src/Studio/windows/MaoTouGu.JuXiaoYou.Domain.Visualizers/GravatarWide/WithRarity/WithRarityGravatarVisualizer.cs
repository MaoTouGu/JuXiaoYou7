// ----------------------------------------------------------
//            文件：WithRarityGravatarVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.GravatarWide
{
    public class WithRarityGravatarVisualizer : MetadataSourceVisualizerOptions<WithRarityGravatarPresenter>, IGravatarWideVisualizer
    {
        protected override IVisualizerOptions Clone(string base64) => JSON2.FromBase64<WithRarityGravatarVisualizer>(base64);


        public override string Id   => "CD1A9371B14446F699A831C6EEBB3F14";
        public override string Name => "头像（稀有度）";
    }
}