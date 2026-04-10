// ----------------------------------------------------------
//            文件：UserRequest.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.Studio.Core
{
    public readonly record struct UserRequest
    {
        public string DisplayName { get; init; }
        public string Email       { get; init; }

        public string UserName { get; init; }
        public string Password { get; init; }
        public bool   Hashed   { get; init; }

        public string HashedPassword => Hashed ? Password : User.Hash(Password);
    }
}