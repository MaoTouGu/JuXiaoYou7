// ----------------------------------------------------------
//            文件：UserController.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 13:46
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using System.Security.Claims;
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Operations;
using MaoTouGu.Studio.Database.Spots;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.Studio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UserController(IUserService _Service,
                                       IHubContext<PushingHub> _Hub,
                                       ILogger<UserController> _Logger) : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest req)
        {
            // 简单示例：固定账号密码
            if (!_Service.TryLogin(req, out var user, out var msg))
            {
                await Channels.Login.Writer.WriteAsync(new IdentityOperation
                {
                    Id       = ID.Get(),
                    Address  = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Feedback = false,
                    UserId   = req.UserName,
                    Type     = UserType.Guest,
                    Utc      = DateTime.UtcNow,
                });

                return Unauthorized(Result<User>.Failed(msg));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim(ClaimTypes.Sid, user.Token),
                new Claim(ClaimTypes.Expired, "false"),
                new Claim(ClaimTypes.Expiration, DateTimeOffset.UtcNow.AddHours(2).ToString("o")),
                new Claim("LoginIP", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"),
            };

            var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await Channels.Login.Writer.WriteAsync(new IdentityOperation
            {
                Id       = ID.Get(),
                Address  = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Feedback = true,
                UserId   = user.Id,
                Type     = user.Type,
                Utc      = DateTime.UtcNow,
            });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Json(Result<User>.Success(user));
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Json(Result.Success("退出登录"));
        }

        /// <summary>
        /// 获得所有用户
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("all")]
        public IActionResult All()
        {
            //
            // 脱敏。
            var result = _Service.GetUsers()
                                 .Select(x => x.Desensitization())
                                 .ToList();

            return Json(Result<List<User>>.Success(result));
        }

        /// <summary>
        /// 获得指定用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get")]
        public IActionResult Get(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return Json(Result<User>.Failed("id为空"));
            }

            return Json(Result<User>.Success(_Service.GetUser(id)));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost("signUp")]
        public IActionResult SignUp([FromBody] UserRequest credential)
        {
            var userName = credential.UserName;
            var pwd      = credential.HashedPassword;

            if (string.IsNullOrEmpty(userName))
            {
                return Json(Result.Failed("账户名不能为空"));
            }

            if (string.IsNullOrEmpty(credential.DisplayName))
            {
                return Json(Result.Failed("用户昵称不能为空"));
            }

            if (userName.Equals(credential.DisplayName, StringComparison.CurrentCultureIgnoreCase))
            {
                return Json(Result.Failed("用户昵称不能与账户名相等"));
            }

            if (string.IsNullOrEmpty(credential.Email))
            {
                return Json(Result.Failed("邮箱不能为空"));
            }

            if (string.IsNullOrEmpty(credential.Password))
            {
                return Json(Result.Failed("密码不能为空"));
            }

            var r = _Service.SignUp(userName, credential.DisplayName, credential.Email, pwd, out var id);

            if (r.IsFinished)
            {
                _Hub.Clients
                    .All
                    .SendAsync(nameof(ISpotRecipient.WhenDataChanged), new UserSpot
                     {
                         Operation = DataOperation.Added,
                         UserID    = id,
                     });
            }

            _Logger.LogInformation($"用户 = {credential.DisplayName}注册成功, Id = {id}");
            return Json(r);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("update")]
        public IActionResult Update([FromBody] User user)
        {
            if (user is null)
            {
                return Json(Result.Failed("参数为空"));
            }

            var spot = _Service.Update(user);

            _Hub.Clients
                .All
                .SendAsync(nameof(ISpotRecipient.WhenDataChanged), spot);

            return Json(Result.Success());
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("changePwd")]
        public async Task<IActionResult> ChangePassword([FromBody] UserRequest credential)
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
            {
                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = "空",
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = credential.HashedPassword,
                    Operation  = "修改密码",
                });

                return Json(Result.Failed("无法获取用户ID"));
            }

            if (string.IsNullOrEmpty(credential.Password))
            {
                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = claim.Value,
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = credential.HashedPassword,
                    Operation  = "修改密码",
                });

                return Json(Result.Failed("密码为空"));
            }

            _Service.ChangePassword(claim.Value, credential.HashedPassword);

            await Channels.Security.Writer.WriteAsync(new SecurityOperation
            {
                Id         = ID.Get(),
                OperatorID = claim.Value,
                Feedback   = true,
                Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Value      = credential.HashedPassword,
                Operation  = "修改密码",
            });
            return Json(Result.Success());
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("changeRole")]
        public async Task<IActionResult> ChangeRole(string id, UserType type)
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
            {
                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = "空",
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = type.ToString(),
                    Operation  = "修改用户权限",
                });

                return Json(Result.Failed("无法获取用户ID"));
            }

            if (string.IsNullOrEmpty(id))
            {
                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = claim.Value,
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = type.ToString(),
                    Operation  = "修改用户权限",
                });

                return Json(Result.Failed("目标id为空"));
            }

            if (!_Service.IsAdmin(claim.Value))
            {

                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = claim.Value,
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = type.ToString(),
                    Operation  = "修改用户身份，没有权限",
                });
                return Json(Result.Failed("没有权限"));
            }

            _Service.ChangeRole(id, type);

            await Channels.Security.Writer.WriteAsync(new SecurityOperation
            {
                Id         = ID.Get(),
                OperatorID = claim.Value,
                Feedback   = true,
                Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Value      = type.ToString(),
                Operation  = "修改用户身份",
            });
            return Json(Result.Success());
        }

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromBody] string email)
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
            {
                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = "空",
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = email,
                    Operation  = "修改邮箱",
                });

                return Json(Result.Failed("无法获取用户ID"));
            }

            if (string.IsNullOrEmpty(email))
            {
                await Channels.Security.Writer.WriteAsync(new SecurityOperation
                {
                    Id         = ID.Get(),
                    OperatorID = claim.Value,
                    Feedback   = false,
                    Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Value      = email,
                    Operation  = "修改邮箱",
                });

                return Json(Result.Failed("邮箱为空"));
            }

            _Service.ChangeEmail(claim.Value, email);

            await Channels.Security.Writer.WriteAsync(new SecurityOperation
            {
                Id         = ID.Get(),
                OperatorID = claim.Value,
                Feedback   = true,
                Address    = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Value      = email,
                Operation  = "修改邮箱",
            });
            return Json(Result.Success());
        }
    }
}