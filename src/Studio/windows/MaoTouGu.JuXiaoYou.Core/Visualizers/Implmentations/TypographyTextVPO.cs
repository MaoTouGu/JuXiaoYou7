// ----------------------------------------------------------
//            文件：TypographyTextVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 14:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public class TypographyTextVPO : TypographyBlockVPO<TypographyText>, ITextTarget
    {
        protected override TypographyBlockVPO OnCreate(TypographyText block, Moniker moniker)
        {
            return new TypographyTextVPO
            {
                Moniker  = moniker,
                Instance = block,
            };
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

        public string BorderBrush
        {
            get => Instance.BorderBrush;
            set
            {
                Instance.BorderBrush = value;
                RaiseUpdated();
            }
        }

        public Int32Thickness Padding
        {
            get => Instance.Padding;
            set
            {
                Instance.Padding = value;
                RaiseUpdated();
            }
        }

        public Int32CornerRadius CornerRadius
        {
            get => Instance.CornerRadius;
            set
            {
                Instance.CornerRadius = value;
                RaiseUpdated();
            }
        }

        public string Text
        {
            get => Instance.Text;
            set
            {
                Instance.Text = value;
                RaiseUpdated();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Top</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Bottom</para>
        /// </remarks>
        public int VerticalAlignment
        {
            get => Instance.VerticalAlignment;
            set
            {
                Instance.VerticalAlignment = value;
                RaiseUpdated();
            }
        }

        public int HorizontalAlignment
        {
            get => Instance.HorizontalAlignment;
            set
            {
                Instance.HorizontalAlignment = value;
                RaiseUpdated();
            }
        }

        public int TextAlignment
        {
            get => Instance.TextAlignment;
            set
            {
                Instance.TextAlignment = value;
                RaiseUpdated();
            }
        }

        public int FontWeight
        {
            get => Instance.FontWeight;
            set
            {
                Instance.FontWeight = value;
                RaiseUpdated();
            }
        }

        public int FontSize
        {
            get => Instance.FontSize;
            set
            {
                Instance.FontSize = value;
                RaiseUpdated();
            }
        }

        public bool IsBold
        {
            get => Instance.IsBold;
            set
            {
                Instance.IsBold = value;
                RaiseUpdated();
            }
        }

        public string FontFamily
        {
            get => Instance.FontFamily;
            set
            {
                Instance.FontFamily = value;
                RaiseUpdated();
            }
        }

        public string Foreground
        {
            get => Instance.Foreground;
            set
            {
                Instance.Foreground = value;
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
    }
}