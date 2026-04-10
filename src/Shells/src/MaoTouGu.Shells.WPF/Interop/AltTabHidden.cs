// ----------------------------------------------------------
//            文件：AltTabHidden.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 17:48
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace MaoTouGu.Shells.Win32
{
    public static partial class AltTabHidden
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            var    error = 0;
            IntPtr result;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                int tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error  = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error  = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int IntSetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);

        public static void HideAltTab(Window window)
        {
            var windowInterop = new WindowInteropHelper(window);
            var exStyle       = GetWindowLong(windowInterop.Handle, -20);


            exStyle |= 0x00000080;
            SetWindowLong(windowInterop.Handle, -20, exStyle);
        }
    }
}