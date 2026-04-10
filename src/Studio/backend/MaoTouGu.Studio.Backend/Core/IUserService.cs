// ----------------------------------------------------------
//            文件：IUserService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 15:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database.Spots;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Core
{
    public interface IUserService
    {
        void Initialize();
        
        bool TryLogin(UserRequest req, out User user, out string msg);

        bool IsAdmin(string id, bool onlyTransaction = false);
        bool IsSuperAdmin(string id);
        
        Result SignUp(string userName, string displayName, string email, string pwd, out string id);
        
        User GetUser(string id);
        List<User> GetUsers();
        
        UserChangeSpot Update(User user);
        
        void ChangePassword(string id, string pwd);
        void ChangeEmail(string id, string email);
        void ChangeRole(string id, UserType type);
    }
}