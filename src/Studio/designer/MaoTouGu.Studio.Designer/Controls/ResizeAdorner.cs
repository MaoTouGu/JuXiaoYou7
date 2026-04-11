// ----------------------------------------------------------
//            文件：ResizeAdorner.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 11:25
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using MaoTouGu.Studio.Database.Topology;

namespace MaoTouGu.JuXiaoYou.Controls
{
    public class ResizeAdorner : Adorner
    {
        private readonly Thumb _leftTopThumb;
        private readonly Thumb _leftBottomThumb;
        private readonly Thumb _rightTopThumb;
        private readonly Thumb _rightBottomThumb;
        private readonly Thumb _translateThumb;

        private readonly VisualCollection _visual;

        private static readonly ControlTemplate OutlineNWSETemplate;
        private static readonly ControlTemplate OutlineNESWTemplate;
        private static readonly ControlTemplate EmptyTemplate;
        private static readonly Thickness       One = new Thickness(2);


        static ResizeAdorner()
        {
            var factory = new FrameworkElementFactory(typeof(Border));

            //
            //
            factory.SetValue(Border.BackgroundProperty, Brushes.Transparent);


            var template = new ControlTemplate
            {
                TargetType = typeof(Thumb),
                VisualTree = factory,
            };

            template.Triggers.Add(new Trigger
            {
                Property = IsMouseOverProperty,
                Value    = true,
                Setters =
                {
                    new Setter { Value = Cursors.SizeAll, Property = CursorProperty },
                },
            });

            OutlineNESWTemplate = GetThumbControlTemplate(Cursors.SizeNESW);
            OutlineNWSETemplate = GetThumbControlTemplate(Cursors.SizeNWSE);
            EmptyTemplate       = template;
        }

        private readonly FrameworkElement _Parent;

        public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _leftTopThumb     = new Thumb { Template = OutlineNWSETemplate };
            _leftBottomThumb  = new Thumb { Template = OutlineNESWTemplate };
            _rightTopThumb    = new Thumb { Template = OutlineNESWTemplate };
            _rightBottomThumb = new Thumb { Template = OutlineNWSETemplate };
            _translateThumb   = new Thumb { Template = EmptyTemplate };
            _visual           = new VisualCollection(this);
            _Parent           = Xaml.FindVisualParent<Canvas>(adornedElement);

            _visual.Add(_translateThumb);
            _visual.Add(_leftTopThumb);
            _visual.Add(_leftBottomThumb);
            _visual.Add(_rightTopThumb);
            _visual.Add(_rightBottomThumb);

            AddLogicalChild(_translateThumb);
            AddLogicalChild(_leftTopThumb);
            AddLogicalChild(_leftBottomThumb);
            AddLogicalChild(_rightTopThumb);
            AddLogicalChild(_rightBottomThumb);

            _translateThumb.DragDelta     += DragDelta_Move;
            _translateThumb.DragCompleted += DragDelta_Completed_AdjustCoordinates;

            _leftTopThumb.DragDelta     += DragDelta_Move_LeftTopAdjust;
            _leftTopThumb.DragCompleted += DragDelta_Completed_AdjustSize;

            _leftBottomThumb.DragDelta     += DragDelta_Move_LeftBottomAdjust;
            _leftBottomThumb.DragCompleted += DragDelta_Completed_AdjustSize;


            _rightTopThumb.DragDelta     += DragDelta_Move_RightTopAdjust;
            _rightTopThumb.DragCompleted += DragDelta_Completed_AdjustSize;

            _rightBottomThumb.DragDelta     += DragDelta_Move_AdjustSize;
            _rightBottomThumb.DragCompleted += DragDelta_Completed_AdjustSize;
        }

        void AdjustX(TypographyBlockVPO block, double xAdjust)
        {

            if (block.X + xAdjust < _Parent.ActualWidth - block.Width &&
                block.X + xAdjust >= 0)
            {
                block.X += xAdjust;
            }
        }

        void AdjustY(TypographyBlockVPO block, double yAdjust)
        {
            if (block.Y + yAdjust < _Parent.ActualHeight - block.Height &&
                block.Y + yAdjust >= 0)
            {
                block.Y += yAdjust;
            }
        }

        private void DragDelta_Move(object sender, DragDeltaEventArgs e)
        {
            var yAdjust = e.VerticalChange;
            var xAdjust = e.HorizontalChange;

            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
                //
                // block.X += (int)(xAdjust / 20) * 20;
                // block.Y += (int)(yAdjust / 20) * 20;
            }
            // 更新元素位置（会同步到绑定的数据源，因为 X, Y 是 TwoWay）

