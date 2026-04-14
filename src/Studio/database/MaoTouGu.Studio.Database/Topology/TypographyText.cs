// ----------------------------------------------------------
//            文件：TypographyText.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 15:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Topology
{
    public sealed class TypographyText : TypographyBlock
    {
        private int            _fontStyle;
        private int            _fontSize;
        private int            _fontWeight;
        private int            _textAlignment;
        private int            _horizontalAlignment;
        private int            _verticalAlignment;
        private int            _borderThickness;

        private string _background;
        private string _foreground;
        private string _fontFamily;
        private string _text;
        private string _borderBrush;
        
        private Int32CornerRadius _cornerRadius;
        private Int32Thickness    _padding;

        public Int32Thickness Padding
        {
            get => _padding;
            set => SetValue(ref _padding, value);
        }        

        public int BorderThickness
        {
            get => _borderThickness;
            set => SetValue(ref _borderThickness, value);
        }
        
        public string BorderBrush
        {
            get => _borderBrush;
            set => SetValue(ref _borderBrush, value);
        }

        public Int32CornerRadius CornerRadius
        {
            get => _cornerRadius;
            set => SetValue(ref _cornerRadius, value);
        }

        public string Text
        {
            get => _text;
            set => SetValue(ref _text, value);
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
            get => _verticalAlignment;
            set => SetValue(ref _verticalAlignment, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Left</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Right</para>
        /// </remarks>
        public int HorizontalAlignment
        {
            get => _horizontalAlignment;
            set => SetValue(ref _horizontalAlignment, value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Left</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Right</para>
        /// <para>3，代表Justify</para>
        /// </remarks>
        public int TextAlignment
        {
            get => _textAlignment;
            set => SetValue(ref _textAlignment, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///
        /// <para>0 = Thin </para>
        /// <para>1 = Light </para>
        /// <para>2 = Normal </para>
        /// <para>3 = Bold </para>
        /// <para>4 = Black </para>
        /// <para>5 = UltraBlack </para>
        /// </remarks>
        public int FontWeight
        {
            get => _fontWeight;
            set => SetValue(ref _fontWeight, value);
        }

        public int FontSize
        {
            get => _fontSize;
            set => SetValue(ref _fontSize, value);
        }
        
        public int FontStyle
        {
            get => _fontStyle;
            set => SetValue(ref _fontStyle, value);
        }

        public string FontFamily
        {
            get => _fontFamily;
            set => SetValue(ref _fontFamily, value);
        }

        public string Foreground
        {
            get => _foreground;
            set => SetValue(ref _foreground, value);
        }

        public string Background
        {
            get => _background;
            set => SetValue(ref _background, value);
        }
    }
}