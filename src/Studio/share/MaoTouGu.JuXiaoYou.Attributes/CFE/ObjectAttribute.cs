// ----------------------------------------------------------
//            文件：ObjectAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 23:00
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ObjectAttribute : CFEAttribute
    {
        public ObjectAttribute(string name) : this(false, name)
        {

        }

        public ObjectAttribute(bool i18n, string name) : base(i18n, name)
        {
        }
    }
}