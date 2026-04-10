namespace MaoTouGu.Shells.Core
{
    public class EnumPickerRoot<T> : PickerRoot<T> where T : struct, Enum
    {
        public EnumPickerRoot() : base(ClassStatic.GetEnums<T>())
        {
        }
        
        public EnumPickerRoot(Predicate<T> expression) : base(ClassStatic.GetEnums<T>()
                                                                         .ToArray()
                                                                         .Where(x => expression(x)))
        {
        }
        
        public EnumPickerRoot(IEnumerable<T> collection) : base(collection)
        {
        }
        
        public bool IsComboBoxStyle { get; init; }
    }
}