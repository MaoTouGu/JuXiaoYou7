// ----------------------------------------------------------
//            文件：RadarItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 22:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Attributes;

namespace MaoTouGu.JuXiaoYou.Visualizers.Models
{
    public class RadarItem : ObservableObject
    {
        private double _value;
        private string _metadata;
        private string _color;

        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }
        
        
        public double Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }
    }
}