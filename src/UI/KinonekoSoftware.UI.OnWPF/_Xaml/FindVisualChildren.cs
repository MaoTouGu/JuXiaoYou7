// ----------------------------------------------------------
//            文件：FindVisualChildren.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月13日 02:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace KinonekoSoftware.UI
{
    partial class Xaml
    {


        /// <summary>
        /// 利用VisualTreeHelper寻找对象的子级对象
        /// </summary>
        /// <typeparam name="T">泛型类型。</typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                var TList = new List<T>(8);

                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);

                    if (child is T instance)
                    {
                        TList.Add(instance);
                        var childOfChildren = FindVisualChildren<T>(child);

                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                    else
                    {
                        var childOfChildren = FindVisualChildren<T>(child);

                        if (childOfChildren != null)
                        {
                            TList.AddRange(childOfChildren);
                        }
                    }
                }

                return TList;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 利用VisualTreeHelper寻找对象的子级对象
        /// </summary>
        /// <typeparam name="TBase">泛型类型。</typeparam>
        /// <typeparam name="T1">泛型类型。</typeparam>
        /// <typeparam name="T2">泛型类型。</typeparam>
        /// <param name="obj"></param>
        /// <param name="maxDepth">最大层数</param>
        /// <returns></returns>
        public static List<TBase> FindVisualChildren<TBase, T1, T2>(DependencyObject obj, int maxDepth = 32) where TBase : DependencyObject
                                                                                                             where T1 : TBase
                                                                                                             where T2 : TBase
        {
            try
            {
                var list = new List<TBase>(8);

                FindVisualChildren<TBase, T1, T2>(list, obj, 0, maxDepth);

                return list;
            }
            catch
            {
                return null;
            }
        }

        private static void FindVisualChildren<TBase, T1, T2>(List<TBase> list, DependencyObject obj, int depth, int maxDepth) where TBase : DependencyObject
                                                                                                                               where T1 : TBase
                                                                                                                               where T2 : TBase
        {
            if (depth > maxDepth)
            {
                return;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child is T1 or T2)
                {
                    list.Add(obj as TBase);
                }

                FindVisualChildren<TBase, T1, T2>(list, child, depth + 1, maxDepth);
            }
        }
    }
}