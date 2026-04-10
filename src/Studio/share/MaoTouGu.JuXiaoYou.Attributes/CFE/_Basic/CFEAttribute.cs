// ----------------------------------------------------------
//            文件：CFEAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{

    public abstract class CFEAttribute : CFAttribute
    {
        protected CFEAttribute(bool i18n, string name)
        {
            UseI18N = i18n;
            Name    = name;
        }


        public bool   UseI18N { get; init; }
        public string Name    { get; init; }
    }
}