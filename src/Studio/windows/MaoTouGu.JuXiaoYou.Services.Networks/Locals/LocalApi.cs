// ----------------------------------------------------------
//            文件：LocalApi.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月25日 23:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;
using LiteDB;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    public partial class LocalApi : Disposable, IWebApi, IDataApiContract, IUserApiContract, IResourceLockApiContract
    {
        public LocalApi(string url)
        {
            Url       = url;
            SafetyUrl = url;
        }



        public Task<Result<User>> LoginAsync(string name, string pwd, bool hashed)
        {
            return Task.Run(() =>
                            {
                                var r = JSON.FromFile<User>(LocalSettingFileName, CreateUser);

                                User = r;

                                return Result<User>.Success(r);
                            });
        }

        public Task<Result> LogoutAsync()
        {
            return Task.FromResult(Result.Success());
        }


        public User   User      { get; private set; }
        public string Url       { get; }
        public string SafetyUrl { get; }

        public string UserID   => User?.Id;
        public bool   IsOnline => false;
    }
}