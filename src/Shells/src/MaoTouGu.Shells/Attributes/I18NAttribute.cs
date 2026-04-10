namespace MaoTouGu.Shells.Attributes
{
    /// <summary>
    /// <see cref="I18NAttribute"/> 用于为源代码生成器指示当前的枚举可以被识别。
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = true)]
    public class I18NAttribute : Attribute
    {
        
    }
}