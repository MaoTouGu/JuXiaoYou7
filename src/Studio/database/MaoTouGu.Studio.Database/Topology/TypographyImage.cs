// ----------------------------------------------------------
//            文件：TypographyImage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 15:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Topology
{
    public class TypographyImage : TypographyBlock
    {
        private string _source;
        private int    _imageWidth;
        private int    _imageHeight;

        public int ImageHeight
        {
            get => _imageHeight;
            set => SetValue(ref _imageHeight, value);
        }

        public int ImageWidth
        {
            get => _imageWidth;
            set => SetValue(ref _imageWidth, value);
        }

        public string Source
        {
            get => _source;
            set => SetValue(ref _source, value);
        }
    }
}