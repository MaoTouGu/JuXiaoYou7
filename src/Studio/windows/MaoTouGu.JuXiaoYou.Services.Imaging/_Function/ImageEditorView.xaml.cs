
using System.Diagnostics;
using System.Windows.Input;
using MaoTouGu.Shells.Attributes;
using MaoTouGu.Shells.Controls;
using ImageInfo = MaoTouGu.JuXiaoYou.Services.Imaging.ImageInfo;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{

    [Associate(View = typeof(ImageEditorView), ViewModel = typeof(ImageEditorViewModel))]
    public partial class ImageEditorView : ForestDialog
    {
        private BitmapImage _OriginImage;
        private BitmapImage _ThumbImage;
        
        private int  _X;
        private int  _Y;
        private int  _W;
        private int  _H;
        private int  _OW;
        private int  _OH;
        private bool _pressed;

        private CroppedBitmap _Cropped;
        private Int32Rect     _Rect;
        private Point         _last;
        private Point         _temp;

        private double _originScale;
        private double _scale;
        
        public ImageEditorView()
        {
            InitializeComponent();
        }

        private void DoMove(ref Point pos)
        {
            var vm = ViewModel<ImageEditorViewModel>();
            var x  = (_last.X - pos.X);
            var y  = (_last.Y - pos.Y);

            //
            //
            x = Math.Clamp(_X + x, 0, _OW - _W);
            y = Math.Clamp(_Y + y, 0, _OH - _H);

            //
            //

            _Rect.X      = (int)x;
            _Rect.Y      = (int)y;
            _Rect.Width  = _W;
            _Rect.Height = _H;
            _temp.X      = _Rect.X;
            _temp.Y      = _Rect.Y;
            vm.X         = x;
            vm.Y         = y;
            
            //
            //
            DoCropped();
            
            //
            //
            MiniMap.Position = _temp;
        }

        private void SetThumbSize(double size, ImageEditorViewModel vm = null)
        {
            vm        ??= ViewModel<ImageEditorViewModel>();
            _W        =   (int)size;
            _H        =   (int)size;
            vm.Width  =   size;
            vm.Height =   size;
            _scale    =   size;
            
            //
            // 将对应的MaxEdgeSize推送给MiniMap
            MiniMap.MaxEdgeSize = size;
            Scale.Text          = $"{(_scale / _originScale):f2}×";
        }
        
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _pressed         = true;
            _last            = e.GetPosition(this);
            MiniMap.Position = _temp;
        }

        private void Button_Plus(object sender, RoutedEventArgs e)
        {
            var vm      = ViewModel<ImageEditorViewModel>();
            var minEdge = Math.Min(_OW, _OH);
            var maxEdge = Math.Max(_W, _H);
            var gap     = (0.05d * minEdge);
            
            if (maxEdge + gap < minEdge)
            {
                _W = (int)Math.Clamp(_W     + gap, 32, _OW);
                _H = (int)Math.Clamp(_H     + gap, 32, _OH);
                _X = (Math.Clamp(_X, 0, _OW - _W));
                _Y = (Math.Clamp(_Y, 0, _OH - _H));

                _Rect.X             = _X;
                _Rect.Y             = _Y;
                _Rect.Width         = _W;
                _Rect.Height        = _H;
                _Cropped            = new CroppedBitmap(_OriginImage, _Rect);
                Image.Source        = _Cropped;

                //
                //
                SetThumbSize(Math.Max(_W, _H), vm);
            }
        }
        
        private void Button_Minus(object sender, RoutedEventArgs e)
        {
            var vm      = ViewModel<ImageEditorViewModel>();
            var minEdge = Math.Min(_OW, _OH);
            var maxEdge = Math.Max(_W, _H);
            var gap     = (0.05d * minEdge);
            
            if (maxEdge - gap < minEdge && maxEdge > 64)
            {
                _W = (int)Math.Clamp(_W     - gap, 32, _OW);
                _H = (int)Math.Clamp(_H     - gap, 32, _OH);
                _X = (Math.Clamp(_X, 0, _OW - _W));
                _Y = (Math.Clamp(_Y, 0, _OH - _H));

                _Rect.X      = _X;
                _Rect.Y      = _Y;
                _Rect.Width  = _W;
                _Rect.Height = _H;
                _Cropped     = new CroppedBitmap(_OriginImage, _Rect);
                Image.Source = _Cropped;

                //
                //
                SetThumbSize(Math.Max(_W, _H), vm);
            }
        }
        
        private void DoCropped()
        {
            //
            //
            _Cropped     = new CroppedBitmap(_OriginImage, _Rect);
            Image.Source = _Cropped;

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(this);

                DoMove(ref pos);
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var vm  = ViewModel<ImageEditorViewModel>();
            var pos = e.GetPosition(this);
            var x   = (_last.X - pos.X);
            var y   = (_last.Y - pos.Y);

            _X = (int)(Math.Clamp(_X + x, 0, _OW - _W));
            _Y = (int)(Math.Clamp(_Y + y, 0, _OH - _H));

            _temp.X          = _X;
            _temp.Y          = _Y;
            vm.X             = _X;
            vm.Y             = _Y;
            MiniMap.Position = _temp;
            _pressed = false;
            Debug.WriteLine($"v up:{_X},{_Y},{_W},{_H}");
        }

        private void Button_LostMouseCapture(object sender, MouseEventArgs e)
        {
            _X = (int)_temp.X;
            _Y = (int)_temp.Y;
            Debug.WriteLine($"v lost:{_X},{_Y},{_W},{_H}");
        }
        
        private void OnMouseRelease(object sender, MouseEventArgs e)
        {
            if (!_pressed)
            {
                return;
            }

            var vm  = ViewModel<ImageEditorViewModel>();
            var pos = e.GetPosition(this);
            var x   = (_last.X - pos.X);
            var y   = (_last.Y - pos.Y);

            _X = (int)(Math.Clamp(_X + x, 0, _OW - _W));
            _Y = (int)(Math.Clamp(_Y + y, 0, _OH - _H));

            vm.X = _X;
            vm.Y = _Y;
            
            _temp.X          = _X;
            _temp.Y          = _Y;
            _pressed         = false;
            MiniMap.Position = _temp;
            Debug.WriteLine($"v release:{_X},{_Y},{_W},{_H}");
        }

        protected override void OnLoaded()
        {
            var vm = ViewModel<ImageEditorViewModel>();
            var ms = new MemoryStream(vm.Buffer);
            var ms2 = new MemoryStream(vm.Buffer);

            if (!ImageInfo.GetMetadata(vm.Buffer, out var width, out var height))
            {
                return;
            }

            _OW             = width;
            _OH             = height;
            vm.OriginHeight = height;
            vm.OriginWidth  = width;
            
            var v = Math.Min(width, height);

            if (width < 256 || height < 256)
            {
                _Rect  = new Int32Rect(0, 0, v, v);
                _scale = _originScale = v;
                SetThumbSize(v, vm);
            }
            else
            {
                SetThumbSize(256, vm);
                _scale = _originScale = 256d;
                _Rect  = new Int32Rect(0, 0, 256, 256);
            }
            
            //
            //
            _ThumbImage  = new BitmapImage();
            _OriginImage = new BitmapImage();
            _ThumbImage.BeginInit();
            _OriginImage.BeginInit();

            _ThumbImage.StreamSource = ms;
            _OriginImage.StreamSource = ms2;

            //
            //
            Scale.Text = "1.0×";

            //
            // 将打开的图片缩放到280px
            if (width > height)
            {
                _ThumbImage.DecodePixelWidth  = 280;
                _ThumbImage.DecodePixelHeight = 280 / height * width;
            }
            else
            {
                _ThumbImage.DecodePixelHeight = 280;
                _ThumbImage.DecodePixelWidth  = 280 / height * width;
            }

            _ThumbImage.EndInit();
            _OriginImage.EndInit();

            //
            //
            MiniMap.ImageSource = _ThumbImage;
            MiniMap.OriginSize  = new Size(width, height);

            DoCropped();
        }

        private void Button_ClearFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}