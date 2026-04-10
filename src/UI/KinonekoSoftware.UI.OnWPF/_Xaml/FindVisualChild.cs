namespace KinonekoSoftware.UI
{ 
    partial class Xaml
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="recursive"></param>
        /// <typeparam name="T">泛型类型。</typeparam>
        /// <returns></returns>
        public static T FindVisualChild<T>(DependencyObject parent, bool recursive = false) where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }

                if (recursive)
                {
                    var foundChild = FindVisualChild<T>(child, recursive);

                    if (foundChild != null)
                    {
                        return foundChild;
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="depth">当前深度</param>
        /// <param name="maxDepth">最大深度</param>
        /// <typeparam name="T">泛型类型。</typeparam>
        /// <returns></returns>
        public static T FindVisualChild<T>(DependencyObject parent, int depth, int maxDepth = 8) where T : DependencyObject
        {
            var childCount = VisualTreeHelper.GetChildrenCount(parent);

            if (depth > maxDepth)
            {
                return null;
            }
            
            for (var i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T instance)
                {
                    return instance;
                }
 
                var foundChild = FindVisualChild<T>(child, depth + 1, maxDepth);

                if (foundChild != null)
                {
                    return foundChild;
                }
            }

            return null;
        }
    }
}