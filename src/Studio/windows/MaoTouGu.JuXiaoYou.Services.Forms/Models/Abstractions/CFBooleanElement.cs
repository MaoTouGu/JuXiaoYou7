// ----------------------------------------------------------
//            文件：CFBooleanElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public abstract class CFBooleanElement : CFElement, IValueSource<bool>
    {
        internal CFBooleanElement()
        {
        }

        protected CFBooleanElement(PropertyInfo propertyInfo, CFEAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
        }

        public bool Value
        {
            get
            {
                if (Source is null)
                {
                    return false;
                }

                return PropertyInfo.GetValue(Source) is bool b && b;
            }
            set
            {

                //
                //
                PropertyInfo.SetValue(Source, value);
                //
                // 通知Form
                Owner.TryFinish();

                RaiseUpdated();
            }
        }
    }
}