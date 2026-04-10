using System.Windows.Shapes;

namespace KinonekoSoftware.UX
{
    public class GeometryDegreeControl : WrapPanel
    {

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            nameof(StrokeThickness),
            typeof(int),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure, OnInvalidatePath));

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            nameof(Stroke),
            typeof(Brush),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.AffectsMeasure, OnInvalidatePath));


        public static readonly DependencyProperty IconGapProperty = DependencyProperty.Register(
            nameof(IconGap),
            typeof(int),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsMeasure, OnInvalidatePath));


        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(Geometry),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(default(Geometry), FrameworkPropertyMetadataOptions.AffectsMeasure, OnInvalidatePath));


        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
            nameof(IconSize),
            typeof(int),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(13, FrameworkPropertyMetadataOptions.AffectsMeasure, OnInvalidatePath));


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(int),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender, OnInvalidatePath));


        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            nameof(Maximum),
            typeof(int),
            typeof(GeometryDegreeControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender, OnInvalidateRenderData));

        private static void OnInvalidateRenderData(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (GeometryDegreeControl)d;
            c.GenerateElement();
        }

        private static void OnInvalidatePath(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (GeometryDegreeControl)d;
            if (c.Children.Count == 0)
                c.GenerateElement();
            c.SyncElement();
        }

        /*
         *
        protected const string Thumb = "F1 M24,24z M0,0z M20.84,4.61A5.5,5.5,0,0,0,13.06,4.61L12,5.67 10.94,4.61A5.5,5.5,0,0,0,3.16,12.39L4.22,13.45 12,21.23 19.78,13.45 20.84,12.39A5.5,5.5,0,0,0,20.84,4.61z";
        protected const string Heart = "F1 M24,24z M0,0z M20.84,4.61A5.5,5.5,0,0,0,13.06,4.61L12,5.67 10.94,4.61A5.5,5.5,0,0,0,3.16,12.39L4.22,13.45 12,21.23 19.78,13.45 20.84,12.39A5.5,5.5,0,0,0,20.84,4.61z";
        protected const string Rate  = "F1 M24,24z M0,0z M12,2L12,2 15.09,8.26 22,9.27 17,14.14 18.18,21.02 12,17.77 5.82,21.02 7,14.14 2,9.27 8.91,8.26 12,2z";
         */


        private void OnElementClick(object sender, RoutedEventArgs e)
        {
            var p     = (FrameworkElement)sender;
            var index = (int)p.Tag;
            
            if (Value == 1)
            {
                Value = 0;
            }
            else
            {
                Value = index;
            }
            HighlightElement();
        }

        protected virtual Border GenerateElement(bool isFirst, bool isLast, int index, double size)
        {
            var path = new Path
            {
                Data                = Icon,
                Width               = size,
                Height              = size,
                UseLayoutRounding   = true,
                SnapsToDevicePixels = true,
                Stretch             = Stretch.Uniform,
                Tag                 = index,
            };

            var bd = new Border
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Child      = path,
                Width      = size,
                Height     = size,
                Tag        = index,
            };

            
            bd.MouseDown   += OnElementClick;
            path.MouseDown += OnElementClick;

            return bd;
        }

        private void SyncElement()
        {
            var thickness = Math.Clamp(StrokeThickness, 1, 500);
            var size      = Math.Clamp(IconSize, 9, 512);
            for (var i = 0; i < Children.Count; i++)
            {
                if (Children[i] is not Border { Child: Path p } b)
                {
                    continue;
                }

                b.Width  = size;
                b.Height = size;
                p.Data   = Icon;
                p.Width  = size;
                p.Height = size;

                if (Value - 1 >= i)
                {
                    p.Fill            = Stroke;
                    p.StrokeThickness = 0;
                    p.Stroke          = null;
                }
                else
                {
                    p.Fill            = XamlCore.Transparent;
                    p.StrokeThickness = thickness;
                    p.Stroke          = Stroke;
                }
            }
        }

        protected void GenerateElement()
        {
            var count = Math.Clamp(Value, 0, 100);
            var size  = Math.Clamp(IconSize, 9, 512);

            //
            // Clear
            ClearElementImpl();

            if (count == 0)
            {
                return;
            }

            GenerateElementImpl(count, size);
            InvalidateMeasure();
            HighlightElement();
        }

        private void ClearElementImpl()
        {
            for (var i = 0; i < Children.Count; i++)
            {
                if (Children[i] is not Border { Child: Path p}b)
                {
                    continue;
                }
                
                b.MouseDown -= OnElementClick;
                p.MouseDown -= OnElementClick;
            }

            Children.Clear();
        }

        private void GenerateElementImpl(int count, int size)
        {

            if (count >= 1)
            {
                Children.Add(GenerateElement(true, false, 1, size));
            }

            if (count == 2)
            {
                Children.Add(GenerateElement(false, true, 2, size));
            }
            else
            {
                for (var i = 1; i < count; i++)
                {
                    Children.Add(GenerateElement(false, i == count - 1, i + 1, size));
                }
            }

            InvalidateMeasure();
        }

        protected void HighlightElement()
        {
            var thickness = Math.Clamp(StrokeThickness, 1, 500);

            if (Children.Count == 0 && Children.Count < Maximum)
            {
                return;
            }

            for (var i = 0; i < Maximum; i++)
            {
                if (Children[i] is not Border { Child: Path p })
                {
                    continue;
                }

                if (Value - 1 >= i)
                {
                    p.Fill            = Stroke;
                    p.StrokeThickness = 0;
                    p.Stroke          = null;
                }
                else
                {
                    p.Fill            = XamlCore.Transparent;
                    p.StrokeThickness = thickness;
                    p.Stroke          = Stroke;
                }
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(Maximum * IconSize + (Maximum - 1) * IconGap, IconSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0d;

            foreach (FrameworkElement element in InternalChildren)
            {
                element.Arrange(new Rect(x, 0, IconSize, IconSize));
                x = x + IconGap + IconSize;
            }

            return base.ArrangeOverride(finalSize);
        }

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        
        public int IconSize
        {
            get => (int)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public int IconGap
        {
            get => (int)GetValue(IconGapProperty);
            set => SetValue(IconGapProperty, value);
        }

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public int StrokeThickness
        {
            get => (int)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
    }
}