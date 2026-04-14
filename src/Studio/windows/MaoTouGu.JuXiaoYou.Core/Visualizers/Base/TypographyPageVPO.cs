// ----------------------------------------------------------
//            文件：TypographyPageVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月13日 16:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class TypographyPageVPO : ObservableObject
    {
        private int    _width;
        private int    _height;
        private string _name;

        public ViewList<TypographyBlockVPO> Blocks { get; init; }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
        
        public int Height
        {
            get => _height;
            set => SetValue(ref _height, value);
        }
        public int Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }
    }
}