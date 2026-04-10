// ----------------------------------------------------------
//            文件：EnumAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 23:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class EnumAttribute : CFEAttribute
    {
        public EnumAttribute(string name) : this(false, name)
        {

        }

        public EnumAttribute(bool i18n, string name) : base(i18n, name)
        {

        }
    }
}