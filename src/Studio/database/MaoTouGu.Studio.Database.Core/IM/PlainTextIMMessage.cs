// ----------------------------------------------------------
//            文件：PlainTextIMMessage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 19:18
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.IM
{
    public sealed class PlainTextIMMessage : MSG
    {
        public string Text { get; init; }
    }
}