// ----------------------------------------------------------
//            文件：MonikerSubTreeIcon.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月07日 16:18
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows.Markup;
using MaoTouGu.Shells.Converters;

namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public class MonikerSubTreeIcon : OneWayConverter, IValueConverter
    {

        private const string G_Folder = "F1 M16,16z M0,0z M8.10584,4.34613L8.25344,4.5 8.46667,4.5 13,4.5C13.8284,4.5,14.5,5.17157,14.5,6L14.5,12.1333C14.5,12.9529,13.932,13.5,13.3667,13.5L2.63333,13.5C2.06804,13.5,1.5,12.9529,1.5,12.1333L1.5,3.86667C1.5,3.04707,2.06804,2.5,2.63333,2.5L6.1217,2.5C6.25792,2.5,6.38824,2.55557,6.48253,2.65387L8.10584,4.34613z";
        private const string G_Star   = "F1 M24,24z M0,0z M12,2L12,2 15.09,8.26 22,9.27 17,14.14 18.18,21.02 12,17.77 5.82,21.02 7,14.14 2,9.27 8.91,8.26 12,2z";
        private const string G_Trash  = "F0 M16,16z M0,0z M15,8C15,11.866 11.866,15 8,15 4.13401,15 1,11.866 1,8 1,4.13401 4.13401,1 8,1 11.866,1 15,4.13401 15,8z M14,8C14,11.3137 11.3137,14 8,14 6.52316,14 5.17094,13.4664 4.1256,12.5815L12.5815,4.1256C13.4664,5.17094,14,6.52316,14,8z M3.41849,11.8744L11.8744,3.41849C10.8291,2.53359 9.47686,2 8,2 4.68629,2 2,4.68629 2,8 2,9.47686 2.53359,10.8291 3.41849,11.8744z";

        private static readonly Geometry Folder = Geometry.Parse(G_Folder);
        private static readonly Geometry Star   = Geometry.Parse(G_Star);
        private static readonly Geometry Trash  = Geometry.Parse(G_Trash);

        public static MonikerSubTreeIcon Instance { get; } = new MonikerSubTreeIcon();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // if (value is MonikerSubTreeIntent intent)
            // {
            //     return intent switch
            //     {
            //         MonikerSubTreeIntent.Deleted  => Trash,
            //         MonikerSubTreeIntent.Recently => Folder,
            //         MonikerSubTreeIntent.All      => Folder,
            //         MonikerSubTreeIntent.Favorite => Star,
            //         _                             => Folder,
            //     };
            // }

            return null;
        }
    }
}