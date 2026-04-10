// ----------------------------------------------------------
//            文件：CFPasswordElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public class CFPasswordElement : CFElement, IValueSource<string>
    {
        internal CFPasswordElement() : base()
        {
            
        }
        
        private CFPasswordElement(PropertyInfo propertyInfo, PasswordAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
        }
            
        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is PasswordAttribute && info.PropertyType == typeof(string);
        }
        
        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFPasswordElement(info, (PasswordAttribute)attribute);
        }

        public string Value
        {
            get
            {
                if (Source is null)
                {
                    return string.Empty;
                }

                return PropertyInfo.GetValue(Source)
                                  ?.ToString();
            }
            set
            {

                //
                //
                PropertyInfo.SetValue(Source,
                                      Convert.ChangeType(value,
                                                         PropertyInfo.PropertyType));
                //
                // 通知Form
                Owner.TryFinish();

                RaiseUpdated();
            }
        }
        public override CFElement Clone()
        {
            return new CFPasswordElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
            };
        }
    }
}