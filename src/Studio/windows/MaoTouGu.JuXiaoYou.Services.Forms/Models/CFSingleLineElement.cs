// ----------------------------------------------------------
//            文件：TextAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFSingleLineElement : CFTextElement
    {
        internal CFSingleLineElement() : base()
        {
            
        }
        
        private CFSingleLineElement(PropertyInfo propertyInfo, TextAttribute attribute) : base(propertyInfo, attribute)
        {
        }
            
        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is TextAttribute { IsMultipleLine : false } && info.PropertyType == typeof(string);
        }
        
        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFSingleLineElement(info, (TextAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFSingleLineElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
            };
        }
    }
}