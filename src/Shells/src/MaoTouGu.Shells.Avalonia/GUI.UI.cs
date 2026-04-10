namespace MaoTouGu.Shells
{
    partial class GUI
    {
        internal static SynchronizationContext _context;

        public static void SetSynchronizationContext()
        {
            _context = SynchronizationContext.Current;
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
    }
}