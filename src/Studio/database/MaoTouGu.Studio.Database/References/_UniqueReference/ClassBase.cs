// ----------------------------------------------------------
//            文件：ClassBase.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 15:55
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    public abstract class ClassBase : Nameable, IComparable<ClassBase>
    {
        public int CompareTo(ClassBase other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (other is null)
            {
                return 1;
            }

            return Index.CompareTo(other.Index);
        }
        
        public int Index { get; set; }
    }
}