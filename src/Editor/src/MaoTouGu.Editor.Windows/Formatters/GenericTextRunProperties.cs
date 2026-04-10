

namespace MaoTouGu.Editor.Formatters
{
    public class GenericTextRunProperties : TextRunProperties, IImmutable<GenericTextRunProperties>
    {
        private readonly CultureInfo _cultureInfo;

        private double   _fontSize;
        private Brush    _background;
        private Brush    _foreground;
        private Typeface _typeface;

        private TextDecorationCollection _textDecorationCollection;
        private TextEffectCollection     _textEffectCollection;
        private BaselineAlignment        _baselineAlignment;

        public GenericTextRunProperties(double pixelsPerDip)
        {
            _cultureInfo = CultureInfo.CurrentUICulture;
            PixelsPerDip = pixelsPerDip;
        }

        public GenericTextRunProperties(double pixelsPerDip, CultureInfo cultureInfo)
        {
            ValidateCulture(cultureInfo);
            _cultureInfo = cultureInfo;
            PixelsPerDip = pixelsPerDip;
        }

        public GenericTextRunProperties(
            Typeface    typeface,
            double      pixelsPerDip,
            double      fontSize,
            Brush       forground,
            Brush       background,
            CultureInfo culture)
        {
            if (typeface == null)
            {
                throw new ArgumentNullException(nameof(typeface));
            }

            ValidateCulture(culture);

            PixelsPerDip = pixelsPerDip;
            _typeface    = typeface;
            _fontSize    = fontSize;
            _foreground  = forground;
            _background  = background;
            _cultureInfo = culture;
        }


        private static void ValidateCulture(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException(nameof(culture));
            if (culture.IsNeutralCulture || culture.Equals(CultureInfo.InvariantCulture))
                throw new ArgumentException("Specific Culture Required", nameof(culture));
        }

        private void Verify()
        {
            if (IsImmutable)
            {
                throw new InvalidOperationException("无法修改一个已经变成不可变类型的对象实例。");
            }
        }

        /// <summary>
        /// 设置文本字号。
        /// </summary>
        /// <param name="fontSize">文本字号</param>
        public void SetFontSize(double fontSize)
        {
            Verify();
            _fontSize = fontSize;
        }

        /// <summary>
        /// 设置背景颜色。
        /// </summary>
        /// <param name="background">背景颜色</param>
        public void SetBackground(Brush background)
        {
            if (background is null)
            {
                return;
            }

            Verify();
            _background = background;
        }

        /// <summary>
        /// 设置文字颜色。
        /// </summary>
        /// <param name="foreground">文字颜色。</param>
        public void SetForeground(Brush foreground)
        {

            if (foreground is null)
            {
                return;
            }

            Verify();
            _foreground = foreground;
        }

        /// <summary>
        /// 设置文本字体。
        /// </summary>
        /// <param name="typeface">文本字体。</param>
        public void SetTypeface(Typeface typeface)
        {
            if (typeface is null)
            {
                return;
            }

            Verify();
            _typeface = typeface;
        }

        /// <summary>
        /// 设置文本字体。
        /// </summary>
        /// <param name="family">文本字体。</param>
        public void SetTypeface(FontFamily family)
        {
            SetTypeface(family, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
        }

        /// <summary>
        /// 设置文本字体。
        /// </summary>
        /// <param name="family">文本字体。</param>
        /// <param name="fontStyle">文本样式。</param>
        public void SetTypeface(FontFamily family, FontStyle fontStyle)
        {
            SetTypeface(family, fontStyle, FontWeights.Normal, FontStretches.Normal);
        }

        /// <summary>
        /// 设置文本字体。
        /// </summary>
        /// <param name="fontFamily">文本字体。</param>
        /// <param name="fontStyle">文本样式。</param>
        /// <param name="fontWeight">文本粗细。</param>
        public void SetTypeface(FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight)
        {
            SetTypeface(new Typeface(fontFamily, fontStyle, fontWeight, FontStretches.Normal));
        }

        /// <summary>
        /// 设置文本字体。
        /// </summary>
        /// <param name="fontFamily">文本字体。</param>
        /// <param name="fontStyle">文本样式。</param>
        /// <param name="fontWeight">文本粗细。</param>
        /// <param name="fontStretch">FontStretch。</param>
        public void SetTypeface(FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch)
        {
            SetTypeface(new Typeface(fontFamily, fontStyle, fontWeight, fontStretch));
        }

        /// <summary>
        /// 设置文本装饰器集合。
        /// </summary>
        /// <param name="decorationCollection">文本装饰器集合，必须为非空对象才能生效。</param>
        public void SetTextDecorations(TextDecorationCollection decorationCollection)
        {
            if (decorationCollection is null)
            {
                return;
            }

            Verify();
            _textDecorationCollection = decorationCollection;
        }

        /// <summary>
        /// 设置文本特效集合。
        /// </summary>
        /// <param name="effectCollection">文本特效集合，必须为非空对象才能生效。</param>
        public void SetTextEffects(TextEffectCollection effectCollection)
        {
            if (effectCollection is null)
            {
                return;
            }

            Verify();
            _textEffectCollection = effectCollection;
        }

        /// <summary>
        /// 设置基准线对齐方式。
        /// </summary>
        /// <param name="baselineAlignment">基准线对齐方式</param>
        public void SetBaselineAlignment(BaselineAlignment baselineAlignment)
        {
            Verify();

            _baselineAlignment = baselineAlignment;
        }

        /// <summary>
        /// 设置为不可变。
        /// </summary>
        public GenericTextRunProperties Immutable()
        {
            IsImmutable = true;
            return this;
        }

        /// <summary>
        /// 是否不可变。
        /// </summary>
        public bool IsImmutable { get; private set; }

        /// <summary>
        /// 基准线对齐方式。
        /// </summary>
        public sealed override BaselineAlignment BaselineAlignment => _baselineAlignment;

        /// <summary>
        /// 文本字号
        /// </summary>
        public sealed override double FontHintingEmSize => _fontSize;

        /// <summary>
        /// 文本字号
        /// </summary>
        public sealed override double FontRenderingEmSize => _fontSize;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public sealed override Brush BackgroundBrush => _background;

        /// <summary>
        /// 
        /// </summary>
        public sealed override CultureInfo CultureInfo => _cultureInfo;

        /// <summary>
        /// 文字颜色
        /// </summary>
        public sealed override Brush ForegroundBrush => _foreground;

        /// <summary>
        /// 文本装饰器集合。
        /// </summary>
        /// <remarks>
        /// 用于存储 TextDecoration 对象的集合。TextDecoration 主要用于文本的装饰，例如 下划线、删除线、上划线等。它可以用于增强文本的可读性或强调特定内容。
        /// <para>例如，在 TextBlock 控件中，你可以使用 TextDecorationCollection 来给文本添加下划线或删除线，使其更具视觉层次感。</para>
        /// </remarks>
        public sealed override TextDecorationCollection TextDecorations => _textDecorationCollection;

        /// <summary>
        /// 文本特效的集合。
        /// </summary>
        /// <remarks>
        /// 用于存储 TextEffect 对象的集合。TextEffect 允许你对文本应用各种视觉效果，例如阴影、模糊、渐变等。这在创建富文本显示或增强 UI 视觉效果时非常有用。
        /// </remarks>
        public sealed override TextEffectCollection TextEffects => _textEffectCollection;

        /// <summary>
        /// 文本字体。
        /// </summary>
        public sealed override Typeface Typeface => _typeface;
    }
}