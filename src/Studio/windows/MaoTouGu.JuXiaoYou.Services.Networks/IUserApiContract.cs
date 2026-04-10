// ----------------------------------------------------------
//            文件：IUserApiContract.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 18:56
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    public interface IUserApiContract
    {
        Task<Result> UpdateUserAsync(User user);
        
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <returns></returns>
        Task<Result<List<User>>> GetUserListAsync();

        /// <summary>
        /// 获得指定用户。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<User>> GetUserAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="hashed"></param>
        /// <returns></returns>
        Task<Result> ChangePasswordAsync(string pwd, bool hashed);

        Task<Result> ChangeEmailAsync(string email);

        Task<Result> ChangeRoleAsync(string id, UserType type);
    }
}