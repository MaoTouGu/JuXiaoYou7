using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public sealed class MiniMap : ContentControl
    {

        public static readonly DependencyProperty OriginSizeProperty;
        public static readonly DependencyProperty MaxEdgeSizeProperty;
        public static readonly DependencyProperty PositionProperty;
        public static readonly DependencyProperty ImageSourceProperty;

        static MiniMap()
        {
            OriginSizeProperty = DependencyProperty.Register(
                                                             nameof(OriginSize),
                                                             typeof(Size),
                                                             typeof(MiniMap),
                                                             new PropertyMetadata(default(Size), OnVisualChanged));
            MaxEdgeSizeProperty = DependencyProperty.Register(
                                                              nameof(MaxEdgeSize),
                                                              typeof(double),
                                                              typeof(MiniMap),
                                                              new PropertyMetadata(default(double), OnVisualChanged));

            ImageSourceProperty = DependencyProperty.Register(
                                                              nameof(ImageSource),
                                                              typeof(ImageSource),
                                                              typeof(MiniMap),
                                                              new PropertyMetadata(default(ImageSource), OnVisualChanged));
            PositionProperty = DependencyProperty.Register(
                                                           nameof(Position),
                                                           typeof(Point),
                                                           typeof(MiniMap),
                                                           new PropertyMetadata(default(Point), OnVisualChanged));
        }

        private static void OnVisualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MiniMap)d).DrawVisual();
        }

        private readonly Border     _Image;
        private readonly Border     _Thumb;
        private readonly ImageBrush _Brush;

        private Thickness _thickness;
        
        public MiniMap()
        {
            _Brush = new ImageBrush();
            _Image = new Border
            {
                Background = _Brush,
            };
            _Thumb = new Border
            {
                Child = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = new Thickness(1),
                },
                BorderBrush         = new SolidColorBrush(Colors.White),
                BorderThickness     = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
            };
            _thickness = new Thickness();

            _Image.Child = _Thumb;
            Content      = _Image;
            MinWidth     = 160;
            MinHeight    = 160;
        }

        private void DrawVisual()
        {
            //
            // 初始化图片源。
            var size   = OriginSize;
            var radius = Math.Min(ActualWidth, ActualHeight);

            if (ActualHeight == 0d || ActualWidth == 0)
            {
                return;
            }

            //
            //
            double actualHeight;
            double actualWidth;
            double scale;
            var    scale2 = MaxEdgeSize;

            //
            //
            _Brush.ImageSource = ImageSource;

            //
            // 计算_Image
            if (size.Width > size.Height)
            {
                actualHeight = radius / size.Width * size.Height;
                actualWidth  = radius;
                scale        = radius / size.Width;
            }
            else
            {
                actualHeight = radius;
                actualWidth  = radius / size.Height * size.Width;
                scale        = radius               / size.Height;
            }

            if(size.Width == 0 || size.Height == 0)
            {
                return;
            }

            //
            //
            _Image.Width  = actualWidth;
            _Image.Height = actualHeight;
            _Thumb.Height = scale2 * scale;
            _Thumb.Width  = scale2 * scale;

            //
            //
            _thickness.Left = Position.X * scale;
            _thickness.Top  = Position.Y * scale;
            _Thumb.Margin   = _thickness;
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        public double MaxEdgeSize
        {
            get => (double)GetValue(MaxEdgeSizeProperty);
            set => SetValue(MaxEdgeSizeProperty, value);
        }

        public Size OriginSize
        {
            get => (Size)GetValue(OriginSizeProperty);
            set => SetValue(OriginSizeProperty, value);
        }
    }
}