// ----------------------------------------------------------
//            文件：TypographyRectangle.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 15:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Topology
{
    /// <summary>
    /// <see cref="TypographyRectangle"/> 
    /// </summary>
    public sealed class TypographyRectangle : TypographyBlock
    {
        public Int32CornerRadius CornerRadius    { get; set; }
        public string            BorderBrush     { get; set; }
        public string            Background      { get; set; }
        public Int32Thickness    BorderThickness { get; set; }
    }
}