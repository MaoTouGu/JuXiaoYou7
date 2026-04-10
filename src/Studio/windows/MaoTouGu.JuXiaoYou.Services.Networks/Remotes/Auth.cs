// ----------------------------------------------------------
//            文件：Auth.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月24日 11:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Net.Http;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class RemoteApi
    {
        private const string Auth_Login  = "/api/User/login";
        private const string Auth_Logout = "/api/User/logout";
        //-------------------------------------------------------------
        //
        //                          Auth
        //
        //-------------------------------------------------------------
        public async Task<Result<User>> LoginAsync(string name, string pwd, bool hashed)
        {
            var payload = JSON.Serialize<UserRequest>(new UserRequest { UserName = name, Password = pwd, Hashed = hashed });
            var r       = await PostJsonAndReturnUser(Auth_Login, payload);

            if (r.IsFinished)
            {
                User = r.Value;
            }

            return r;
        }

        public async Task<Result> LogoutAsync()
        {
            var r = await GetAndReturnResult(Auth_Logout);

            if (r.IsFinished)
            {
                User = null;
            }

            return r;
        }
    }
}