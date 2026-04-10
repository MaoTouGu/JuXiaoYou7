
namespace MaoTouGu.Editor.Formatters
{
    public class DefaultMetricTextSource : TextSource
    {
        private const string _text = "x";

        public DefaultMetricTextSource(double pixelsPerDip)
        {
            TextRunProperties   = new DefaultTextRunProperties(pixelsPerDip);
            ParagraphProperties = new GenericTextParagraphProperties(TextRunProperties);
        }

        public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit)
        {
            var cbr = new CharacterBufferRange(_text, 0, textSourceCharacterIndexLimit);

            //
            // 似乎是双击鼠标，全选功能？
            return new TextSpan<CultureSpecificCharacterBufferRange>(
                                                                     textSourceCharacterIndexLimit,
                                                                     new CultureSpecificCharacterBufferRange(CultureInfo.CurrentUICulture, cbr));
        }

        public override int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex)
        {
            //
            // WPF内部实现的SimpleLine、ComplexLine是直接返回textSourceCharacterIndex变量的。
            // AvalonEdit，是不实现此方法的。
            // 
            return textSourceCharacterIndex;
        }

        public override TextRun GetTextRun(int textSourceCharacterIndex)
        {
            if (textSourceCharacterIndex == 0)
                return new TextCharacters(_text,
                                          0,
                                          1,
                                          TextRunProperties);

            return new TextEndOfLine(1);
        }

        public TextParagraphProperties ParagraphProperties { get; }
        public TextRunProperties       TextRunProperties   { get; }
    }
}