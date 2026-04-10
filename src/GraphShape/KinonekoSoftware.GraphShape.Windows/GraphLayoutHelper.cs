using System.Windows;
using System.Windows.Controls;
using GraphShape.Controls;

namespace KinonekoSoftware.GraphShape
{
    public static class GraphLayoutHelper
    {
        public static readonly DependencyProperty LayoutControlProperty
            = DependencyProperty.RegisterAttached(
                                                  "LayoutControl",
                                                  typeof(GraphLayout),
                                                  typeof(GraphLayoutHelper),
                                                  new PropertyMetadata(default(GraphLayout)));

        public static readonly DependencyProperty LayoutMethodProperty 
            = DependencyProperty.RegisterAttached( "LayoutMethod", 
                                                  typeof(string), 
                                                  typeof(GraphLayoutHelper), 
                                                  new PropertyMetadata(default(string)));


        public static readonly DependencyProperty UseMethodProperty
            = DependencyProperty.RegisterAttached(
                                                  "ShadowUseMethod",
                                                  typeof(bool),
                                                  typeof(GraphLayoutHelper),
                                                  new PropertyMetadata(default(bool)));
        
        public static string GetLayoutMethod(DependencyObject element)
        {
            return (string)element.GetValue(LayoutMethodProperty);
        }

        
        public static void SetLayoutMethod(DependencyObject element, string value)
        {
            element.SetValue(LayoutMethodProperty, value);
        }

        public static void SetUseMethod(DependencyObject element, bool value)
        {
            if (element is MenuItem control)
            {
                control.Click += ControlOnClick;
            }
            
            element.SetValue(UseMethodProperty, value);
        }

        private static void ControlOnClick(object sender, RoutedEventArgs e)
        {
            var method = GetLayoutMethod(sender as DependencyObject);
            var layout = GetLayoutControl(sender as DependencyObject);

            if (layout is null || string.IsNullOrEmpty(method))
            {
                return;
            }

            layout.LayoutAlgorithmType = method;
        }

        public static bool GetUseMethod(DependencyObject element)
        {
            return (bool)element.GetValue(UseMethodProperty);
        }

        public static void SetLayoutControl(DependencyObject element, GraphLayout value)
        {
            element.SetValue(LayoutControlProperty, value);
        }

        public static GraphLayout GetLayoutControl(DependencyObject element)
        {
            return (GraphLayout)element.GetValue(LayoutControlProperty);
        }
    }
}