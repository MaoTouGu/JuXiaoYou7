// ----------------------------------------------------------
//            文件：StaticProperties.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 16:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------=

namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public static partial class ImageSystem
    {
        /// <summary>
        /// 为了避免null或者string.empty时Source属性不更改，而设置的默认属性。
        /// </summary>
        public const string DefaultImageUri = "______";

        /// <summary>
        /// 默认路径。
        /// </summary>
        public const string DefaultDirPath = "Images";

        /// <summary>
        /// Fallback的设置。
        /// </summary>
        public static BitmapImage Image { get; set; }

        /// <summary>
        /// Fallback的设置。
        /// </summary>
        public static BitmapImage Gravatar { get; set; }

        /// <summary>
        /// Fallback的设置。
        /// </summary>
        public static BitmapImage Icon { get; set; }

        /// <summary>
        /// 根目录，即获取图片的目标目录，例如: C:\Users\Admin\Documents\MaoTouGu\JuXiaoYou\Caches
        /// </summary>
        public static string RootPath { get; set; }
    }
}