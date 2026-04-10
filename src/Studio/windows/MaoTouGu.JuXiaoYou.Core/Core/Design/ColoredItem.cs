// ----------------------------------------------------------
//            文件：ColoredItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月26日 14:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core.Design
{
    public class ColoredItem : ValueItem
    {
        private string _color;

        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }
    }
}