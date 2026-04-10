// ----------------------------------------------------------
//            文件：CFUploadElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFUploadElement : CFElement, IValueSource<string>
    {

        internal CFUploadElement()
        {
        }

        private CFUploadElement(PropertyInfo propertyInfo, UploadAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
            Type         = attribute.Type;
            Pick         = new DelegateCommand(DoPick);
        }
        
        private async void DoPick()
        {
            throw new NotImplementedException();
        }


        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is UploadAttribute && info.PropertyType == typeof(string);
        }
        
        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFUploadElement(info, (UploadAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFUploadElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
                Type         = Type,
            };
        }
        
        public AssetType Type { get; private init; }
        
        public ICommandEX Pick { get; }
        
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
                PropertyInfo.SetValue(Source, value);
                
                //
                // 通知Form
                Owner.TryFinish();

                RaiseUpdated();
            }
        }
    }
}