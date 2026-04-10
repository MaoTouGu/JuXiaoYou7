// ----------------------------------------------------------
//            文件：CFSelectorElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月24日 16:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    public abstract class CFSelectorElement : CFElement
    {
        
        internal CFSelectorElement() : base()
        {

        }

        protected CFSelectorElement(PropertyInfo propertyInfo, CFEAttribute attribute) : base(attribute)
        {
            PropertyInfo = propertyInfo;
            PropertyName = propertyInfo.Name;
            DataType     = propertyInfo.DeclaringType;
            IsEnum       = propertyInfo.PropertyType.IsEnum;
        }

        public override bool CanAccept(CFEAttribute attribute, PropertyInfo info) => info.PropertyType is not null  &&
                                                                                     !info.PropertyType.IsPrimitive &&
                                                                                     !info.PropertyType.IsMarshalByRef;

        public override void Initialize()
        {
            if (Owner is null)
            {
                return;
            }


            if (IsEnum)
            {
                Handler = new EnumListBoxHandler(PropertyInfo.PropertyType);
            }
            else
            {
                Handler = Owner?.GetHandler(DataType, PropertyName);
            }

            //
            //
            ItemsSource = Handler?.ItemsSource;
            Template    = Handler?.Template;
            
            //
            //
            RaiseUpdated(nameof(ItemsSource));
            RaiseUpdated(nameof(Template));
        }

        public bool   IsEnum       { get; protected set; }
        public string PropertyName { get; protected set; }
        public Type   DataType     { get; protected set; }

        public IEnumerable<object> ItemsSource { get; protected set; }

        public ICFListBoxHandler Handler { get; private set; }

        public object SelectedItemInternal
        {
            get
            {
                if (Source is null)
                {
                    return null;
                }
                return PropertyInfo.GetValue(Source);
            }
            set
            {

                if (Source is null)
                {
                    return;
                }

                PropertyInfo.SetValue(Source, value);
            }
        }

        public object SelectedItem
        {
            get
            {
                if (Handler.ObjectSelector is null)
                {
                    return SelectedItemInternal;
                }

                return Handler.ObjectSelector(ItemsSource, SelectedItemInternal);
            }
            set
            {
                if (Handler.ValueSelector is null)
                {
                    SelectedItemInternal = value;
                }
                else
                {
                    SelectedItemInternal = Handler.ValueSelector(value);
                }

                RaiseUpdated();
            }
        }
        

        public DataTemplate Template { get; set; }
    }
}