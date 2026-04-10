namespace KinonekoSoftware.UI
{
    public static partial class Xaml
    {
        
        
        /// <summary>
        /// 获取指定元素相对于父元素的位置。
        /// </summary>
        /// <param name="parent">父元素</param>
        /// <param name="target">目标元素</param>
        /// <returns>返回指定元素相对于父元素的位置。</returns>
        public static Rect GetPosition(Visual parent, FrameworkElement target)
        {
            var pos = target.TransformToAncestor(parent).Transform(Zero);
            return new Rect(pos, new Size(target.ActualWidth, target.ActualHeight));
        }

        /// <summary>
        /// 获取指定元素相对于父元素的位置。
        /// </summary>
        /// <param name="parent">父元素</param>
        /// <param name="target">目标元素</param>
        /// <param name="offset">偏移量</param>
        /// <returns>返回指定元素相对于父元素的位置。</returns>
        public static Rect GetPosition(Visual parent, FrameworkElement target, Point offset)
        {
            var pos = target.TransformToAncestor(parent).Transform(offset);
            return new Rect(pos, new Size(target.ActualWidth, target.ActualHeight));
        }

        
        /// <summary>
        /// 查找指定元素的视觉父级。
        /// </summary>
        /// <param name="dp">指定要查找视觉父级的元素，要求不为空。</param>
        /// <param name="maxDepth">最高的深度。</param>
        /// <typeparam name="T">泛型类型。</typeparam>
        /// <returns>返回视觉父级，可能为空。</returns>
        public static T FindVisualParent<T>(DependencyObject dp, int maxDepth = 32) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dp);
            var depth  = 0;

            while (parent is not null && parent is not T && depth < maxDepth)
            {
                parent = VisualTreeHelper.GetParent(parent);
                depth++;
            }


            return parent as T;
        }
        
        
        public static T FindLogicalParent<T>(DependencyObject dp, int maxDepth = 32) where T : DependencyObject
        {
            var parent = LogicalTreeHelper.GetParent(dp);
            var depth  = 0;

            while (parent is not null && parent is not T && depth < maxDepth)
            {
                parent = LogicalTreeHelper.GetParent(parent);
                depth++;
            }


            return parent as T;
        }
        
        /// <summary>
        /// 查找指定元素的视觉父级。
        /// </summary>
        /// <param name="dp">指定要查找视觉父级的元素，要求不为空。</param>
        /// <param name="expression">判断此元素是否复合条件。</param>
        /// <param name="maxDepth">最高的深度。</param>
        /// <returns>返回视觉父级，可能为空。</returns>
        public static DependencyObject FindVisualParent(DependencyObject dp, Predicate<DependencyObject> expression, int maxDepth = 32)
        {
            var parent = VisualTreeHelper.GetParent(dp);
            var depth  = 0;

            if (expression is null)
            {
                return null;
            }
            
            while (parent is not null && depth < maxDepth)
            {
                if (expression(parent))
                {
                    break;
                }
                
                parent = VisualTreeHelper.GetParent(parent);
                depth++;
            }


            return parent;
        }
        
        

        /// <summary>
        /// 获得该元素及其子树的所有元素。
        /// </summary>
        /// <param name="obj">要获取视觉子树的元素，要求不为空。</param>
        /// <returns>返回一个列表。</returns>
        public static List<DependencyObject> GetSubVisualTree(DependencyObject obj)
        {
            var              pending = new Queue<DependencyObject>();
            var              list    = new List<DependencyObject>(32);
            DependencyObject current;
            DependencyObject item;

            pending.Enqueue(obj);

            while (pending.Count > 0 && list.Count < 32)
            {
                //
                //
                current = pending.Dequeue();

                //
                //
                var count = VisualTreeHelper.GetChildrenCount(current);

                //
                //
                for (var i = 0; i < count; i++)
                {
                    item = VisualTreeHelper.GetChild(current, i);

                    //
                    //
                    pending.Enqueue(item);
                    list.Add(item);
                }
            }

            return list;
        }
    }
}