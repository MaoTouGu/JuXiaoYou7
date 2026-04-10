// ----------------------------------------------------------
//            文件：User.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月24日 12:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using System.IO;
using MaoTouGu.Shells;
using MaoTouGu.Shells.AppConfigs;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class LocalApi
    {
        //-------------------------------------------------------------
        //
        //                          User
        //
        //-------------------------------------------------------------

        static string LocalSettingFileName
        {
            get
            {
                var dir      = Ioc.Get<IAppConfig>().DirOfSettings;
                var fileName = Path.Combine(dir, "JuXiaoYou-V7-Local.json");
                return fileName;
            }
        }

        static User CreateUser() => new User
        {
            Id          = ID.Get(),
            DisplayName = "本地用户",
            UserName    = "Local",
        };

        public Task<Result> UpdateUserAsync(User user)
        {
            return Task.Run(() =>
                            {
                                try
                                {
                                    JSON.ToFile(LocalSettingFileName, user);
                                    return Result.Success();
                                }
                                catch(Exception e)
                                {
                                    return Result.Failed(e.Message);
                                }
                            });
        }

        public Task<Result<List<User>>> GetUserListAsync()
        {
            return Task.Run(() =>
                            {
                                try
                                {
                                    var usr = JSON.FromFile<User>(LocalSettingFileName, CreateUser);
                                    
                                    var list = new List<User> { usr };
                                    return Result<List<User>>.Success(list);
                                }
                                catch(Exception e)
                                {
                                    return Result<List<User>>.Failed(e.Message);
                                }
                            });
        }

        public Task<Result<User>> GetUserAsync(string id)
        {
            return Task.Run(() =>
                            {
                                try
                                {
                                    var usr = JSON.FromFile<User>(LocalSettingFileName, CreateUser);
                                    return Result<User>.Success(usr);
                                }
                                catch(Exception e)
                                {
                                    return Result<User>.Failed(e.Message);
                                }
                            });
        }

        public Task<Result> SignUpAsync(string displayName, string email, string name, string pwd, bool hashed)
        {
            return Task.FromResult(Result.Success());
        }

        public Task<Result> ChangePasswordAsync(string pwd, bool hashed)
        {
            return Task.FromResult(Result.Success());
        }

        public Task<Result> ChangeEmailAsync(string email)
        {
            return Task.FromResult(Result.Success());
        }
        
        public Task<Result> ChangeRoleAsync(string id, UserType type)
        {
            return Task.FromResult(Result.Success());
        }
    }
}