// ----------------------------------------------------------
//            文件：EdgeReference.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using QuikGraph;

namespace MaoTouGu.Studio.Database.References
{
    public class EdgeReference : IEdge<string>
    {
        public string Source { get; init; }
        public string Target { get; init; }
    }
}