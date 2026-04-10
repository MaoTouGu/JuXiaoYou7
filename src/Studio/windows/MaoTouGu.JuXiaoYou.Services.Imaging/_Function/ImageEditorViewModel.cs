using System.Diagnostics;
using MaoTouGu.Shells.Core;

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public sealed partial class ImageEditorViewModel : ObjectRoot<Tuple<int, int, int, int>>
    {
        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private double _originWidth;
        private double _originHeight;

        [DebuggerHidden]
        public ImageEditorViewModel() {}

        public ImageEditorViewModel(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Buffer  { get; }
        public byte[] Buffer2 { get; set; }

        protected override Tuple<int, int, int, int> OnFinish(bool edit)
        {

            Debug.WriteLine($"vm:{_x},{_y},{_width},{_height}");
            return new Tuple<int, int, int, int>((int)_x, (int)_y, (int)_width, (int)_height);
        }

        public double OriginHeight
        {
            get => _originHeight;
            set => SetValue(ref _originHeight, value);
        }

        public double OriginWidth
        {
            get => _originWidth;
            set => SetValue(ref _originWidth, value);
        }

        public double Height
        {
            get => _height;
            set => SetValue(ref _height, value);
        }

        public double Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }

        public double Y
        {
            get => _y;
            set => SetValue(ref _y, value);
        }

        public double X
        {
            get => _x;
            set => SetValue(ref _x, value);
        }
    }

}