// ----------------------------------------------------------
//            文件：UserType.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Identity
{
    public enum UserType
    {
        /// <summary>
        /// 超级管理员，只有一个
        /// </summary>
        Super,

        /// <summary>
        /// 管理员，多个
        /// </summary>
        Admin,

        /// <summary>
        /// 用户，多个
        /// </summary>
        User,

        /// <summary>
        /// 游客，待定
        /// </summary>
        Guest,
    }
}