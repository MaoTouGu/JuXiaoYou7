// ----------------------------------------------------------
//            文件：User.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月24日 12:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class RemoteApi
    {

        private const string User_Add         = "/api/User/signUp";
        private const string User_Get         = "/api/User/get";
        private const string User_ChangePwd   = "/api/User/changePwd";
        private const string User_ChangeEmail = "/api/User/changeEmail";
        private const string User_ChangeRole  = "/api/User/changeRole";
        private const string User_Update      = "/api/User/update";
        private const string User_All         = "/api/User/all";

        //-------------------------------------------------------------
        //
        //                          User
        //
        //-------------------------------------------------------------


        public async Task<Result> UpdateUserAsync(User user)
        {
            var payload = JSON.Serialize<User>(user);
            return await PostJsonAndReturnResult(User_Update, payload);
        }

        public async Task<Result<List<User>>> GetUserListAsync() => await GetAndReturnJson(User_All);

        public async Task<Result<User>> GetUserAsync(string id) => await GetAndReturnUser($"{User_Get}?id={id}");

        public async Task<Result> SignUpAsync(string displayName, string email, string name, string pwd, bool hashed)
        {
            var payload = JSON.Serialize<UserRequest>(new UserRequest
            {
                Email       = email,
                DisplayName = displayName,
                UserName    = name,
                Password    = pwd,
                Hashed      = hashed,
            });
            return await PostJsonAndReturnResult(User_Add, payload);
        }

        public async Task<Result> ChangePasswordAsync(string pwd, bool hashed)
        {
            var payload = JSON.Serialize<UserRequest>(new UserRequest { Password = pwd, Hashed = hashed });
            return await PostJsonAndReturnResult(User_ChangePwd, payload);
        }

        public async Task<Result> ChangeEmailAsync(string email)
        {
            return await PostJsonAndReturnResult(User_ChangeEmail, $"\"{email}\"");
        }
        public async Task<Result> ChangeRoleAsync(string id, UserType type)
        {
            return await GetAndReturnResult($"{User_ChangeRole}?id={id}&type={type}");
        }
    }
}