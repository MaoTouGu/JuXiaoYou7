using System.Runtime.InteropServices;

namespace MaoTouGu.Shells.Interops
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MONITORINFO
    {
        public int  cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MONITORINFOEX
    {
        internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));

        internal RECT rcMonitor;

        internal RECT rcWork;

        internal int dwFlags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        internal char[] szDevice = new char[32];
    }
}