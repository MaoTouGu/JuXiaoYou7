using System.Globalization;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;

namespace KinonekoSoftware.UI
{
    public static partial class Xaml
    {

        public static T FindParent<T>(Visual dp, int maxDepth = 32) where T : Visual
        {
            var parent = dp.GetVisualParent();
            var depth  = 0;

            while (parent is not T && depth < maxDepth)
            {
                parent = parent?.GetVisualParent();
                depth++;
            }


            return parent as T;
        }


        public static object FindResource(string key)
        {

            if (Application.Current.TryFindResource(key, out var r))
            {
                return r;
            }

            return null;
        }
    }
}