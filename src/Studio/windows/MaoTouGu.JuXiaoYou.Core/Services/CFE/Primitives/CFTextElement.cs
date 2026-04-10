// ----------------------------------------------------------
//            文件：CFTextElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 15:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public abstract class CFTextElement : CFElement, IValueSource<string>
    {
        internal CFTextElement()
        {
        }

        protected CFTextElement(PropertyInfo propertyInfo, TextAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
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
    }
}