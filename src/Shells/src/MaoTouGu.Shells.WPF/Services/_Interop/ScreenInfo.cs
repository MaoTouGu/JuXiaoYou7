
namespace MaoTouGu.Shells.Interops
{
    public sealed class ScreenInfo : ObservableObject
    {
        /// <summary>
        /// 是否为主屏幕。
        /// </summary>
        public bool IsPrimaryScreen { get; internal set; }
        
        public IntPtr hMonitor { get; internal set; }
        
        public int Left { get; internal set; }
        public int Right { get; internal set; }
        public int Top { get; internal set; }
        public int Bottom { get; internal set; }
        
        /// <summary>
        /// 高度。
        /// </summary>
        public int Height { get; internal set; }
        
        /// <summary>
        /// 宽度。
        /// </summary>
        public int Width { get; internal set; }
        
        /// <summary>
        /// 
        /// </summary>
        public uint DpiX { get; internal set; }
        
        /// <summary>
        /// 
        /// </summary>
        public uint DpiY { get; internal set; }
        
        /// <summary>
        /// 真实高度。
        /// </summary>
        public int ActualHeight { get; internal set; }
        
        /// <summary>
        /// 真实宽度。
        /// </summary>
        public int ActualWidth { get; internal set; }
        
        
        /// <summary>
        /// 真实高度。
        /// </summary>
        public int WorkAreaHeight { get; internal set; }
        
        /// <summary>
        /// 真实宽度。
        /// </summary>
        public int WorkAreaWidth { get; internal set; }

        public override string ToString()
        {
            return $"Dpi: {(DpiX /96d):f2} x {DpiY /96d:f2}\n{Left},{Right},{Top},{Bottom}\n分辨率: {Width}x{Height}\n真实分辨率: {ActualWidth}x{ActualHeight}\n工作区: {WorkAreaWidth}x{WorkAreaHeight}";
        }
    }
}