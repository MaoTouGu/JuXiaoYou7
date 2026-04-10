// ----------------------------------------------------------
//            文件：CFMultiLineElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 13:30
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFMultiLineElement : CFTextElement
    {
        internal CFMultiLineElement()
        {
        }

        private CFMultiLineElement(PropertyInfo propertyInfo, TextAttribute attribute) : base(propertyInfo, attribute)
        {
        }

        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is TextAttribute { IsMultipleLine : true } && info.PropertyType == typeof(string);
        }

        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFMultiLineElement(info, (TextAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFMultiLineElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
            };
        }

        
    }
}