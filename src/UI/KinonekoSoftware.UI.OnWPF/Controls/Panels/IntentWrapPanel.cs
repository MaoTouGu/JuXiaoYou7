using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace KinonekoSoftware.UI.Controls.Panels
{

    using System.Windows;
    using System.Windows.Controls;

    public class IntentWrapPanel : Panel
    {
        private bool _skip;
        private bool _measure;

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            _skip    = false;
            _measure = false;
            
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            if (_skip && _measure)
            {
                return;
            }

            base.OnChildDesiredSizeChanged(child);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetActualColumn(double w, double space, double iW, int count)
        {
           var maxC =  w > (iW + space) ? (int)(w / iW) : 1;
           if (maxC > count)
               return count;
           return maxC;
        }


        protected override Size MeasureOverride(Size constraint)
        {
            double h;
            double w;
            int    spaceColumn;
            var    space = Space;

            //
            // 第一次测量的时候，也就是这个Panel第一次初始化的时候
            // 会给一个长和宽都是无限大的Size
            // 尽量将所有的控件都布局在第一行
            if (double.IsInfinity(constraint.Width) && double.IsInfinity(constraint.Height))
            {
                h           = -1d;
                w           = -1d;
                spaceColumn = Math.Clamp(InternalChildren.Count - 1, 0, InternalChildren.Count);

                foreach (UIElement element in InternalChildren)
                {
                    element.Measure(constraint);
                    h = Math.Max(h, element.DesiredSize.Height);
                    w = Math.Max(w, element.DesiredSize.Width);
                }

                return new Size(w * InternalChildren.Count + spaceColumn * space, h);
            }

            //
            // 第二次布局的时候，这个Panel大小再次调整
            // 会给一个固定尺寸的宽度和无限大的高度
            // 那就很好办了
            var intentWidth = IntentWidth;
            var column      = GetActualColumn(constraint.Width, space, intentWidth, InternalChildren.Count);
            spaceColumn = Math.Clamp(column - 1, 0, column);

            w = (constraint.Width - (spaceColumn * space)) / column;
            h = -1d;

            var size = new Size(w, constraint.Height);

            foreach (UIElement element in InternalChildren)
            {
                element.Measure(size);
                h = Math.Max(h, element.DesiredSize.Height);
            }

            var row      = InternalChildren.Count == 0 ? 0 : (InternalChildren.Count + spaceColumn) / column;
            var spaceRow = Math.Clamp(row - 1, 0, row);

            return new Size(
                constraint.Width,
                h * row + spaceRow * space);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var intentWidth = IntentWidth;
            var space       = Space;
            var column      = GetActualColumn(finalSize.Width, space, intentWidth, InternalChildren.Count);
            var spaceColumn = Math.Clamp(column - 1, 0, column);
            var w           = (finalSize.Width - (spaceColumn * space)) / column;
            var top         = 0d;
            var offset      = 0d;


            _measure = true;
            _skip    = true;

            for (var i = 0; i < InternalChildren.Count;)
            {
                var h = 0d;

                for (var n = 0; n < column && n + i < InternalChildren.Count; n++)
                {
                    h = Math.Max(h, InternalChildren[n + i].DesiredSize.Height);
                }

                for (var n = 0; n < column && i < InternalChildren.Count;)
                {
                    var element = InternalChildren[i];
                    var x       = i % column;
                    var rect    = new Rect(x * w + offset, top, w, h);
                    element.Arrange(rect);
                    offset += space;
                    n++;
                    i++;
                }

                offset =  0;
                top    += h + space;
            }
            return finalSize;
        }


        public static readonly DependencyProperty IntentWidthProperty = DependencyProperty.Register(
            nameof(IntentWidth),
            typeof(double),
            typeof(IntentWrapPanel),
            new FrameworkPropertyMetadata(320d, FrameworkPropertyMetadataOptions.AffectsMeasure));


        public static readonly DependencyProperty SpaceProperty = DependencyProperty.Register(
            nameof(Space),
            typeof(double),
            typeof(IntentWrapPanel),
            new FrameworkPropertyMetadata(16d, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Space
        {
            get => (double)GetValue(SpaceProperty);
            set => SetValue(SpaceProperty, value);
        }

        public double IntentWidth
        {
            get => (double)GetValue(IntentWidthProperty);
            set => SetValue(IntentWidthProperty, value);
        }

    }
}