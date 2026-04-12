// ----------------------------------------------------------
//            文件：NamedBitmap.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 12:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{


    public sealed class NamedBitmap : ObservableObject
    {
        private BitmapImage _image;
        private string      _name;

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public BitmapImage Image
        {
            get => _image;
            set => SetValue(ref _image, value);
        }
    }
}