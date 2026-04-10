namespace MaoTouGu.Shells.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class AssociateAttribute : Attribute
    {
        public Type View { get; init; }

        public Type ViewModel { get; init; }
    }
}