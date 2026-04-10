// ----------------------------------------------------------
//            文件：CustomFilter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using MaoTouGu.Studio.Database.Objects;

#pragma warning disable CS0660, CS0661

namespace MaoTouGu.Studio.Database.References
{
    [SuppressMessage("Design", "CA1067:在实现 IEquatable<T> 时替代 Object.Equals(object)")]
    public abstract class CustomFilter : Nameable, IEquatable<CustomFilter>
    {
        public abstract bool Equals(CustomFilter other);
        

        public static bool operator ==(CustomFilter left, CustomFilter right) => Equals(left, right);
        public static bool operator !=(CustomFilter left, CustomFilter right) => !Equals(left, right);
    }
}