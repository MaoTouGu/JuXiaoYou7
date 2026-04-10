// ----------------------------------------------------------
//            文件：TupleContextCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月23日 17:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Shells.Inputs
{
    public abstract class TupleContextCommand<T1, T2, TContext>(TContext target) : ContextCommand<TContext>(target)
    {
        public sealed override bool CanExecute(object parameter) => parameter is Tuple<T1, T2>;

        public sealed override void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2> item)
            {
                Execute(item.Item1,
                        item.Item2);
            }
        }

        protected abstract void Execute(T1 item1, T2 item2);
    }

    public abstract class TupleContextCommand<T1, T2, T3, TContext>(TContext target) : ContextCommand<TContext>(target)
    {
        public sealed override bool CanExecute(object parameter) => parameter is Tuple<T1, T2, T3>;

        public sealed override void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2, T3> item)
            {
                Execute(item.Item1,
                        item.Item2,
                        item.Item3);
            }
        }

        protected abstract void Execute(T1 item1, T2 item2, T3 item3);
    }

    public abstract class TupleContextCommand<T1, T2, T3, T4, TContext>(TContext target) : ContextCommand<TContext>(target)
    {
        public sealed override bool CanExecute(object parameter) => parameter is Tuple<T1, T2, T3, T4>;

        public sealed override void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2, T3, T4> item)
            {
                Execute(item.Item1,
                        item.Item2,
                        item.Item3,
                        item.Item4);
            }
        }

        protected abstract void Execute(T1 item1, T2 item2, T3 item3, T4 item4);
    }

    public abstract class TupleContextCommand<T1, T2, T3, T4, T5, TContext>(TContext target) : ContextCommand<TContext>(target)
    {
        public sealed override bool CanExecute(object parameter) => parameter is Tuple<T1, T2, T3, T4, T5>;

        public sealed override void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2, T3, T4, T5> item)
            {
                Execute(item.Item1,
                        item.Item2,
                        item.Item3,
                        item.Item4,
                        item.Item5);
            }
        }

        protected abstract void Execute(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5);
    }

    public abstract class TupleContextCommand<T1, T2, T3, T4, T5, T6, TContext>(TContext target) : ContextCommand<TContext>(target)
    {
        public sealed override bool CanExecute(object parameter) => parameter is Tuple<T1, T2, T3, T4, T5, T6>;

        public sealed override void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2, T3, T4, T5, T6> item)
            {
                Execute(item.Item1,
                        item.Item2,
                        item.Item3,
                        item.Item4,
                        item.Item5,
                        item.Item6);
            }
        }

        protected abstract void Execute(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6);
    }

    public abstract class TupleContextCommand<T1, T2, T3, T4, T5, T6, T7, TContext>(TContext target) : ContextCommand<TContext>(target)
    {
        public sealed override bool CanExecute(object parameter) => parameter is Tuple<T1, T2, T3, T4, T5, T6, T7>;

        public sealed override void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2, T3, T4, T5, T6, T7> item)
            {
                Execute(item.Item1,
                        item.Item2,
                        item.Item3,
                        item.Item4,
                        item.Item5,
                        item.Item6,
                        item.Item7);
            }
        }

        protected abstract void Execute(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7);
    }
}