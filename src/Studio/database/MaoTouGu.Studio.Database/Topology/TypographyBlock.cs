// ----------------------------------------------------------
//            文件：TypographyBlock.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 23:55
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Topology
{
    public abstract class TypographyBlock : Nameable
    {
        private double _width;
        private double _height;
        private double _y;
        private double _x;
        private bool   _isLock;
        private double _opacity;

        public double Opacity
        {
            get => _opacity;
            set => SetValue(ref _opacity, value);
        }

        public bool IsLock
        {
            get => _isLock;
            set => SetValue(ref _isLock, value);
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
            set
            {
                SetValue(ref _y, value);
            }
        }

        public double X
        {
            get => _x;
            set
            {
                SetValue(ref _x, value);
            }
        }

    }
}