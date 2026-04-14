// ----------------------------------------------------------
//            文件：DesignView.Scale.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 20:13
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    partial class DesignViewModel
    {
        private int _intScale = 100;

        public int IntScale
        {
            get => _intScale;
            set
            {
                SetValue(ref _intScale, value);
                RaiseUpdated(nameof(Scale));
                RaiseUpdated(nameof(ScaleText)); 
            }
        }


        public string ScaleText => $"{_intScale}%";
        public double Scale     => _intScale / 100d;
    }
}