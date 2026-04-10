namespace MaoTouGu.Shells.Core
{
    public static class PickerExt
    {

        public static Task<Result<T>> PickEnum<T>(this PageBase target, bool comboBoxStyle = false) where T : struct, Enum
        {
            return Dialog.AddDialog(target, new EnumPickerRoot<T>
                          {
                              IsComboBoxStyle = comboBoxStyle,
                          })
                         .Awaitable;
        }

        public static Task<Result<T>> PickEnum<T>(this PageBase target, Predicate<T> expression, bool comboBoxStyle = false) where T : struct, Enum
        {
            if (expression is null)
            {
                return Task.FromResult(Result<T>.Failure);
            }

            return Dialog.AddDialog(target, new EnumPickerRoot<T>(expression)
                          {
                              IsComboBoxStyle = comboBoxStyle,
                          })
                         .Awaitable;
        }

        public static Task<Result<T>> PickEnum<T>(this PageBase target, IEnumerable<T> collection, bool comboBoxStyle = false) where T : struct, Enum
        {
            if (collection is null)
            {
                return Task.FromResult(Result<T>.Failure);
            }

            return Dialog.AddDialog(target, new EnumPickerRoot<T>(collection)
                          {
                              IsComboBoxStyle = comboBoxStyle,
                          })
                         .Awaitable;
        }

        public static Task<Result<T>> PickEnum<T>(this DialogBase target, bool comboBoxStyle = false) where T : struct, Enum
        {
            return Dialog.AddDialog(target, new EnumPickerRoot<T>
                          {
                              IsComboBoxStyle = comboBoxStyle,
                          })
                         .Awaitable;
        }

        public static Task<Result<T>> PickEnum<T>(this DialogBase target, Predicate<T> expression, bool comboBoxStyle = false) where T : struct, Enum
        {
            if (expression is null)
            {
                return Task.FromResult(Result<T>.Failure);
            }

            return Dialog.AddDialog(target, new EnumPickerRoot<T>(expression)
                          {
                              IsComboBoxStyle = comboBoxStyle,
                          })
                         .Awaitable;
        }

        public static Task<Result<T>> PickEnum<T>(this DialogBase target, IEnumerable<T> collection, bool comboBoxStyle = false) where T : struct, Enum
        {
            if (collection is null)
            {
                return Task.FromResult(Result<T>.Failure);
            }

            return Dialog.AddDialog(target, new EnumPickerRoot<T>(collection)
                          {
                              IsComboBoxStyle = comboBoxStyle,
                          })
                         .Awaitable;
        }

    }
}