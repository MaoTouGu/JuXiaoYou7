// ----------------------------------------------------------
//            文件：RadarItemFrom.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 22:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Attributes;

namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public class RadarItemFrom : DatabaseObject
    {
        private string _metadataSource;
        private string _color;
        private string _name;

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
        
        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }

        public string MetadataSource
        {
            get => _metadataSource;
            set => SetValue(ref _metadataSource, value);
        }
    }
}