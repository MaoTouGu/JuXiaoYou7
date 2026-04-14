// ----------------------------------------------------------
//            文件：TextSourceVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月14日 13:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Blocks
{
    public class TextSourceVisualizer : VisualizerOptions<TextSettingView, TextSourcePresenter>, ITextTarget
    {
        private bool _isBold;
        private int  _fontSize;
        private int  _fontWeight;
        private int  _textAlignment;
        private int  _horizontalAlignment;
        private int  _verticalAlignment;

        private string _background;
        private string _foreground;
        private string _fontFamily;
        private string _metadataSource;
        private string _borderBrush;

        private Int32Thickness    _borderThickness;
        private Int32CornerRadius _cornerRadius;
        private Int32Thickness    _padding;

        public override IEnumerable<string> GetMetadataSources() => new[] { _metadataSource };

        protected override IVisualizerOptions Clone(string base64)
        {
            return new TextSourceVisualizer
            {
                Background          = "#00000000",
                BorderBrush         = "#00000000",
                FontFamily          = "Micorsoft Yahei",
                FontWeight          = 2,
                FontSize            = 14,
                VerticalAlignment   = 3,
                HorizontalAlignment = 3,
            };
        }

        public void RaisePropertyChanged(string name)
        {
            RaiseUpdated(name);
        }

        public Int32Thickness Padding
        {
            get => _padding;
            set => SetValue(ref _padding, value);
        }

        public Int32Thickness BorderThickness
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

        public string MetadataSource
        {
            get => _metadataSource;
            set => SetValue(ref _metadataSource, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>0，代表Top</para>
        /// <para>1，代表Center</para>
        /// <para>2，代表Bottom</para>
        /// <para>3，代表Stretch</para>
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
        /// <para>3，代表Stretch</para>
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

        public bool IsBold
        {
            get => _isBold;
            set => SetValue(ref _isBold, value);
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

        public override string Id   => "343A75655BE14136A6D9E1073FFC9078";
        public override string Name => "设定文本";

        public override int MinHeight => 40;
        public override int MinWidth  => 100;
    }
}