// ----------------------------------------------------------
//            文件：CategoryAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class CategoryAttribute : CFAttribute
    {
        public CategoryAttribute(string category) : this(false, category)
        {
        }

        public CategoryAttribute(bool i18n, string category)
        {
            UseI18N  = i18n;
            Category = category;
        }

        public bool   UseI18N  { get; init; }
        public string Category { get; init; }
    }
}