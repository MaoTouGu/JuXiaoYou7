using System.IO;
using MaoTouGu.Shells.Inputs;

namespace MaoTouGu.Shells
{
    public static partial class GUI
    {
        static void Do1xSnapshot(FrameworkElement target) => DoSnapshotCommand(target, 1d);
        static void Do2xSnapshot(FrameworkElement target) => DoSnapshotCommand(target, 2d);
        static void Do3xSnapshot(FrameworkElement target) => DoSnapshotCommand(target, 3d);
        static void Do4xSnapshot(FrameworkElement target) => DoSnapshotCommand(target, 4d);

        public static async Task Capture(FrameworkElement target, string fileName, int dpi)
        {
            dpi = Math.Clamp(dpi, 96, 960);
            var buffer = Xaml.CaptureToBuffer(target, dpi);
            await File.WriteAllBytesAsync(fileName, buffer);
        }
        
        static async void DoSnapshotCommand(FrameworkElement target, double ratio)
        {
            if (target is null)
            {
                return;
            }
        
            var r = Interop.SaveFileAsync("PNG图片|*.png", "png");
        
            if (!r.IsFinished)
            {
                return;
            }
        
        
            var dpi    = VisualTreeHelper.GetDpi(target).PixelsPerDip * 96 * ratio;
            var buffer = Xaml.CaptureToBuffer(target, (int)dpi);
            await File.WriteAllBytesAsync(r.Value, buffer);
        }
        
        
        
        public static ICommandEX Snapshot      { get; } = new DelegateCommand<FrameworkElement>(Do1xSnapshot, x => x is not null);
        public static ICommandEX Snapshot2xDPI { get; } = new DelegateCommand<FrameworkElement>(Do2xSnapshot, x => x is not null);
        public static ICommandEX Snapshot3xDPI { get; } = new DelegateCommand<FrameworkElement>(Do3xSnapshot, x => x is not null);
        public static ICommandEX Snapshot4xDPI { get; } = new DelegateCommand<FrameworkElement>(Do4xSnapshot, x => x is not null);
    }
}