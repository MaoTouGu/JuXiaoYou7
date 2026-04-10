// ----------------------------------------------------------
//            文件：ComboBoxAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 23:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{
    /// <summary>
    /// ComboBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ComboBoxAttribute : CFEAttribute
    {
        public ComboBoxAttribute(string name) : this(false, name)
        {

        }

        public ComboBoxAttribute(bool i18n, string name) : base(i18n, name)
        {
        }

    }
}