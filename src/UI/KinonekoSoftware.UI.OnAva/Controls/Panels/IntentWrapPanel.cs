using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;

namespace KinonekoSoftware.UI.Controls.Panels
{
   
    public class IntentWrapPanel : Panel
    {
        private bool _skip;
        private bool _measure;
        private int  _count;

        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            _skip    = false;
            _measure = false;
            base.OnSizeChanged(e);
        }
        
        protected override void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_skip && _measure && _count == Children.Count)
            {
                return;
            }

            _count = Children.Count;
            base.ChildrenChanged(sender, e);
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
                spaceColumn = Math.Clamp(Children.Count - 1, 0, Children.Count);

                foreach (var element in Children)
                {
                    element.Measure(constraint);
                    h = Math.Max(h, element.DesiredSize.Height);
                    w = Math.Max(w, element.DesiredSize.Width);
                }

                return new Size(w * Children.Count + spaceColumn * space, h);
            }

            //
            // 第二次布局的时候，这个Panel大小再次调整
            // 会给一个固定尺寸的宽度和无限大的高度
            // 那就很好办了
            var intentWidth = IntentWidth;
            var column      = GetActualColumn(constraint.Width, space, intentWidth, Children.Count);
            spaceColumn = Math.Clamp(column - 1, 0, column);
            
            w = (constraint.Width - (spaceColumn * space)) / column;
            h = -1d;

            var size = new Size(w, constraint.Height);

            foreach (var element in Children)
            {
                element.Measure(size);
                h = Math.Max(h, element.DesiredSize.Height);
            }

            var row = Children.Count == 0 ? 0 : (Children.Count + spaceColumn) / column;
            var spaceRow = Math.Clamp(row - 1, 0, row);

            return new Size(
                constraint.Width,
                h * row + spaceRow * space);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var intentWidth = IntentWidth;
            var space       = Space;
            var column      = GetActualColumn(finalSize.Width, space, intentWidth, Children.Count);
            var spaceColumn = Math.Clamp(column - 1, 0, column);
            var w           = (finalSize.Width - (spaceColumn * space)) / column;
            var top         = 0d;
            var offset      = 0d;

            _measure = true;
            _skip    = true;

            foreach (var element in Children)
            {
                element.Measure(new Size(w, element.DesiredSize.Height));
            }

            for (var i = 0; i < Children.Count;)
            {
                var h = 0d;

                for (var n = 0; n < column && n + i < Children.Count; n++)
                {
                    h = Math.Max(h, Children[n + i].DesiredSize.Height);
                }

                for (var n = 0; n < column && i < Children.Count;)
                {
                    var element = Children[i];
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

        public static readonly StyledProperty<double> IntentWidthProperty = AvaloniaProperty.Register<IntentWrapPanel, double>(nameof(IntentWidth), 320d);
        public static readonly StyledProperty<double> SpaceProperty = AvaloniaProperty.Register<IntentWrapPanel, double>(nameof(Space), 16d);

        public double IntentWidth
        {
            get => GetValue(IntentWidthProperty);
            set => SetValue(IntentWidthProperty, value);
        }


        public double Space
        {
            get => GetValue(SpaceProperty);
            set => SetValue(SpaceProperty, value);
        }

    }
}