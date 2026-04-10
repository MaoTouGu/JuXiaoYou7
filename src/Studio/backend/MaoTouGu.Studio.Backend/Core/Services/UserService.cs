// ----------------------------------------------------------
//            文件：UserService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using LiteDB;
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database.Spots;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.Studio.Services
{
    public class UserService : IUserService
    {
        private readonly LiteDatabase          _database;
        private readonly ILiteCollection<User> _UserTable;

        private bool _hasUserCol;

        public UserService(IDatabaseService _Env)
        {
            _database   = _Env.IdentityDB;
            _hasUserCol = _database.CollectionExists("Users");
            _UserTable  = _database.GetCollection<User>("Users");
        }

        static bool NotUserName(string anyString)
        {
            return string.IsNullOrEmpty(anyString) ||
                   anyString.Length > 20           ||
                   anyString.Any(x => !char.IsLetterOrDigit(x));
        }

        public void Initialize()
        {
            if (_hasUserCol)
            {
                return;
            }
            const string admin = "admin";

            _UserTable.Insert(new User
            {
                Id                 = ID.Get(),
                CreatedTime        = DateTime.Now,
                UserName           = admin,
                DisplayName        = admin,
                NormalizedUserName = admin.ToUpper(),
                Type               = UserType.Super,
                HashedPwd          = User.Hash("123456"),
            });

            _hasUserCol = true;
        }
        
        public User GetUser(string id) => _UserTable.FindById(id)
                                                    .Desensitization();
        
        public List<User> GetUsers() => _UserTable.FindAll()
                                                  .ToList();
        public UserChangeSpot Update(User user)
        {
            var inside = _UserTable.FindById(user.Id);

            if (inside is null)
            {
                return null;
            }
            var spot = new UserChangeSpot
            {
                OldGravatar = inside.Gravatar,
                NewGravatar = user.Gravatar,
                OldName     = inside.DisplayName,
                NewName     = user.DisplayName,
                UserID      = user.Id,
            };
            
            inside.Gravatar    = user.Gravatar;
            inside.DisplayName = user.DisplayName;

            if (string.IsNullOrEmpty(inside.UserName))
            {
                inside = inside.Fixed(StringExtensions.RandomLetterString(4));
            }
            
            if (string.IsNullOrEmpty(inside.HashedPwd))
            {
                inside.HashedPwd = User.Hash("123456");
            }
            
            _UserTable.Update(inside);
            return spot;
        }
        
        public void ChangePassword(string id, string pwd)
        {
            if (_UserTable.FindById(id) is not {} user)
            {
                return;
            }

            user.HashedPwd = pwd;

            _database.BeginTrans();
            _UserTable.Update(user);
            _database.Commit();
        }

        public void ChangeEmail(string id, string email)
        {
            if (_UserTable.FindById(id) is not {} user)
            {
                return;
            }

            var normalizedEmail = email.ToUpper();
            var maskedEmail     = User.MaskEmail(email);

            user.Email           = email;
            user.NormalizedEmail = normalizedEmail;
            user.MaskedEmail     = maskedEmail;

            _database.BeginTrans();
            _UserTable.Update(user);
            _database.Commit();
        }

        public void ChangeRole(string id, UserType type)
        {
            if (_UserTable.FindById(id) is not {} user)
            {
                return;
            }

            user.Type = type;

            try
            {

                _database.BeginTrans();
                _UserTable.Update(user);
                _database.Commit();
            }
            catch(Exception e)
            {
                _database.Rollback();
            }
        }


        public Result SignUp(string userName, string displayName, string email, string pwd, out string id)
        {
            if (_UserTable.FindOne(x => x.NormalizedUserName == userName) is not null)
            {
                id = string.Empty;
                return Result.Failed("无法注册，账户名已经存在！");
            }

            var normalizedEmail    = email.ToUpper();
            var normalizedUserName = userName.ToUpper();


            if (_UserTable.FindOne(x => x.NormalizedEmail == normalizedEmail) is not null)
            {
                id = string.Empty;
                return Result.Failed("无法注册，邮箱已经存在！");
            }

            var user = new User
            {
                Id                 = ID.Get(),
                UserName           = userName,
                DisplayName        = displayName,
                NormalizedEmail    = normalizedEmail,
                NormalizedUserName = normalizedUserName,
                Type               = UserType.User,
                IsSoftDeleted      = false,
                MaskedEmail        = User.MaskEmail(email),
                HashedPwd          = pwd,
                CreatedTime        = DateTime.Now,
                Email              = email,
            };

            try
            {
                _database.BeginTrans();
                _UserTable.Insert(user);
                _database.Commit();

                id = user.Id;
                return Result.Success();
            }
            catch(Exception e)
            {
                _database.Rollback();
                id = string.Empty;
                return Result.Failed(e.Message);
            }
        }

        public bool TryLogin(UserRequest req, out User user2, out string msg)
        {
            var pwd       = req.HashedPassword;
            var anyString = req.UserName;

            if (!User.IsEmail(anyString) && NotUserName(anyString))
            {
                user2 = null;
                msg   = "账户格式不正确";
                return false;
            }

            if (!User.IsBase64String(pwd))
            {
                user2 = null;
                msg   = "密码格式不正确";
                return false;
            }

            var user = _UserTable.FindOne(x => x.NormalizedUserName == anyString || x.NormalizedEmail == anyString);

            if (user is null)
            {
                msg   = "账户错误";
                user2 = null;
                return false;
            }

            if (user.IsSoftDeleted)
            {
                msg   = "账户已被移除。";
                user2 = null;
                return false;
            }

            if (user.HashedPwd != pwd)
            {
                msg   = "密码错误。";
                user2 = null;
                return false;
            }

            var token = ID.Get();

            user2 = new User
            {
                Id          = user.Id,
                CreatedTime = user.CreatedTime,
                Gravatar    = user.Gravatar,
                Type        = user.Type,
                DisplayName = user.DisplayName,
                Token       = token,
                MaskedEmail = user.MaskedEmail,
            };


            msg = "登陆成功";
            return true;
        }

        public bool IsAdmin(string id, bool onlyTransaction = false)
        {
            if (_UserTable.FindById(id) is not {} user)
            {
                return false;
            }

            if (onlyTransaction)
            {
                return user.Type == UserType.Admin;
            }

            return user.Type == UserType.Admin || user.Type == UserType.Super;
        }

        public bool IsSuperAdmin(string id) => _UserTable.FindById(id) is { Type: UserType.Super };
    }
}