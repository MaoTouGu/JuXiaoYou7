using Avalonia;
using Avalonia.Media;
using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls
{
    using Avalonia.Controls.Metadata;
    using Control = TemplatedControl;

    [PseudoClasses(":filled", ":stroke")]
    public class Iconify : Control
    {

        public static readonly StyledProperty<Geometry> IconProperty;
        public static readonly StyledProperty<bool>     IsFilledProperty;
        public static readonly StyledProperty<int>      StrokeThicknessProperty;
        public static readonly StyledProperty<double>   IconSizeProperty;
        public static readonly StyledProperty<IconMode> IconModeProperty;

        static Iconify()
        {
            IconProperty            = AvaloniaProperty.Register<Iconify, Geometry>(nameof(Geometry));
            IsFilledProperty        = AvaloniaProperty.Register<Iconify, bool>(nameof(IsFilled));
            IconSizeProperty        = AvaloniaProperty.Register<Iconify, double>(nameof(IconSize));
            IconModeProperty        = AvaloniaProperty.Register<Iconify, IconMode>(nameof(IconMode));
            StrokeThicknessProperty = AvaloniaProperty.Register<Iconify, int>(nameof(StrokeThickness));
        }


        public IconMode IconMode
        {
            get => GetValue(IconModeProperty);
            set => SetValue(IconModeProperty, value);
        }

        public double IconSize
        {
            get => GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public bool IsFilled
        {
            get => GetValue(IsFilledProperty);
            set => SetValue(IsFilledProperty, value);
        }

        public Geometry Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public int StrokeThickness
        {
            get => GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
    }
}