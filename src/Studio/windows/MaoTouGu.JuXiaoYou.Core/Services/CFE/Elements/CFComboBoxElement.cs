// ----------------------------------------------------------
//            文件：CFComboBoxElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFComboBoxElement : CFSelectorElement
    {
        internal CFComboBoxElement() : base()
        {
            
        }
        
        private CFComboBoxElement(PropertyInfo propertyInfo, ComboBoxAttribute attribute) : base(propertyInfo, attribute)
        {
        }
        
        
        private CFComboBoxElement(PropertyInfo propertyInfo, EnumAttribute attribute): base(propertyInfo, attribute)
        {
        }
        
        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is ComboBoxAttribute or EnumAttribute && base.CanAccept(attribute, info);
        }
        
        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            if (attribute is EnumAttribute ea)
            {
                return new CFComboBoxElement(info, ea);
            }
            
            return new CFComboBoxElement(info, (ComboBoxAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFComboBoxElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
                IsEnum       = IsEnum,
                PropertyName = PropertyName,
                DataType     = DataType,
            };
        }
    }
}