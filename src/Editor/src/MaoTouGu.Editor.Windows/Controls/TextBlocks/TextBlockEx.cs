using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MaoTouGu.Foundation;
using TextFormatter = System.Windows.Media.TextFormatting.TextFormatter;
using TextLine = System.Windows.Media.TextFormatting.TextLine;

namespace MaoTouGu.Editor.Controls
{
    public partial class TextBlockEx : Control
    {

        //------------------------------------------------------------
        //
        //                    Nested Class
        //
        //------------------------------------------------------------

        //
        // 定义仅用于当前TextBlockEx类型的文本渲染行。
        // TextVisualLine用于实现包装布局位置信息以及文本行。
        readonly record struct TextVisualLine(double X, double Y, TextLine Line)
        {
            public void Draw(DrawingContext dc)
            {
                Line.Draw(dc, new Point(X, Y), InvertAxes.None);
            }
        }

        //------------------------------------------------------------
        //
        //                    Static Constructors
        //
        //------------------------------------------------------------
        static TextBlockEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockEx), new FrameworkPropertyMetadata(typeof(TextBlockEx)));
            TextProperty =
                DependencyProperty.Register(
                                            nameof(Text),
                                            typeof(string),
                                            typeof(TextBlockEx),
                                            new FrameworkPropertyMetadata(null,
                                                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                          FrameworkPropertyMetadataOptions.AffectsRender));
            ParagraphSpacingProperty =
                DependencyProperty.Register(
                                            nameof(ParagraphSpacing),
                                            typeof(double),
                                            typeof(TextBlockEx),
                                            new FrameworkPropertyMetadata(16d,
                                                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                          FrameworkPropertyMetadataOptions.AffectsRender));
            FirstLetterIndentProperty =
                DependencyProperty.Register(
                                            nameof(FirstLetterIndent),
                                            typeof(int),
                                            typeof(TextBlockEx),
                                            new FrameworkPropertyMetadata(2,
                                                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                          FrameworkPropertyMetadataOptions.AffectsRender));
            EnableFirstLetterIndentProperty =
                DependencyProperty.Register(
                                            nameof(EnableFirstLetterIndent),
                                            typeof(bool),
                                            typeof(TextBlockEx),
                                            new FrameworkPropertyMetadata(Boxing.False,
                                                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                          FrameworkPropertyMetadataOptions.AffectsRender));

            //
            // 添加对行高的支持。
            TextBlock.LineHeightProperty.AddOwner(typeof(TextBlockEx));
        }

        //------------------------------------------------------------
        //
        //                    Initialize
        //
        //------------------------------------------------------------

        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty ParagraphSpacingProperty;
        public static readonly DependencyProperty FirstLetterIndentProperty;
        public static readonly DependencyProperty EnableFirstLetterIndentProperty;

        private readonly TextFormatter        _Formatter;
        private readonly List<TextVisualLine> _Lines;

        private GenericTextSource              _TextSource;
        private DefaultTextRunProperties       _RunProperty;
        private GenericTextParagraphProperties _ParagraphProperty;

        //------------------------------------------------------------
        //
        //                    Initialize
        //
        //------------------------------------------------------------
        public TextBlockEx()
        {
            _Formatter = TextFormatter.Create();
            _Lines     = new List<TextVisualLine>(32);
        }

        //------------------------------------------------------------
        //
        //                    Private Methods
        //
        //------------------------------------------------------------
        void InitializeTextRenderer(string text)
        {
            //
            //
            _TextSource  = new GenericTextSource();
            _RunProperty = new DefaultTextRunProperties(VisualTreeHelper.GetDpi(this));
            _ParagraphProperty = new GenericTextParagraphProperties(
                                                                    GetFlowDirection(this),
                                                                    TextAlignment.Left,
                                                                    null,
                                                                    _RunProperty,
                                                                    TextWrapping.Wrap,
                                                                    TextBlock.GetLineHeight(this),
                                                                    0);
            _RunProperty.SetForeground(Foreground);
            _RunProperty.SetFontSize(FontSize);
            _RunProperty.SetTypeface(FontFamily, FontStyle, FontWeight, FontStretch);

            _TextSource.Text       = text;
            _TextSource.Properties = _RunProperty;
        }

        Size MeasureText(Size size, string text)
        {
            var enable  = EnableFirstLetterIndent;
            var indent  = FirstLetterIndent * XWidth;
            var spacing = ParagraphSpacing;
            var height  = double.IsNaN(_ParagraphProperty.LineHeight) ? 0d : _ParagraphProperty.LineHeight;

            var indexOfLine = 0;
            var offset      = 0;
            var w           = double.IsInfinity(size.Width) ? 600: size.Width;
            var h           = 0d;
            var x           = 0d;
            var y           = 0d;
            var maxW        = 0d;

            while (offset < text.Length)
            {
                double w2;

                if (enable && indexOfLine == 0)
                {
                    x  = indent;
                    w2 = w - indent;
                }
                else
                {
                    x  = 0;
                    w2 = w;
                }

                //
                //
                var line         = _Formatter.FormatLine(_TextSource, offset, w2, _ParagraphProperty, null);
                var newLineCount = line.NewlineLength;

                //
                //
                _Lines.Add(new TextVisualLine(x, y, line));

                //
                //
                if (newLineCount > 0)
                {
                    y           += spacing;
                    h           += spacing;
                    indexOfLine =  0;
                }
                else
                {
                    indexOfLine++;
                }

                y      += (height + line.Height);
                offset += line.Length;
                h      += (height + line.Height);
                maxW   =  Math.Max(maxW, line.Width);
            }

            return new Size(maxW, h);
        }

        //------------------------------------------------------------
        //
        //                    Override Methods
        //
        //------------------------------------------------------------
        protected override Size MeasureOverride(Size constraint)
        {
            _Lines.Clear();

            var text = Text;

            if (string.IsNullOrEmpty(text))
            {
                return new Size();
            }

            InitializeTextRenderer(text);
            return MeasureText(constraint, text);
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            foreach (var visualLine in _Lines)
            {
                visualLine.Draw(drawingContext);
            }
        }

        //------------------------------------------------------------
        //
        //                    Initialize
        //
        //------------------------------------------------------------
        public double ParagraphSpacing
        {
            get => (double)GetValue(ParagraphSpacingProperty);
            set => SetValue(ParagraphSpacingProperty, value);
        }

        public int FirstLetterIndent
        {
            get => (int)GetValue(FirstLetterIndentProperty);
            set => SetValue(FirstLetterIndentProperty, value);
        }

        public bool EnableFirstLetterIndent
        {
            get => (bool)GetValue(EnableFirstLetterIndentProperty);
            set => SetValue(EnableFirstLetterIndentProperty, Boxing.Box(value));
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        internal double XWidth
        {
            get
            {
                if (_ParagraphProperty is null)
                {
                    return 0d;
                }

                var ts   = new GenericTextSource { Text = "X", Properties = _RunProperty };
                var line = _Formatter.FormatLine(ts, 0, 100000, _ParagraphProperty, null);

                return line.Width;
            }
        }
    }
}