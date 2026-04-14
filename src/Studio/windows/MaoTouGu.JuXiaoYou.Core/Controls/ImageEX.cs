// ----------------------------------------------------------
//            文件：ImageEX.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月17日 21:43
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaoTouGu.Foundation.Mathematics;
using MaoTouGu.JuXiaoYou.Services.Imaging;

namespace MaoTouGu.JuXiaoYou.Controls
{
    public sealed class ImageEX : Control, IImageWorker
    {

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                                        nameof(Source),
                                        typeof(BitmapSource),
                                        typeof(ImageEX),
                                        new FrameworkPropertyMetadata(default(BitmapSource), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(
                                        nameof(X),
                                        typeof(int),
                                        typeof(ImageEX),
                                        new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(
                                        nameof(Y),
                                        typeof(int),
                                        typeof(ImageEX),
                                        new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender));


        public static readonly DependencyProperty EnableCroppedProperty =
            DependencyProperty.Register(
                                        nameof(EnableCropped),
                                        typeof(bool),
                                        typeof(ImageEX),
                                        new PropertyMetadata(Boxing.False, OnEnableCroppedChanged));

        private static void OnEnableCroppedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool b && d is ImageEX o)
            {
                if (b)
                {
                    o.MouseLeftButtonDown += o.OnCropStarted;
                    o.MouseLeftButtonUp   += o.OnCropStopped;
                    o.MouseLeave          += o.OnLostMouseCapture;
                }
                else
                {
                    o.MouseLeftButtonDown -= o.OnCropStarted;
                    o.MouseLeftButtonUp   -= o.OnCropStopped;
                    o.MouseMove           -= o.OnCropping;
                    o.MouseLeave          -= o.OnLostMouseCapture;
                }
            }
        }

        private Point _LastPosition;
        private Point _CurrentPosition;
        private int   _X;
        private int   _Y;

        private void OnCropStarted(object sender, MouseButtonEventArgs e)
        {
            MouseMove     += OnCropping;
            _X            =  X;
            _Y            =  Y;
            _LastPosition =  e.GetPosition(this);
        }

        private void OnCropping(object sender, MouseEventArgs e)
        {
            _CurrentPosition = e.GetPosition(this);

            var v  = _LastPosition - _CurrentPosition;
            var x  = (int)(_X + v.X);
            var y  = (int)(_Y + v.Y);
            var s  = Source;
            var iW = (short)Math.Min(s.PixelWidth, ActualWidth);
            var iH = (short)Math.Min(s.PixelHeight, ActualHeight);
            var aW = (short)Math.Max(s.PixelWidth, ActualWidth);
            var aH = (short)Math.Max(s.PixelHeight, ActualHeight);

            x = Math.Clamp(x, iW - aW, aW - iW);
            y = Math.Clamp(y, iH - aH, aH - iH);

            if (x + iW < s.PixelWidth)
            {
                //
                // 锁X轴
                SetValue(XProperty, x);
            }

            if (y + iH < s.PixelHeight)
            {
                SetValue(YProperty, y);
            }
        }

        private void OnCropStopped(object sender, MouseButtonEventArgs e)
        {
            MouseMove -= OnCropping;
            _X        =  0;
            _Y        =  0;
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            MouseMove -= OnCropping;
            _X        =  0;
            _Y        =  0;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Source is null)
            {
                return;
            }

            var s  = Source;
            var x  = (short)Math.Clamp(X, 0, short.MaxValue);
            var y  = (short)Math.Clamp(Y, 0, short.MaxValue);
            var iW = (short)Math.Min(s.PixelWidth, ActualWidth);
            var iH = (short)Math.Min(s.PixelHeight, ActualHeight);
            var aW = (short)Math.Max(s.PixelWidth, ActualWidth);
            var aH = (short)Math.Max(s.PixelHeight, ActualHeight);
            var vH = (short)Math.Clamp(ActualHeight, 0, short.MaxValue);
            var vW = (short)Math.Clamp(ActualWidth, 0, short.MaxValue);

            if (DoubleStatic.IsZero(vW) || DoubleStatic.IsZero(vH))
            {
                return;
            }

            if (DoubleStatic.IsZero(iW) || DoubleStatic.IsZero(iH))
            {
                return;
            }

            if (s.PixelWidth < vW && s.PixelHeight < vH)
            {
                x = (short)((vW - s.PixelWidth)  / 2f);
                y = (short)((vH - s.PixelHeight) / 2f);
                drawingContext.DrawImage(s, new Rect(x, y, s.PixelWidth, s.PixelHeight));
                return;
            }

            x = (short)Math.Clamp(x, iW - aW, aW - iW);
            y = (short)Math.Clamp(y, iH - aH, aH - iH);

            if (x + iW > s.PixelWidth)
            {
                //
                // 锁X轴
                x = 0;
            }

            if (y + iH > s.PixelHeight)
            {
                y = 0;
            }

            var croppedImage = new CroppedBitmap(Source, new Int32Rect(x, y, iW, iH));

            drawingContext.DrawImage(croppedImage, new Rect(RenderSize));
        }

        public void SetImage(BitmapImage bi)
        {
            Source = bi;
        }

        public bool EnableCropped
        {
            get => (bool)GetValue(EnableCroppedProperty);
            set => SetValue(EnableCroppedProperty, Boxing.Box(value));
        }

        public int Y
        {
            get => (int)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }

        public int X
        {
            get => (int)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public BitmapSource Source
        {
            get => (BitmapSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

    }
}