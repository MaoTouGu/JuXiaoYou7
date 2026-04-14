// ----------------------------------------------------------
//            文件：TypographyRectangleVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 21:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public class TypographyRectangleVPO : TypographyBlockVPO<TypographyRectangle>, ICornerRadiusTarget, IBorderThicknessTarget
    {

        public Int32CornerRadius CornerRadius
        {
            get => Instance.CornerRadius;
            set
            {
                Instance.CornerRadius = value;
                RaiseUpdated();
            }
        }

        public string BorderBrush
        {
            get => Instance.BorderBrush;
            set
            {
                Instance.BorderBrush = value;
                RaiseUpdated();
            }
        }
        
        public string Background
        {
            get => Instance.Background;
            set
            {
                Instance.Background = value;
                RaiseUpdated();
            }
        }
        public Int32Thickness BorderThickness
        {
            get => Instance.BorderThickness;
            set
            {
                Instance.BorderThickness = value;
                RaiseUpdated();
            }
        }
        
        protected override TypographyBlockVPO OnCreate(TypographyRectangle block, Moniker moniker) => new TypographyRectangleVPO
        {
            Moniker  = moniker,
            Instance = block,
        };
    }
}