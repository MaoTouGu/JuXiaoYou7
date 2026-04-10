using Avalonia;
using MaoTouGu.Foundation;

namespace KinonekoSoftware.UI.Controls
{
    public sealed class ControlAssist : AvaloniaObject
    {
        public static readonly AttachedProperty<CornerRadius> CornerRadiusProperty;
        public static readonly AttachedProperty<Geometry>     IconProperty;
        public static readonly AttachedProperty<Dock>         IconDockProperty;
        public static readonly AttachedProperty<Thickness>    IconMarginProperty;
        public static readonly AttachedProperty<double>       IconSizeProperty;
        public static readonly AttachedProperty<bool>         IsFilledProperty;
        public static readonly AttachedProperty<double>       StrokeThicknessProperty;
        
        public static readonly AttachedProperty<IBrush>       ForegroundProperty;
        public static readonly AttachedProperty<double>       FontSizeProperty;
        
        public static readonly AttachedProperty<object> ObjectProperty;
        public static readonly AttachedProperty<object> ObjectViewProperty;

        static ControlAssist()
        {
            CornerRadiusProperty    = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, CornerRadius>("CornerRadius");
            IconProperty            = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, Geometry>("Icon");
            IconDockProperty        = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, Dock>("IconDock");
            IconMarginProperty      = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, Thickness>("IconMargin");
            IconSizeProperty        = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, double>("IconSize");
            IsFilledProperty        = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, bool>("IsFilled");
            StrokeThicknessProperty = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, double>("StrokeThickness");
            
            ForegroundProperty = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, IBrush>("Foreground");
            FontSizeProperty   = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, double>("FontSize");
            
            ObjectProperty     = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, object>("Object");
            ObjectViewProperty = AvaloniaProperty.RegisterAttached<ControlAssist, TemplatedControl, object>("ObjectView");
        }
        
        
        #region IsFilled

        
        public static void SetIsFilled(Control element, bool value)
        {
            element.SetValue(IsFilledProperty, Boxing.Box(value));
        }

        public static bool GetIsFilled(Control element)
        {
            return element.GetValue(IsFilledProperty);
        }

        #endregion
        
        #region Icon

        
        public static void SetIcon(Control element, Geometry value)
        {
            element.SetValue(IconProperty, value);
        }

        public static Geometry GetIcon(Control element)
        {
            return element.GetValue(IconProperty);
        }

        #endregion
        
        #region IconDock

        
        public static void SetIconDock(Control element, Dock value)
        {
            element.SetValue(IconDockProperty, value);
        }

        public static Dock GetIconDock(Control element)
        {
            return element.GetValue(IconDockProperty);
        }

        #endregion

        #region IconMargin


        public static void SetIconMargin(Control element, Thickness value)
        {
            element.SetValue(IconMarginProperty, value);
        }

        public static Thickness GetIconMargin(Control element)
        {
            return element.GetValue(IconMarginProperty);
        }

        #endregion
        
        #region IconSize

        
        public static void SetIconSize(Control element, double value)
        {
            element.SetValue(IconSizeProperty, value);
        }

        public static double GetIconSize(Control element)
        {
            return element.GetValue(IconSizeProperty);
        }

        #endregion
        
        #region StrokeThickness

        
        public static void SetStrokeThickness(Control element, double value)
        {
            element.SetValue(StrokeThicknessProperty, value);
        }

        public static double GetStrokeThickness(Control element)
        {
            return element.GetValue(StrokeThicknessProperty);
        }

        #endregion

        #region CornerRadius

        
        public static void SetCornerRadius(Control element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static CornerRadius GetCornerRadius(Control element)
        {
            return element.GetValue(CornerRadiusProperty);
        }

        #endregion
        
        #region Foreground

        
        public static void SetForeground(Control element, IBrush value)
        {
            element.SetValue(ForegroundProperty, value);
        }

        public static IBrush GetForeground(Control element)
        {
            return element.GetValue(ForegroundProperty);
        }

        #endregion
        
        #region Foreground

        
        public static void SetFontSize(Control element, double value)
        {
            element.SetValue(FontSizeProperty, value);
        }

        public static double GetFontSize(Control element)
        {
            return element.GetValue(FontSizeProperty);
        }

        #endregion
        
        #region ObjectView

        
        public static void SetObjectView(AvaloniaObject element, object value)
        {
            element.SetValue(ObjectViewProperty, value);
        }

        public static object GetObjectView(AvaloniaObject element)
        {
            return (object)element.GetValue(ObjectViewProperty);
        }

        #endregion

        #region Object

        

        
        public static void SetObject(AvaloniaObject element, object value)
        {
            element.SetValue(ObjectProperty, value);
        }

        public static object GetObject(AvaloniaObject element)
        {
            return (object)element.GetValue(ObjectProperty);
        }
        #endregion
    }
}