// ----------------------------------------------------------
//            文件：CFStringObjectElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 19:16
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public sealed class CFStringObjectElement : CFElement, IValueSource<string>
    {
        internal CFStringObjectElement()
        {
        }

        private CFStringObjectElement(PropertyInfo propertyInfo, ObjectAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
            PropertyName = propertyInfo.Name;
            DataType     = propertyInfo.DeclaringType;
            Open         = new DelegateCommand(DoOpenCommand);
        }

        private async void DoOpenCommand()
        {
            await Owner.GetObjectContext(DataType, PropertyName);
        }

        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is ObjectAttribute && info.PropertyType == typeof(string);
        }

        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFStringObjectElement(info, (ObjectAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFStringObjectElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
                PropertyName = PropertyName,
                DataType     = DataType,
            };
        }

        public string PropertyName { get; private init; }
        public Type   DataType     { get; private init; }

        public ICommandEX Open { get; }

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

                //
                //
                RaiseUpdated();
            }
        }
    }
}