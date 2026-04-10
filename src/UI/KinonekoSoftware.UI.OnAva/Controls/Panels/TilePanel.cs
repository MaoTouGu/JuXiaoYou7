using Avalonia;
using Avalonia.Controls;

namespace KinonekoSoftware.UI.Controls.Panels
{

    /// <summary>
    ///     用以代替Grid
    /// </summary>
    /// <remarks>
    ///     当不需要Grid的行、列分隔等功能时建议用此轻量级类代替
    /// </remarks>
    public class TilePanel : Panel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            var w = -100000d;
            var h = -100000d;

            foreach (var child in Children.Where(child => child != null))
            {
                child.Measure(constraint);
                w = Math.Max(w, child.DesiredSize.Width);
                h = Math.Max(h, child.DesiredSize.Height);
            }

            return new Size(w, h);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (var child in Children)
            {
                child?.Arrange(new Rect(arrangeSize));
            }

            return arrangeSize;
        }
    }
}