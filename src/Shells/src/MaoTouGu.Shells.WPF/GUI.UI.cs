using System.Windows.Threading;
using MaoTouGu.Shells.Threadings;

namespace MaoTouGu.Shells
{
    sealed class ThreadingInvokerImpl : IThreadingInvoker
    {

        public void RunOnUIThread(Action callback) => GUI.RunOnUIThread(callback);
    }


    partial class GUI
    {
        internal static SynchronizationContext _context;

        public static void SetSynchronizationContext()
        {
            _context = SynchronizationContext.Current;

            Ioc.Use<IThreadingInvoker>(new ThreadingInvokerImpl());
        }

        //-------------------------------------------------------------
        //
        //                          Threading
        //
        //-------------------------------------------------------------

        public static bool IsUIThread() => SynchronizationContext.Current == _context;

        /// <summary>
        /// 在UI上下文中执行指定的操作。
        /// </summary>
        /// <param name="callback">要执行的操作。</param>
        public static void RunOnUIThread(Action callback)
        {
            if (callback is null)
            {
                return;
            }

            if (IsUIThread())
            {
                callback();
            }
            else
            {
                _context.Send(_ => callback(), null);
            }
        }


        // /// <summary>
        // /// 在UI上下文中执行指定的操作。
        // /// </summary>
        // /// <param name="callback">要执行的操作。</param>
        // public static T RunOnUIThread<T>(Func<T> callback)
        // {
        //     if (callback is null)
        //     {
        //         return default;
        //     }
        //
        //     T r;
        //
        //     if (IsUIThread())
        //     {
        //         r = callback();
        //     }
        //     else
        //     {
        //         return Dispatcher.CurrentDispatcher
        //                          .Invoke(callback, DispatcherPriority.Normal);
        //     }
        //
        //     return r;
        // }
    }
}