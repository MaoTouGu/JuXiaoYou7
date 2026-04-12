// ----------------------------------------------------------
//            文件：MetadataSourceVisualizerOptions.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 23:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Visualizers.Commons;

namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public abstract class MetadataSourceVisualizerOptions<TView> : VisualizerOptions<MetadataSourceOnlySettingView, TView>,
        IVisualizerOptions, 
        IVisualizerGenerator,
        IMetadataSourceOnlySetting
        where TView : VisualizerControl
    {
        private string _metadataSource;

        public override IEnumerable<string> GetMetadataSources()
        {
            yield return MetadataSource;
        }

        public string MetadataSource
        {
            get => _metadataSource;
            set => SetValue(ref _metadataSource, value);
        }

    }
}