            AdjustX(block, xAdjust);
            AdjustY(block, yAdjust);
        }

        private void DragDelta_Move_RightTopAdjust(object sender, DragDeltaEventArgs e)
        {

            var yAdjust = e.VerticalChange;
            var xAdjust = e.HorizontalChange;

            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
            }

            block.Width = Math.Clamp(block.Width + xAdjust, 60, _Parent.ActualWidth - block.X);
        }

        private void DragDelta_Move_AdjustSize(object sender, DragDeltaEventArgs e)
        {
            var yAdjust = e.VerticalChange;
            var xAdjust = e.HorizontalChange;

            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
            }



            // 更新元素位置（会同步到绑定的数据源，因为 X, Y 是 TwoWay）
            block.Width  = Math.Clamp(block.Width  + xAdjust, 60, _Parent.ActualWidth  - block.X);
            block.Height = Math.Clamp(block.Height + yAdjust, 60, _Parent.ActualHeight - block.Y);

        }

        private void DragDelta_Move_LeftTopAdjust(object sender, DragDeltaEventArgs e)
        {
            var yAdjust = e.VerticalChange;
            var xAdjust = e.HorizontalChange;

            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
            }

            block.Width  = Math.Clamp(block.Width  - xAdjust, 60, _Parent.ActualWidth  - block.X);
            block.Height = Math.Clamp(block.Height - yAdjust, 60, _Parent.ActualHeight - block.Y);

            DragDelta_Move(sender, e);
        }

        private void DragDelta_Move_LeftBottomAdjust(object sender, DragDeltaEventArgs e)
        {
            var yAdjust = e.VerticalChange;
            var xAdjust = e.HorizontalChange;

            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
            }

            block.Width  = Math.Clamp(block.Width  + xAdjust, 60, _Parent.ActualWidth  - block.X);
            block.Height = Math.Clamp(block.Height + yAdjust, 60, _Parent.ActualHeight - block.Y);

            DragDelta_Move(sender, e);
        }

        private void DragDelta_Completed_AdjustCoordinates(object sender, DragCompletedEventArgs e)
        {
            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
                //
                // block.X += (int)(xAdjust / 20) * 20;
                // block.Y += (int)(yAdjust / 20) * 20;
            }

            // 更新元素位置（会同步到绑定的数据源，因为 X, Y 是 TwoWay）
            block.X = Math.Round(block.X / 20d) * 20d;
            block.Y = Math.Round(block.Y / 20d) * 20d;

        }

        private void DragDelta_Completed_AdjustSize(object sender, DragCompletedEventArgs e)
        {
            if (AdornedElement is not FrameworkElement { DataContext: TypographyBlockVPO block })
            {
                return;
                //
                // block.X += (int)(xAdjust / 20) * 20;
                // block.Y += (int)(yAdjust / 20) * 20;
            }

            // 更新元素位置（会同步到绑定的数据源，因为 X, Y 是 TwoWay）
            block.Width  = Math.Round(block.Width  / 20d) * 20d;
            block.Height = Math.Round(block.Height / 20d) * 20d;

        }

        private static bool IsCorrect(double v) => !double.IsInfinity(v) && !double.IsNaN(v);

        static ControlTemplate GetThumbControlTemplate(Cursor cursor)
        {
            var factory = new FrameworkElementFactory(typeof(Border));


            factory.SetValue(Border.BackgroundProperty, Brushes.Black);
            factory.SetValue(Border.BorderBrushProperty, Brushes.White);
            factory.SetValue(Border.BorderThicknessProperty, One);
            factory.SetValue(Border.HeightProperty, 8d);
            factory.SetValue(Border.WidthProperty, 8d);

            var template = new ControlTemplate
            {
                TargetType = typeof(Thumb),
                VisualTree = factory,
            };

            template.Triggers.Add(new Trigger
            {
                Property = IsMouseOverProperty,
                Value    = true,
                Setters =
                {
                    new Setter { Value = cursor, Property = CursorProperty },
                },
            });

            return template;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var w     = IsCorrect(constraint.Width) ? constraint.Width : 1000;
            var h     = IsCorrect(constraint.Height) ? constraint.Height : 1000;
            var size2 = new Size(w + 8, h + 8);

            _leftTopThumb.Measure(size2);
            _leftBottomThumb.Measure(size2);
            _rightTopThumb.Measure(size2);
            _rightBottomThumb.Measure(size2);

            _translateThumb.Height = AdornedElement.RenderSize.Height;
            _translateThumb.Width  = AdornedElement.RenderSize.Width;
            _translateThumb.Measure(size2);


            return base.MeasureOverride(size2);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _leftTopThumb.Arrange(new Rect(new Point(-4, -4), _leftTopThumb.RenderSize));
            _leftBottomThumb.Arrange(new Rect(new Point(-4, finalSize.Height - 4), _leftBottomThumb.RenderSize));

            _rightTopThumb.Arrange(new Rect(new Point(finalSize.Width    - 4, -4), _rightBottomThumb.RenderSize));
            _rightBottomThumb.Arrange(new Rect(new Point(finalSize.Width - 4, finalSize.Height - 4), _rightBottomThumb.RenderSize));


            _translateThumb.Arrange(new Rect(new Point(4, 4), AdornedElement.RenderSize));

            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var rect = new Rect(0, 0, AdornedElement.RenderSize.Width, AdornedElement.RenderSize.Height);
            drawingContext.DrawRectangle(Brushes.Transparent, new Pen(Brushes.White, 2), rect);
        }

        protected override Visual GetVisualChild(int index) => _visual[index];

        protected override int VisualChildrenCount => _visual.Count;
    }
}