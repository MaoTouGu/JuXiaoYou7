// ----------------------------------------------------------
//            文件：SecurityOperation.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Operations
{
    public class SecurityOperation
    {
        public required string Id         { get; init; }
        public required string OperatorID { get; init; }
        public required bool   Feedback   { get; init; }
        public required string Address    { get; init; }
        public required string Value      { get; init; }
        public required string Operation  { get; init; }
    }
}