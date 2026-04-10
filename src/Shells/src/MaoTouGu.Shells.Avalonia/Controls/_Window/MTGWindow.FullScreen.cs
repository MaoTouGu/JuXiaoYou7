using System.Runtime.InteropServices;
using MaoTouGu.Shells.Interops;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace MaoTouGu.Shells.Controls
{
    partial class MTGWindow
    {
        //  private const int WM_SIZE          = 0x0005; 
        //  private const int WM_GetMinMaxInfo =  0x0024; 
        //
        // [DllImport("user32")]
        // private static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);
        //
        // /// <summary>
        // ///
        // /// </summary>
        // [DllImport("User32")]
        // private static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        //
        //
        // [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        // private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        // {
        //     var mmi     = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
        //     var monitor = MonitorFromWindow(hwnd, 0x00000002);
        //
        //     if (monitor != IntPtr.Zero)
        //     {
        //         var monitorInfo = new MONITORINFO();
        //         GetMonitorInfo(monitor, monitorInfo);
        //         var rcWorkArea    = monitorInfo.rcWork;
        //         var rcMonitorArea = monitorInfo.rcMonitor;
        //         mmi.ptMaxPosition.x  = Math.Abs(rcWorkArea.left   - rcMonitorArea.left);
        //         mmi.ptMaxPosition.y  = Math.Abs(rcWorkArea.top    - rcMonitorArea.top);
        //         mmi.ptMaxSize.x      = Math.Abs(rcWorkArea.right  - rcWorkArea.left);
        //         mmi.ptMaxSize.y      = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
        //         mmi.ptMinTrackSize.x = (int)MinWidth;
        //         mmi.ptMinTrackSize.y = (int)MinHeight;
        //     }
        //
        //     Marshal.StructureToPtr(mmi, lParam, true);
        // }
        
        //
        // TODO:
        //
        // https://github.com/dotnet/wpf/blob/master/src/Microsoft.DotNet.Wpf/src/PresentationFramework/System/Windows/Window.cs#L7358
        //
        // internal Point DeviceToLogicalUnits(Point ptDeviceUnits)
        // {
        //     Point ptLogicalUnits = CompositionTarget.TransformFromDevice.Transform(ptDeviceUnits);
        //     return ptLogicalUnits;
        // }
        //
        // internal virtual WindowMinMax GetWindowMinMax()
        // {
        //     WindowMinMax mm = new WindowMinMax( );
        //
        //     Invariant.Assert(!IsCompositionTargetInvalid, "IsCompositionTargetInvalid is supposed to be false here");
        //
        //     // convert the max/min size (taken in to account the hwnd size restrictions by win32) into logical units
        //     double maxWidthDeviceUnits = _trackMaxWidthDeviceUnits;
        //     double maxHeightDeviceUnits = _trackMaxHeightDeviceUnits;
        //     if (WindowState == WindowState.Maximized)
        //     {
        //         // On some systems, the trackMax size is a few pixels smaller than
        //         // the windowMax size.   Use the larger size for maximized windows.
        //         maxWidthDeviceUnits = Math.Max(_trackMaxWidthDeviceUnits, _windowMaxWidthDeviceUnits);
        //         maxHeightDeviceUnits = Math.Max(_trackMaxHeightDeviceUnits, _windowMaxHeightDeviceUnits);
        //     }
        //
        //     Point maxSizeLogicalUnits = DeviceToLogicalUnits(new Point(maxWidthDeviceUnits, maxHeightDeviceUnits));
        //     Point minSizeLogicalUnits = DeviceToLogicalUnits(new Point(_trackMinWidthDeviceUnits, _trackMinHeightDeviceUnits));
        //
        //     //
        //     // Get the final Min/Max Width
        //     //
        //     mm.minWidth = Math.Max(this.MinWidth, minSizeLogicalUnits.X);
        //
        //     // Min's precedence is higher than Max; If Min is greater than Max, use Min.
        //     if (MinWidth > MaxWidth)
        //     {
        //         mm.maxWidth = Math.Min(MinWidth, maxSizeLogicalUnits.X);
        //     }
        //     else
        //     {
        //         if (!Double.IsPositiveInfinity(MaxWidth))
        //         {
        //             mm.maxWidth = Math.Min(MaxWidth, maxSizeLogicalUnits.X);
        //         }
        //         else
        //         {
        //             mm.maxWidth = maxSizeLogicalUnits.X;
        //         }
        //     }
        //
        //     //
        //     // Get the final Min/Max Height
        //     //
        //     mm.minHeight = Math.Max(this.MinHeight, minSizeLogicalUnits.Y);
        //
        //     // Min's precedence is higher than Max; If Min is greater than Max, use Min.
        //     if (MinHeight > MaxHeight)
        //     {
        //         mm.maxHeight = Math.Min(this.MinHeight, maxSizeLogicalUnits.Y);
        //     }
        //     else
        //     {
        //         if (!Double.IsPositiveInfinity(MaxHeight))
        //         {
        //             mm.maxHeight = Math.Min(MaxHeight, maxSizeLogicalUnits.Y);
        //         }
        //         else
        //         {
        //             mm.maxHeight = maxSizeLogicalUnits.Y;
        //         }
        //     }
        //
        //     return mm;
        // }

        #region Nested type: MINMAXINFO

        [StructLayout(LayoutKind.Sequential)]
        internal struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        #endregion

        #region Nested type: MONITORINFO

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class MONITORINFO
        {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor;

            /// <summary>
            /// </summary>            
            public RECT rcWork;

            /// <summary>
            /// </summary>            
            public int dwFlags;
        }

        #endregion

        #region Nested type: POINT

        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;

            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        #endregion
        
        internal struct WindowMinMax
        {
            internal double minWidth;
            internal double maxWidth;
            internal double minHeight;
            internal double maxHeight;

            internal WindowMinMax(double minSize, double maxSize)
            {
                minWidth  = minSize;
                maxWidth  = maxSize;
                minHeight = minSize;
                maxHeight = maxSize;
            }
        }


        private static void OnWindowStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = ((MTGWindow)d);

            if (window.MaximumButton is null)
            {
                return;
            }

            window.MaximumButton.WindowState = (WindowState)e.NewValue;
        }
        
        public static bool IsFullScreen { get; set; }
    }
}