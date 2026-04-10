// ----------------------------------------------------------
//            文件：IdentityOperation.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.Studio.Database.Operations
{
    public class IdentityOperation : DatabaseObject
    {
        public string   UserId   { get; init; }
        public UserType Type     { get; init; }
        public string   Address  { get; init; }
        public bool     Feedback { get; init; }
        public DateTime Utc      { get; init; }
    }
}