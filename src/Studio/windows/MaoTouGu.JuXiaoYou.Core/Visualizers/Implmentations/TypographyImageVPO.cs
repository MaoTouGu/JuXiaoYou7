// ----------------------------------------------------------
//            文件：TypographyImageVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 14:43
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public sealed class TypographyImageVPO : TypographyBlockVPO<TypographyImage>
    {
        protected override TypographyBlockVPO OnCreate(TypographyImage block, Moniker moniker)
        {
            return new TypographyImageVPO
            {
                Moniker  = moniker,
                Instance = block,
            };
        }
        
        
        public int ImageHeight
        {
            get => Instance.ImageHeight;
            set
            {
                Instance.ImageHeight = value;
                RaiseUpdated();
            }
        }

        public int ImageWidth
        {
            get => Instance.ImageWidth;
            set
            {
                Instance.ImageWidth = value;
                RaiseUpdated();
            }
        }

        public string Source
        {
            get => Instance.Source;
            set
            {
                Instance.Source = value;
                RaiseUpdated();
            }
        }
    }
}