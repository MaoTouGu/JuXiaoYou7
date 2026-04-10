

namespace MaoTouGu.Shells.Core
{
    public static class ObjectExt
    {
        public static Task Flyout(this DialogBase target, FlyoutRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }

        public static Task<Result<T>> Object<T, TViewModel>(this DialogBase target) where TViewModel : ObjectRoot<T>, new()
        {
            var vm = ClassStatic.CreateInstance<TViewModel>();
                
            return Dialog.AddDialog(target, vm).Awaitable;
        }

        public static Task<Result<T>> Object<T>(this DialogBase target,ObjectRoot<T> root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }
            
        public static Task<Result<string>> MultiLine(this DialogBase target,string title, string description, string value = null)
        {
            return Dialog.AddDialog(target, new TextInputRoot(title, description, value, true)).Awaitable;
        }
        
        public static Task<Result<string>> SingleLine(this DialogBase target,string title, string description, string value = null)
        {
            return Dialog.AddDialog(target, new TextInputRoot(title, description, value, false)).Awaitable;
        }

        public static Task<Result<int>> Range(this DialogBase target,string title, string description, int max)
        {
            return Dialog.AddDialog(target, new RangeInputRoot(title, description, 0, max, 1)).Awaitable;
        }
        
        public static Task Flyout(this PageBase target, FlyoutRoot root)
        {
                
            return Dialog.AddDialog(target, root).Awaitable;
        }

        public static Task<Result<T>> Object<T, TViewModel>(this PageBase target) where TViewModel : ObjectRoot<T>, new()
        {
            var vm = ClassStatic.CreateInstance<TViewModel>();
                
            return Dialog.AddDialog(target, vm).Awaitable;
        }
            
        public static Task<Result<T>> Object<T>(this PageBase target,ObjectRoot<T> root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }
            
        public static Task<Result<string>> MultiLine(this PageBase target,string title, string description, string value = null)
        {
            return Dialog.AddDialog(target, new TextInputRoot(title, description, value, true)).Awaitable;
        }
        
        public static Task<Result<string>> SingleLine(this PageBase target,string title, string description, string value = null)
        {
            return Dialog.AddDialog(target, new TextInputRoot(title, description, value, false)).Awaitable;
        }

        public static Task<Result<int>> Range(this PageBase target,string title, string description, int max)
        {
            return Dialog.AddDialog(target, new RangeInputRoot(title, description, 0, max, 1)).Awaitable;
        }
    }
}