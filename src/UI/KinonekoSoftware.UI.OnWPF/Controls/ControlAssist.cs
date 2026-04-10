using System.Windows;
using System.Windows.Media;


namespace KinonekoSoftware.UI.Controls
{
    public static class ControlAssist
    {
        public static readonly DependencyProperty CornerRadiusProperty;
        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty IconDockProperty;
        public static readonly DependencyProperty IconMarginProperty;
        public static readonly DependencyProperty IconSizeProperty;
        public static readonly DependencyProperty IsFilledProperty;
        public static readonly DependencyProperty StrokeThicknessProperty;

        public static readonly DependencyProperty ForegroundProperty;
        public static readonly DependencyProperty FontSizeProperty;
        public static readonly DependencyProperty IntroProperty;

        public static readonly DependencyProperty ToolBarProperty;
        public static readonly DependencyProperty CommandPanelProperty;
        public static readonly DependencyProperty ObjectViewProperty;
        public static readonly DependencyProperty ObjectProperty;

        private static readonly object CornerRadius_Zero;
        private static readonly object CornerRadius_Three;

        static ControlAssist()
        {
            CornerRadius_Zero  = new CornerRadius(0);
            CornerRadius_Three = new CornerRadius(3);

            CornerRadiusProperty = DependencyProperty.RegisterAttached(
                                                                       "CornerRadius",
                                                                       typeof(CornerRadius),
                                                                       typeof(ControlAssist),
                                                                       new PropertyMetadata(CornerRadius_Zero));
            IconProperty = DependencyProperty.RegisterAttached(
                                                               "Icon",
                                                               typeof(Geometry),
                                                               typeof(ControlAssist),
                                                               new PropertyMetadata(default(Geometry)));
            
            IconSizeProperty = DependencyProperty.RegisterAttached(
                                                                   "IconSize",
                                                                   typeof(double),
                                                                   typeof(ControlAssist),
                                                                   new PropertyMetadata(default(double)));
            
            IconDockProperty = DependencyProperty.RegisterAttached(
                                                                       "IconDock",
                                                                       typeof(Dock),
                                                                       typeof(ControlAssist),
                                                                       new PropertyMetadata(default(Dock)));
            IconMarginProperty = DependencyProperty.RegisterAttached(
                                                                    "IconMargin",
                                                                    typeof(Thickness),
                                                                    typeof(ControlAssist), 
                                                                    new PropertyMetadata(default(Thickness)));
            
            StrokeThicknessProperty = DependencyProperty.RegisterAttached(
                                                                          "StrokeThickness",
                                                                          typeof(double),
                                                                          typeof(ControlAssist),
                                                                          new PropertyMetadata(default(double)));
            IsFilledProperty = DependencyProperty.RegisterAttached(
                                                                   "IsFilled",
                                                                   typeof(bool),
                                                                   typeof(ControlAssist),
                                                                   new PropertyMetadata(Boxing.False));
            ForegroundProperty = DependencyProperty.RegisterAttached(
                                                                     "Foreground",
                                                                     typeof(Brush),
                                                                     typeof(ControlAssist),
                                                                     new PropertyMetadata(default(Brush)));
            FontSizeProperty = DependencyProperty.RegisterAttached(
                                                                   "FontSize", 
                                                                   typeof(double), 
                                                                   typeof(ControlAssist),
                                                                   new PropertyMetadata(default(double)));
            IntroProperty = DependencyProperty.RegisterAttached(
                                                                   "Intro", 
                                                                   typeof(string), 
                                                                   typeof(ControlAssist),
                                                                   new PropertyMetadata(default(string)));
            
            ToolBarProperty = DependencyProperty.RegisterAttached("ToolBar",
                                                                  typeof(object), 
                                                                  typeof(ControlAssist), 
                                                                  new PropertyMetadata(default(object)));
            CommandPanelProperty = DependencyProperty.RegisterAttached("CommandPanel",
                                                                      typeof(object), 
                                                                      typeof(ControlAssist), 
                                                                      new PropertyMetadata(default(object)));
            ObjectViewProperty = DependencyProperty.RegisterAttached("ObjectView",
                                                                         typeof(object), 
                                                                         typeof(ControlAssist), 
                                                                         new PropertyMetadata(default(object)));
            
            ObjectProperty = DependencyProperty.RegisterAttached("Object",
                                                                     typeof(object), 
                                                                     typeof(ControlAssist), 
                                                                     new PropertyMetadata(default(object)));
        }

