// ----------------------------------------------------------
//            文件：ThumbnailLevel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 16:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public enum ThumbnailLevel
    {
        /// <summary>
        /// 不缩放
        /// </summary>
        None,
        
        /// <summary>
        /// 360p的缩放比例
        /// </summary>
        Of360p,
        
        /// <summary>
        /// 720p
        /// </summary>
        Of720p,
        
        /// <summary>
        /// 1080p
        /// </summary>
        Of1080p,
    }
}