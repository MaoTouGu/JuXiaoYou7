// ----------------------------------------------------------
//            文件：CFToggleSwitchElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public class CFToggleSwitchElement : CFBooleanElement
    {
        internal CFToggleSwitchElement() : base()
        {
            
        }
        
        private CFToggleSwitchElement(PropertyInfo propertyInfo, ToggleSwitchAttribute attribute) : base(propertyInfo, attribute)
        {
            PropertyInfo = propertyInfo;
        }
        
        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is CheckBoxAttribute && info.PropertyType == typeof(bool);
        }
        
        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFToggleSwitchElement(info, (ToggleSwitchAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFToggleSwitchElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
            };
        }
    }
}