        #region IsFilled

        
        public static void SetIsFilled(DependencyObject element, bool value)
        {
            element.SetValue(IsFilledProperty, Boxing.Box(value));
        }

        public static bool GetIsFilled(DependencyObject element)
        {
            return (bool)element.GetValue(IsFilledProperty);
        }

        #endregion
        
        #region Icon

        
        public static void SetIcon(DependencyObject element, Geometry value)
        {
            element.SetValue(IconProperty, value);
        }

        public static Geometry GetIcon(DependencyObject element)
        {
            return (Geometry)element.GetValue(IconProperty);
        }

        #endregion
        
        #region IconDock

        
        public static void SetIconDock(DependencyObject element, Dock value)
        {
            element.SetValue(IconDockProperty, value);
        }

        public static Dock GetIconDock(DependencyObject element)
        {
            return (Dock)element.GetValue(IconDockProperty);
        }

        #endregion

        #region IconMargin


        public static void SetIconMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(IconMarginProperty, value);
        }

        public static Thickness GetIconMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(IconMarginProperty);
        }

        #endregion
        
        #region IconSize

        
        public static void SetIconSize(DependencyObject element, double value)
        {
            element.SetValue(IconSizeProperty, value);
        }

        public static double GetIconSize(DependencyObject element)
        {
            return (double)element.GetValue(IconSizeProperty);
        }

        #endregion
        
        #region StrokeThickness

        
        public static void SetStrokeThickness(DependencyObject element, double value)
        {
            element.SetValue(StrokeThicknessProperty, value);
        }

        public static double GetStrokeThickness(DependencyObject element)
        {
            return (double)element.GetValue(StrokeThicknessProperty);
        }

        #endregion

        #region CornerRadius

        
        public static void SetCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static CornerRadius GetCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(CornerRadiusProperty);
        }

        #endregion

        #region Foreground


        public static void SetForeground(DependencyObject element, Brush value)
        {
            element.SetValue(ForegroundProperty, value);
        }

        public static Brush GetForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(ForegroundProperty);
        }

        #endregion

        #region FontSize

        public static void SetFontSize(DependencyObject element, double value)
        {
            element.SetValue(FontSizeProperty, value);
        }

        public static double GetFontSize(DependencyObject element)
        {
            return (double)element.GetValue(FontSizeProperty);
        }
        

        #endregion

        #region Intro

        
        public static void SetIntro(DependencyObject element, string value)
        {
            element.SetValue(IntroProperty, value);
        }

        public static string GetIntro(DependencyObject element)
        {
            return (string)element.GetValue(IntroProperty);
        }

        #endregion

        #region ObjectView

        
        public static void SetObjectView(DependencyObject element, object value)
        {
            element.SetValue(ObjectViewProperty, value);
        }

        public static object GetObjectView(DependencyObject element)
        {
            return (object)element.GetValue(ObjectViewProperty);
        }

        #endregion

        #region Object

        

        
        public static void SetObject(DependencyObject element, object value)
        {
            element.SetValue(ObjectProperty, value);
        }

        public static object GetObject(DependencyObject element)
        {
            return (object)element.GetValue(ObjectProperty);
        }
        #endregion

        #region CommandPanel

        

        public static void SetCommandPanel(DependencyObject element, object value)
        {
            element.SetValue(CommandPanelProperty, value);
        }

        public static object GetCommandPanel(DependencyObject element)
        {
            return (object)element.GetValue(CommandPanelProperty);
        }
        #endregion
        
        #region ToolBar

        

        public static void SetToolBar(DependencyObject element, object value)
        {
            element.SetValue(ToolBarProperty, value);
        }

        public static object GetToolBar(DependencyObject element)
        {
            return (object)element.GetValue(ToolBarProperty);
        }
        
        #endregion
    }
}