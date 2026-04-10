// ----------------------------------------------------------
//            文件：UniqueReference.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:43
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// <see cref="UniqueReference"/> 表示一种唯一引用。
    /// </summary>
    /// <remarks>
    /// 设定只有一种引用。
    /// </remarks>
    public sealed class UniqueReference : DatabaseObject
    {
        public string DocumentID { get; init; }
        public string TopClass   { get; init; }
        public string SubClass   { get; init; }
    }
}