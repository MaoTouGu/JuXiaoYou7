
namespace MaoTouGu.Editor.Formatters
{
    public class GenericTextSource : TextSource
    {
        internal static readonly char[] NewLineArray = { '\r', '\n' };

        private bool   _initialize;
        private string _text;

        public void Initialize(ITextBuffer textBuffer)
        {
            _initialize = true;
            _text       = textBuffer.GetText();
        }

        public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit)
        {
            var cbr = new CharacterBufferRange(_text, 0, textSourceCharacterIndexLimit);

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

            if (_text.Length <= tscIdx)
            {
                return new TextEndOfParagraph(1);
            }

            return new TextCharacters(
                                      _text,
                                      tscIdx,
                                      _text.Length - tscIdx,
                                      Properties);
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _initialize = !string.IsNullOrEmpty(value);
            }
        }
        public TextRunProperties Properties { get; set; }
    }
}