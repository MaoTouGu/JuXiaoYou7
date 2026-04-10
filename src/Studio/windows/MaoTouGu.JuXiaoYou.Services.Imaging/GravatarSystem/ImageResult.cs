// ----------------------------------------------------------
//            文件：ImageResult.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 15:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Imaging
{
    public class ImageResult
    {
        public string Id           { get; init; }
        public byte[] Buffer       { get; init; }
        public string Ext          { get; init; }
        public int    OriginHeight { get; init; }
        public int    OriginWidth  { get; init; }
        public int    Width        { get; init; }
        public int    Height       { get; init; }
        public object Feedback     { get; init; }
    }
}