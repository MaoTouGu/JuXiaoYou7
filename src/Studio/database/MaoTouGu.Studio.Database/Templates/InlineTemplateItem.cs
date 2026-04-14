// ----------------------------------------------------------
//            文件：InlineTemplateItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月13日 16:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Templates
{
    public class InlineTemplateItem : DatabaseObject
    {
        private string _metadata;
        private string _visualizer;

        public string Visualizer
        {
            get => _visualizer;
            set => SetValue(ref _visualizer, value);
        }
        
        public string Metadata
        {
            get => _metadata;
            set => SetValue(ref _metadata, value);
        }
    }
}