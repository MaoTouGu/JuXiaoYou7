// ----------------------------------------------------------
//            文件：TextAttribute.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 22:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Attributes.CFE
{

    /// <summary>
    /// 文本属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class TextAttribute : CFEAttribute
    {

        public TextAttribute(bool isMultiple, string name) : this(isMultiple, false, name)
        {

        }

        public TextAttribute(bool isMultiple, bool i18n, string name) : base(i18n, name)
        {
            IsMultipleLine = isMultiple;
        }

        public bool IsMultipleLine { get; init; }
    }

    /// <summary>
    /// 文本属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class PasswordAttribute : CFEAttribute
    {
        public PasswordAttribute(string name) : this(false, name)
        {

        }

        public PasswordAttribute(bool i18n, string name) : base(i18n, name)
        {

        }
    }

    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class RangeAttribute : CFEAttribute
    {
        public RangeAttribute(string name) : this(false, name)
        {

        }

        public RangeAttribute(bool i18n, string name) : base(i18n, name)
        {

        }
    }
    
    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ToggleSwitchAttribute : CFEAttribute
    {
        public ToggleSwitchAttribute(string name) : this(false, name)
        {

        }

        public ToggleSwitchAttribute(bool i18n, string name) : base(i18n, name)
        {

        }
    }   
    
    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class CheckBoxAttribute : CFEAttribute
    {
        public CheckBoxAttribute(string name) : this(false, name)
        {

        }

        public CheckBoxAttribute(bool i18n, string name) : base(i18n, name)
        {

        }
    }
    
    /// <summary>
    /// 范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ColorAttribute : CFEAttribute
    {
        public ColorAttribute(string name) : this(false, name)
        {

        }

        public ColorAttribute(bool i18n, string name) : base(i18n, name)
        {

        }
    }
}