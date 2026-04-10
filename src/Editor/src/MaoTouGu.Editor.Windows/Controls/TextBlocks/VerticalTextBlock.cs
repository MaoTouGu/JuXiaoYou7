using MaoTouGu.Foundation.Mathematics;

namespace MaoTouGu.Editor.Controls
{
    public class VerticalTextBlock : Control
    {
        //------------------------------------------------------------
        //
        //                    Nested Class
        //
        //------------------------------------------------------------

        //
        // 定义仅用于当前TextBlockEx类型的文本渲染行。
        // TextVisualLine用于实现包装布局位置信息以及文本行。
        class TextVisualLine
        {
            public void Draw(DrawingContext dc)
            {
                Line.Draw(dc, new Point(X, Y), InvertAxes.None);
            }

            public double H => Line.Height;
            public double W => Line.Width;

            public double   X    { get; set; }
            public double   Y    { get; set; }
            public TextLine Line { get; init; }
        }

        public static readonly DependencyProperty TextProperty;
        //------------------------------------------------------------
        //
        //                    Static Constructors
        //
        //------------------------------------------------------------
        static VerticalTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalTextBlock), new FrameworkPropertyMetadata(typeof(VerticalTextBlock)));
            

            //
            // 添加对行高的支持。
            TextBlock.LineHeightProperty.AddOwner(typeof(VerticalTextBlock));
            
            TextProperty =
                DependencyProperty.Register(
                                            nameof(Text),
                                            typeof(string),
                                            typeof(VerticalTextBlock),
                                            new FrameworkPropertyMetadata(null,
                                                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                          FrameworkPropertyMetadataOptions.AffectsRender));
            PaddingProperty.OverrideMetadata(typeof(VerticalTextBlock),
                                             new FrameworkPropertyMetadata(default(Thickness),
                                                                           FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                           FrameworkPropertyMetadataOptions.AffectsRender));
        }

        private readonly TextFormatter        _Formatter;
        private readonly List<TextVisualLine> _Lines;

        private DefaultTextRunProperties       _RunProperty;
        private GenericTextParagraphProperties _ParagraphProperty;

        public VerticalTextBlock()
        {
            _Formatter = TextFormatter.Create();
            _Lines     = new List<TextVisualLine>(32);
        }

        //------------------------------------------------------------
        //
        //                    Private Methods
        //
        //------------------------------------------------------------
        void InitializeTextRenderer()
        {
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
        }

        double GetWidth(double canvasW, double maxW, double minW, HorizontalAlignment hAlign, Thickness padding)
        {
            return hAlign switch
            {
                HorizontalAlignment.Left  => padding.Left,
                HorizontalAlignment.Right => canvasW                 - padding.Right - maxW,
                _                         => ((canvasW - maxW) / 2d) - ((padding.Left + padding.Right) / 2d),
            };
        }

        Size MeasureText(Size size, string text)
        {
            var y          = 0d;
            var h          = 0d;
            var w          = size.Width;
            var lineHeight = DoubleStatic.GetAvailableValue(TextBlock.GetLineHeight(this));
            var iterators = text.Select(x => new GenericTextSource { Text = x.ToString(), Properties = _RunProperty })
                                .Select(x => _Formatter.FormatLine(x, 0, w, _ParagraphProperty, null));

            foreach (var textLine in iterators)
            {
                _Lines.Add(new TextVisualLine
                {
                    X    = (w - textLine.Width) / 2d,
                    Y    = y,
                    Line = textLine,
                });

                h += textLine.Height;
                y += textLine.Height + lineHeight;
            }

            var maxW = 0d;
            var minW = 9999d;
            var sumH = 0d;

            foreach (var line in _Lines)
            {
                maxW =  Math.Max(line.W, maxW);
                minW =  Math.Min(line.W, minW);
                sumH += line.H + lineHeight;
            }

            var    padding = Padding;
            var    hAlign  = HorizontalContentAlignment;
            double y2;

            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Bottom:
                    y2 = (size.Height - sumH) - padding.Bottom;
                    break;
                case VerticalAlignment.Top:
                    y2 = 0;
                    break;
                case VerticalAlignment.Center:
                default:
                    y2 = ((size.Height - sumH) / 2d) - ((padding.Bottom + padding.Top) / 2d);
                    break;
            }

            foreach (var line in _Lines)
            {
                line.Y =  y2;
                line.X =  GetWidth(size.Width, maxW, minW, hAlign, padding);
                y2     += line.H + lineHeight;
            }

            return new Size(w, h);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _Lines.Clear();

            var text = Text;

            if (string.IsNullOrEmpty(text))
            {
                return new Size();
            }

            InitializeTextRenderer();
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
        //                    Properties
        //
        //------------------------------------------------------------


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