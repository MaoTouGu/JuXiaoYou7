// ----------------------------------------------------------
//            文件：VisualizerConfiguration.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 00:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Templates
{
    /// <summary>
    /// 帮助用户创建视觉项。
    /// </summary>
    public sealed class VisualizerConfiguration : DatabaseObject
    {
        private string _metadata;
        private string _visualizerName;
        private string _visualizer;

        public string Visualizer
        {
            get => _visualizer;
            set => SetValue(ref _visualizer, value);
        }

        public string VisualizerName
        {
            get => _visualizerName;
            set => SetValue(ref _visualizerName, value);
        }

        /// <summary>
        /// 设定名。
        /// </summary>
        public string Metadata
        {
            get => _metadata;
            set => SetValue(ref _metadata, value);
        }
    }
}