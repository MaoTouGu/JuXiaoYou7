// ----------------------------------------------------------
//            文件：Reference.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// <see cref="Reference"/> 表示一种引用关系。
    /// </summary>
    public class Reference : Nameable
    {
        public string DocumentID { get; init; }
    }
}