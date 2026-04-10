// ----------------------------------------------------------
//            文件：ICroppedImageTarget.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 13:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    /// <summary>
    /// 表示一个支持图片裁切显示的目标。
    /// </summary>
    public interface ICroppedImageTarget
    {
        int X { get; set; }
        int Y { get; set; }

        int Ratio { get; set; }

        int ViewportHeight { get; set; }
        int ViewportWidth  { get; set; }
        int ImageHeight    { get; set; }
        int ImageWidth     { get; set; }
    }
}