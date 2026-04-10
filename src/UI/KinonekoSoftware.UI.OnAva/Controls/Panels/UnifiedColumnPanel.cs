using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;

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
        public static readonly StyledProperty<int> ColumnProperty = AvaloniaProperty.Register<UnifiedColumnPanel, int>(nameof(Column));

        private bool _skip;
        private bool _measure;


        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            _skip    = false;
            _measure = false;
            base.OnSizeChanged(e);
        }
        
        protected override void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_skip && _measure)
            {
                return;
            }

            base.ChildrenChanged(sender, e);
        }

       protected override Size MeasureOverride(Size constraint)
        {
            var column = Column;
            var row    = (Children.Count + column - 1) / column;
            var w      = 0d;
            var h      = 0d;

            //
            // 第一次测量的时候，也就是这个Panel第一次初始化的时候
            // 会给一个长和宽都是无限大的Size
            // 尽量将所有的控件都布局在第一行
            if (double.IsInfinity(constraint.Width) && double.IsInfinity(constraint.Height))
            {
                foreach (var element in Children)
                {
                    //
                    //
                    element.Measure(constraint);
                    element.Measure(new Size(element.DesiredSize.Width / column, constraint.Height));

                    w = Math.Max(w, element.DesiredSize.Width);
                    h = Math.Max(h, element.DesiredSize.Height);
                }

                return new Size(w * column, h * row);
            }

            w = constraint.Width / column;
            var size = new Size(w, constraint.Height);
            var h1   = 0d;
            
            for (var i = 0; i < Children.Count;)
            {
                h = 0;
                w = 0;
                
                for (var c = 0; c < column && i < Children.Count;)
                {
                    var item = Children[i];
                    item.Measure(size);
                    
                    //
                    //
                    h = Math.Max(h, item.DesiredSize.Height);
                    
                    c++;
                    i++;
                }
                
                //
                //
                h1 += h;
            }

            return new Size(w * column, h1);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var column = Column;    
            var w      = finalSize.Width / column;
            var top    = 0d;
            
            
            _measure = true;
            _skip    = true;

            for (var i = 0; i < Children.Count;)
            {
                var h = 0d;

                for (var n = 0; n < column && n + i < Children.Count; n++)
                {
                    h = Math.Max(h, Children[n + i].DesiredSize.Height);
                }

                for (var n = 0; n < column && i < Children.Count; )
                {
                    var x       = i % column;
                    var element = Children[i];
                    var rect    = new Rect(x * w, top, w, h);
                    element.Arrange(rect);
                    n++;
                    i++;
                }

                top += h;
            }
            return finalSize;
        }
        
        public int Column
        {
            get => GetValue(ColumnProperty);
            set => SetValue(ColumnProperty, value);
        }
    }
}