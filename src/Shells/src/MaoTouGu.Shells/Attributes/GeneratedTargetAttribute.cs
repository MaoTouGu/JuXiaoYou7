namespace MaoTouGu.Shells.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class GeneratedTargetAttribute : Attribute
    {
        public GeneratedTargetAttribute(string className){}
    }
}