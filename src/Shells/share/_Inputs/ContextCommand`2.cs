// ----------------------------------------------------------
//            文件：ContextCommand`2.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 15:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Inputs
{
    public abstract class ContextCommand<TItem, TContext>(TContext target) : ContextCommand<TContext>(target) where TItem : class
    {
        public sealed override bool CanExecute(object parameter) => parameter is TItem;

        public sealed override void Execute(object parameter)
        {
            if (parameter is TItem item)
            {
                Execute(item);
            }
        }

        protected abstract void Execute(TItem target);
    }
}