
namespace MaoTouGu.Editor.Formatters
{
    public class GenericTextParagraphProperties : TextParagraphProperties, IImmutable<GenericTextParagraphProperties>
    {
        private double _indent;
        private double _lineHeight;


        private FlowDirection _direction;
        private TextWrapping  _wrapping;
        private TextAlignment _alignment;

        private TextRunProperties    _textRunProperties;
        private TextMarkerProperties _markerProperties;
        
        public GenericTextParagraphProperties() {}

        public GenericTextParagraphProperties(TextRunProperties textRunProperties)
        {
            _textRunProperties = textRunProperties;
        }
        
        public GenericTextParagraphProperties(
            FlowDirection        flowDirection,
            TextAlignment        textAlignment,
            TextMarkerProperties markerProperties,
            TextRunProperties    defaultTextRunProperties,
            TextWrapping         textWrap,
            double               lineHeight,
            double               indent)
        {
            _direction            = flowDirection;
            _alignment            = textAlignment;
            _markerProperties     = markerProperties;
            _textRunProperties    = defaultTextRunProperties;
            _wrapping             = textWrap;
            _lineHeight           = lineHeight;
            _indent               = indent;
        }

        private void Verify()
        {
            if (IsImmutable)
            {
                throw new InvalidOperationException("无法修改一个已经变成不可变类型的对象实例。");
            }
        }

        /// <summary>
        /// 设置为不可变。
        /// </summary>
        public GenericTextParagraphProperties Immutable()
        {
            IsImmutable = true;
            return this;
        }

        /// <summary>
        /// 设置多倍行距。
        /// </summary>
        /// <param name="scale">表示倍速，实际数值为scale * 0.25x。</param>
        /// <param name="fontSize">字号。</param>
        public void SetLineHeight(int scale, double fontSize)
        {
            Verify();
            _lineHeight = fontSize * (Math.Clamp(scale, 1, 32) / 4d);
        }

        /// <summary>
        /// 设置行距。
        /// </summary>
        /// <param name="lineHeight">行距。</param>
        public void SetLineHeight(double lineHeight)
        {
            Verify();
            _lineHeight = lineHeight;
        }

        /// <summary>
        /// 设置段落行缩进。
        /// </summary>
        /// <param name="indent">段落行缩进</param>
        public void SetIndent(double indent)
        {
            Verify();
            _indent = indent;
        }

        /// <summary>
        /// 设置内容流方向。
        /// </summary>
        /// <param name="direction">内容流方向</param>
        public void SetFlowDirection(FlowDirection direction)
        {
            Verify();
            _direction = direction;
        }

        /// <summary>
        /// 设置文本换行策略。
        /// </summary>
        /// <param name="wrapping">文本换行策略</param>
        public void SetTextWrapping(TextWrapping wrapping)
        {
            Verify();
            _wrapping = wrapping;
        }

        /// <summary>
        /// 设置文本对齐属性。
        /// </summary>
        /// <param name="alignment">文本对齐属性</param>
        public void SetTextAlignment(TextAlignment alignment)
        {
            Verify();
            _alignment = alignment;
        }

        /// <summary>
        /// 设置默认的TextRun样式属性。
        /// </summary>
        public void SetTextRunProperties(TextRunProperties textRunProperties)
        {
            Verify();
            _textRunProperties = textRunProperties;
        }

        /// <summary>
        /// 设置TextMarker的样式属性。
        /// </summary>
        /// <param name="markerProperties">TextMarker的样式属性。</param>
        public void SetTextMarkerProperties(TextMarkerProperties markerProperties)
        {
            Verify();
            _markerProperties = markerProperties;
        }
        
        
        /// <summary>
        /// 默认的TextRun样式属性。
        /// </summary>
        public sealed override TextRunProperties DefaultTextRunProperties => _textRunProperties;

        /// <summary>
        /// 文本对齐属性
        /// </summary>
        public sealed override TextAlignment TextAlignment => _alignment;

        /// <summary>
        /// TextMarkerProperties 是一个用于定义文本标记的抽象类，通常用于创建独特的视觉元素，例如项目符号和自动编号的列表。它可以帮助格式化文本，使其更具可读性和结构化
        /// </summary>
        /// <remarks>
        /// <para>它可以帮助格式化文本，使其更具可读性和结构化在 TextParagraphProperties 类中，TextMarkerProperties 还用于指定段落中第一行的标记特征。这对于文本布局和格式化非常重要，尤其是在处理复杂的文本排版时。</para>
        /// <para>如果你正在开发涉及文本格式化的应用程序，TextMarkerProperties 可能会是一个有用的工具！你是想在具体的项目中使用它吗？</para>
        /// </remarks>
        public sealed override TextMarkerProperties TextMarkerProperties => _markerProperties;

        /// <summary>
        /// 文本换行策略
        /// </summary>
        public sealed override TextWrapping TextWrapping => _wrapping;

        /// <summary>
        /// 是否不可变。
        /// </summary>
        public bool IsImmutable { get; private set; }

        /// <summary>
        /// 内容流方向
        /// </summary>
        public sealed override FlowDirection FlowDirection => _direction;

        /// <summary>
        /// 行号
        /// </summary>
        public sealed override double LineHeight => _lineHeight;

        /// <summary>
        /// 该属性因为没有实际案例表示
        /// </summary>
        public sealed override double Indent => _indent;

        /// <summary>
        /// 该属性因为没有实际案例表示
        /// </summary>
        public sealed override bool FirstLineInParagraph => false;
    }
}