// ----------------------------------------------------------
//            文件：ListBoxAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:46
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    
    
    /// <summary>
    /// ListBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ListBoxAttribute : CFEAttribute
    {

        public ListBoxAttribute(string name) : this(false, name)
        {

        }

        public ListBoxAttribute(bool i18n, string name) : base(i18n, name)
        {
        }
    }
}