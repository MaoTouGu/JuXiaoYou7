namespace KinonekoSoftware.UI
{
    partial class Xaml
    {
        static T FindVisualChild<T>(DependencyObject parent, Predicate<T> expression, int depth, int maxDepth) where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(parent);

            if (depth > maxDepth)
            {
                return null;
            }
            
            for (var i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T instance && expression(instance))
                {
                    return instance;
                }
 
                var foundChild = FindVisualChild<T>(child, expression, depth, maxDepth);

                if (foundChild != null)
                {
                    return foundChild;
                }
            }

            return null;
        }
        
        
        
        
        public static T FindVisualChild<T>(DependencyObject parent, Predicate<T> expression, int maxDepth = 8) where T : DependencyObject
        {
            return FindVisualChild<T>(parent, expression, 0, maxDepth);
        }
    }
}