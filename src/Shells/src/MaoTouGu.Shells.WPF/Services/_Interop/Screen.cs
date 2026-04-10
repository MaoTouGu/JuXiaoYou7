using System.Runtime.InteropServices;

namespace MaoTouGu.Shells.Interops
{
    public static class Screen
    {
        private static readonly List<ScreenInfo> _screen;

        static Screen()
        {
            _screen = new List<ScreenInfo>(8);
        }

        //-------------------------------------------------------------
        //
        //          Extern Methods
        //
        //-------------------------------------------------------------
        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [DllImport("Shcore.dll")]
        private static extern int GetDpiForMonitor(IntPtr hmonitor, DpiType dpiType, out uint dpiX, out uint dpiY);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern int GetSystemMetrics(int nIndex);
        
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        private static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

        //-------------------------------------------------------------
        //
        //          Delegates
        //
        //-------------------------------------------------------------
        private delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        //-------------------------------------------------------------
        //
        //          Private Methods
        //
        //-------------------------------------------------------------
        private static readonly bool MultiMonitorSupport = GetSystemMetrics(80) != 0;
        


        public static IEnumerable<ScreenInfo> GetScreens()
        {
            //
            //
            _screen.Clear();

            //
            //
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnum, IntPtr.Zero);

            return _screen.ToArray();
        }

        private static bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            var mi = new MONITORINFO();
            var si = new ScreenInfo();

            mi.cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            var result = GetDpiForMonitor(hMonitor, DpiType.Effective, out var dpiX, out var dpiY);

            if (result == 0 && GetMonitorInfo(hMonitor, ref mi))
            {
                si.DpiX   = dpiX;
                si.DpiY   = dpiY;
                si.Left   = mi.rcMonitor.left;
                si.Right  = mi.rcMonitor.right;
                si.Top    = mi.rcMonitor.top;
                si.Bottom = mi.rcMonitor.bottom;

                si.Width           = mi.rcMonitor.right  - mi.rcMonitor.left;
                si.Height          = mi.rcMonitor.bottom - mi.rcMonitor.top;
                si.IsPrimaryScreen = mi.dwFlags == 0;
                si.WorkAreaWidth   = mi.rcWork.right  - mi.rcWork.left;
                si.WorkAreaHeight  = mi.rcWork.bottom - mi.rcWork.top;
                si.hMonitor        = hMonitor;
                si.ActualWidth     = (int)(si.Width  / dpiX * 96);
                si.ActualHeight    = (int)(si.Height / dpiX * 96);
            }

            _screen.Add(si);
            return true;
        }
    }
}