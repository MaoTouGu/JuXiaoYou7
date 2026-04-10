// ----------------------------------------------------------
//            文件：IImageWorker.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月11日 02:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    /// <summary>
    /// <see cref="IImageWorker"/>>接口可以让用户自定义的控件接入橘小柚的图片系统的显示逻辑。
    /// </summary>
    public interface IImageWorker
    {
        /// <summary>
        /// 设置图片源。
        /// </summary>
        /// <param name="bi">给定的图片源，一定为非空。</param>
        void SetImage(BitmapImage bi);
    }
}