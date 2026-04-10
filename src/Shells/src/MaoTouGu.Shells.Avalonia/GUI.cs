using System.IO;
using MaoTouGu.Shells.Inputs;

namespace MaoTouGu.Shells
{
    public static partial class GUI
    {
        // static async void DoSnapshotCommand(FrameworkElement target)
        // {
        //     if (target is null)
        //     {
        //         return;
        //     }
        //
        //     var r = Interop.SaveFileAsync("PNG图片|*.png", "png");
        //
        //     if (!r.IsFinished)
        //     {
        //         return;
        //     }
        //
        //
        //     var dpi    = VisualTreeHelper.GetDpi(target).PixelsPerDip * 96;
        //     var buffer = Xaml.CaptureToBuffer(target, (int)dpi);
        //     await File.WriteAllBytesAsync(r.Value, buffer);
        // }
        //
        // public static ICommandEX Snapshot { get; } = new CommandEX<FrameworkElement>(DoSnapshotCommand);
    }
}