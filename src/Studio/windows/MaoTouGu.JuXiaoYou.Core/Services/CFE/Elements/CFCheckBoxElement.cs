// ----------------------------------------------------------
//            文件：CFCheckBoxElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFCheckBoxElement : CFBooleanElement
    {
        internal CFCheckBoxElement() : base()
        {
            
        }
        
        private CFCheckBoxElement(PropertyInfo propertyInfo, CheckBoxAttribute attribute) : base(propertyInfo, attribute)
        {
            PropertyInfo = propertyInfo;
        }
        
        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is CheckBoxAttribute && info.PropertyType == typeof(bool);
        }
        
        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFCheckBoxElement(info, (CheckBoxAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFCheckBoxElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
            };
        }
    }
}