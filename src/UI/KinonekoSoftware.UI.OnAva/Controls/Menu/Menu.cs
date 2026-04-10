using KinonekoSoftware.UI;

namespace KinonekoSoftware.UI.Controls
{
    public class Menu : Avalonia.Controls.Menu
    {
        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new MenuItem();
    }

    public class ContextMenu : Avalonia.Controls.ContextMenu
    {
        protected override Control CreateContainerForItemOverride(object item, int index, object recycleKey) => new MenuItem();
    }


    public class MenuItem : Avalonia.Controls.MenuItem
    {
        public new static readonly StyledProperty<Geometry>     IconProperty;

        public static readonly StyledProperty<bool>     IsFilledProperty;
        public static readonly StyledProperty<double>   IconSizeProperty;
        public static readonly StyledProperty<IconMode> IconModeProperty;
        public static readonly StyledProperty<int>      StrokeThicknessProperty;

        static MenuItem()
        {
            IconProperty            = AvaloniaProperty.Register<MenuItem, Geometry>(nameof(Icon));
            IsFilledProperty        = AvaloniaProperty.Register<MenuItem, bool>(nameof(IsFilled));
            IconSizeProperty        = AvaloniaProperty.Register<MenuItem, double>(nameof(IconSize));
            IconModeProperty        = AvaloniaProperty.Register<MenuItem, IconMode>(nameof(IconMode));
            StrokeThicknessProperty = AvaloniaProperty.Register<MenuItem, int>(nameof(StrokeThickness), 1);
        }




        //------------------------------------------------
        //
        //  Properties
        //
        //------------------------------------------------

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

        public new Geometry Icon
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