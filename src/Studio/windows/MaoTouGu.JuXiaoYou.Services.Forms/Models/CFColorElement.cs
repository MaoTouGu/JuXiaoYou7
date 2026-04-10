// ----------------------------------------------------------
//            文件：CFColorElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------



namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public class CFColorElement : CFElement, IValueSource<string>
    {
        private Color _color;

        internal CFColorElement()
        {
        }

        private CFColorElement(PropertyInfo propertyInfo, ColorAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
        }



        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info)
        {
            return attribute is ColorAttribute && info.PropertyType == typeof(string);
        }

        public override CFElement Accept(CFEAttribute attribute, PropertyInfo info)
        {
            return new CFColorElement(info, (ColorAttribute)attribute);
        }

        public override CFElement Clone()
        {
            return new CFColorElement
            {
                PropertyInfo = PropertyInfo,
                Name         = Name,
                Category     = Category,
                Index        = Index,
            };
        }

        public override void Initialize()
        {
            _color = Xaml.ToColor(Value);
        }

        public Color Color
        {
            get => _color;
            set
            {
                SetValue(ref _color, value);
                Value = value.ToString();
            }
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
                PropertyInfo.SetValue(Source, value);

                //
                // 通知Form
                Owner.TryFinish();

                RaiseUpdated();
            }
        }
    }
}