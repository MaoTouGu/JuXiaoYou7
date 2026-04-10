using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KinonekoSoftware.UI.Controls.Panels
{
    /// <summary>
    ///     UnifiedColumnPanel
    /// </summary>
    /// <remarks>
    ///     当不需要Grid的行、列分隔等功能时建议用此轻量级类代替
    /// </remarks>
    public class UnifiedColumnPanel : Panel
    {

        public static readonly DependencyProperty DesiredWidthProperty =
            DependencyProperty.Register(
                                        nameof(DesiredWidth),
                                        typeof(double),
                                        typeof(UnifiedColumnPanel),
                                        new FrameworkPropertyMetadata(160d, FrameworkPropertyMetadataOptions.AffectsMeasure |
                                            FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty DesiredGapProperty =
            DependencyProperty.Register(
                                        nameof(DesiredGap),
                                        typeof(double),
                                        typeof(UnifiedColumnPanel),
                                        new FrameworkPropertyMetadata(8d, FrameworkPropertyMetadataOptions.AffectsMeasure |
                                            FrameworkPropertyMetadataOptions.AffectsArrange));

        private readonly List<LayoutRow> _layoutRows = new List<LayoutRow>();

        class LayoutRow : Collection<UIElement>
        {
            protected override void InsertItem(int index, UIElement item)
            {
                if (item is null)
                {
                    return;
                }

                H = Math.Max(H, item.DesiredSize.Height);
                W = Math.Max(DesiredWidth, item.DesiredSize.Width);

                if (double.IsInfinity(H) || double.IsNaN(H))
                {
                    H = 0;
                }       
                
                if (double.IsInfinity(W) || double.IsNaN(W))
                {
                    W = 0;
                }

                base.InsertItem(index, item);
            }

            public double H            { get; private set; }
            public double W            { get; private set; }
            public double DesiredWidth { get; init; }
        }

        protected override Size MeasureOverride(Size availableSize)
        {

            double w;

            var desiredWidth  = DesiredWidth;
            var desiredWidth2 = double.IsInfinity(desiredWidth) || double.IsNaN(desiredWidth) ? 10 : desiredWidth;
            var desiredWidth3 = Math.Clamp(desiredWidth2, 10, short.MaxValue);

            var h = 0d;
            var i = 0;
            //
            //
            _layoutRows.Clear();

            //
            //
            if (double.IsInfinity(availableSize.Width) || double.IsNaN(availableSize.Width))
            {
                //
                //
                w = SystemParameters.PrimaryScreenWidth;
            }
            else
            {
                w = availableSize.Width;
            }



            //
            //
            var column       = (int)(w                                     / desiredWidth3);
            var remain       = availableSize.Width               - (column * desiredWidth3);
            var resizedWidth = desiredWidth3 + (remain / column) - Margin.Left - Margin.Right;
            var row          = new LayoutRow { DesiredWidth = resizedWidth };

            foreach (UIElement element in InternalChildren)
            {
                if (double.IsNaN(resizedWidth) || double.IsInfinity(resizedWidth))
                {
                    resizedWidth = desiredWidth3;
                }

                element.InvalidateMeasure();
                element.Measure(new Size(resizedWidth, double.PositiveInfinity));

                if (i + 1 > column)
                {

                    _layoutRows.Add(row);
                    h   += row.H;
                    i   =  0;
                    row =  new LayoutRow { DesiredWidth = resizedWidth };
                }

                row.Add(element);
                i++;
            }

            if (row.Count > 0)
            {
                _layoutRows.Add(row);
                h += row.H;
            }

            var actualRow = Math.Clamp(_layoutRows.Count - 1, 0, int.MaxValue);
            return new Size(w, h + actualRow * DesiredGap);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var y             = 0d;
            var desiredWidth  = DesiredWidth;
            var desiredWidth2 = double.IsInfinity(desiredWidth) || double.IsNaN(desiredWidth) ? 10 : desiredWidth;
            var desiredWidth3 = Math.Clamp(desiredWidth2, 10, short.MaxValue);
            var desiredHeight = DesiredGap;

            foreach (var row in _layoutRows)
            {
                var x = 0d;

                for (var i = 0; i < row.Count; i++)
                {
                    var column = row[i];
                    x = i * row.W;

                    //
                    //
                    column.Arrange(new Rect(x, y, row.W, row.H));
                }

                y += row.H + desiredHeight;
            }

            return new Size(finalSize.Width, y);
        }

        public double DesiredGap
        {
            get => (double)GetValue(DesiredGapProperty);
            set => SetValue(DesiredGapProperty, value);
        }

        public double DesiredWidth
        {
            get => (double)GetValue(DesiredWidthProperty);
            set => SetValue(DesiredWidthProperty, value);
        }
    }
}