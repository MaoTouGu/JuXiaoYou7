// ----------------------------------------------------------
//            文件：TypographyWithVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 15:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Topology
{
    public class TypographyWithVisualizer : TypographyBlock
    {
        private string _visualizer;
        private string _base64;
        
        public string Base64
        {
            get => _base64;
            set => SetValue(ref _base64, value);
        }

        public string Visualizer
        {
            get => _visualizer;
            set => SetValue(ref _visualizer, value);
        }
    }
}