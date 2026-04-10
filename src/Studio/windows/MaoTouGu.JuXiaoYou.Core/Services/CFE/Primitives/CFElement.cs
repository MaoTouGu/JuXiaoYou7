// ----------------------------------------------------------
//            文件：CFElement.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 18:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Attributes.CFE;

namespace MaoTouGu.JuXiaoYou.Services.CFE
{
    /// <summary>
    /// <see cref="CFElement"/> 可组合的表单元素。
    /// </summary>
    public abstract class CFElement : ObservableObject, ICloneable<CFElement>
    {
        private object _source;
        
        protected CFElement()
        {

        }

        protected CFElement(CFEAttribute attribute)
        {
            if (attribute.UseI18N)
            {
                Name = I18N.GetText(attribute.Name);
            }
            else
            {
                Name = attribute.Name;
            }
        }

        public abstract bool CanAccept(CFEAttribute attribute, PropertyInfo info);

        public abstract CFElement Accept(CFEAttribute attribute, PropertyInfo info);

        public void Accept(CFAttribute attribute)
        {
            if (attribute is IndexAttribute ia)
            {
                Index = ia.Index;
            }

            if (attribute is CategoryAttribute ca)
            {
                if (ca.UseI18N)
                {
                    Category = I18N.GetText(ca.Category);
                }
                else
                {
                    Category = ca.Category;
                }
            }
        }

        public virtual void Initialize()
        {
            
        }

        public abstract CFElement Clone();

        public object Source
        {
            get => _source;
            set
            {
                _source = value;
                if (_source is not null)
                {
                    Initialize();
                }
            }
        }

        public PropertyInfo      PropertyInfo { get; protected set; }
        public ICompositableForm Owner        { get; set; }

        public string Name { get; protected set; }

        public string Category { get; protected set; }
        public int    Index    { get; protected set; }
    }
}