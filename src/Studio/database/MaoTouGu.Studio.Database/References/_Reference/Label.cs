// ----------------------------------------------------------
//            文件：Label.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 15:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database.References
{
    public sealed class Label : Nameable, ISortable<Label>
    {
        private string _color;

        public int CompareTo(Label other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (other is null)
                return 1;
            return Index.CompareTo(other.Index);
        }

        public string Parent { get; set; }
        public int    Index  { get; set; }
        

        public string Color
        {
            get => _color;
            set => SetValue(ref _color, value);
        }
    }
}