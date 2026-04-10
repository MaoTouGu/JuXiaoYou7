namespace MaoTouGu.Editor.Formatters
{
    public class HighlightTextSource : TextSource
    {
        private bool _initialize;

        public void Initialize()
        {
            if (!string.IsNullOrEmpty(Text) &&
                Length > -1 &&
                Length + Offset <= Text.Length)
            {
                _initialize = true;
            }
        }

        public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit)
        {
            var cbr = new CharacterBufferRange(Text, 0, textSourceCharacterIndexLimit);

            //
            // 似乎是双击鼠标，全选功能？
            return new TextSpan<CultureSpecificCharacterBufferRange>(
                                                                     textSourceCharacterIndexLimit,
                                                                     new CultureSpecificCharacterBufferRange(CultureInfo.CurrentUICulture, cbr)
                                                                    );
        }

        public override int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex)
        {
            //
            // WPF内部实现的SimpleLine、ComplexLine是直接返回textSourceCharacterIndex变量的。
            // AvalonEdit，是不实现此方法的。
            // 
            return textSourceCharacterIndex;
        }

        public override TextRun GetTextRun(int tscIdx)
        {
            if (!_initialize)
            {
                throw new InvalidOperationException();
            }

            if (Text.Length <= tscIdx)
            {
                return new TextEndOfParagraph(1);
            }

            if (tscIdx < Offset)
            {
                return new TextCharacters(
                                          Text,
                                          tscIdx,
                                          Offset - tscIdx,
                                          TextProperties);
            }

            if (tscIdx >= Offset + Length)
            {
                return new TextCharacters(
                                          Text,
                                          tscIdx,
                                          Text.Length - Offset - Length,
                                          TextProperties);
            }
            

            return new TextCharacters(
                                      Text,
                                      Offset,
                                      Length,
                                      HighlightProperties);
        }

        public int    Offset { get; set; }
        public int    Length { get; set; }
        public string Text   { get; set; }

        public TextRunProperties HighlightProperties { get; set; }
        public TextRunProperties TextProperties      { get; set; }
    }
}