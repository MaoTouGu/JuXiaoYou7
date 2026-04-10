// ----------------------------------------------------------
//            文件：User.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Security.Cryptography;
using System.Text;

namespace MaoTouGu.Studio.Database.Identity
{
    public partial class User : DatabaseObject, IGravatarTarget
    {
        private const string EmailValidatePattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$";
        private const string FindValidatePattern  = @"^(?'prefix'[a-zA-Z0-9_.+-]+)(?'suffix'@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+)$";
        
        private bool     _isSoftDeleted;
        private string   _displayName;
        private UserType _type;
        private string   _gravatar;

        public string GetGravatar() => Gravatar;
        public void SetGravatar(string value)
        {
            Gravatar = value;
        }
        
        public bool IsAdmin() => Type is UserType.Admin or UserType.Super;
        
        public static bool IsBase64String(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // 长度必须是 4 的倍数
            if (input.Length % 4 != 0)
                return false;

            // 只允许 Base64 字符
            if (!Regex.IsMatch(input, @"^[A-Za-z0-9+/]*={0,2}$"))
                return false;

            try
            {
                var bytes   = Convert.FromBase64String(input);
                var encoded = Convert.ToBase64String(bytes);
                return encoded == input;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 判断是否为邮箱。
        /// </summary>
        /// <param name="email">要判断的字符串。</param>
        /// <returns>返回结果</returns>
        public static bool IsEmail(string email)
        {
            return IsEmail().IsMatch(email);
        }
        
        /// <summary>
        /// 邮箱打码。
        /// </summary>
        /// <param name="email">要打码的字符串。</param>
        /// <returns>返回结果</returns>
        public static string MaskEmail(string email)
        {
            var match  = GetMailParts().Match(email);
            var prefix = match.Groups["prefix"].Value;
            var suffix = match.Groups["suffix"].Value;

            var mask = prefix.Length switch
            {
                1 => $"{prefix}****{suffix}",
                2 => $"{prefix[0]}***{suffix}",
                _ => $"{prefix[0]}****{prefix[^1]}{suffix}",
            };

            return mask;
        }
        
        /// <summary>
        /// 哈希化
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string Hash(string pwd)
        {
            var buffer       = Encoding.UTF8.GetBytes(pwd);
            var salted       = BitConverter.GetBytes((int)0x1314157);
            var saltedBuffer = new byte[buffer.Length + 4];

            Array.Copy(salted, 0, saltedBuffer, 0, salted.Length);
            Array.Copy(buffer, 0, saltedBuffer, 4, buffer.Length);

            var hashed = MD5.HashData(saltedBuffer);

            return Convert.ToBase64String(hashed);
        }

        /// <summary>
        /// 修复
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User Fixed(string userName)
        {
            return new User
            {
                Id                 = Id,
                CreatedTime        = CreatedTime,
                IsSoftDeleted      = IsSoftDeleted,
                DisplayName        = DisplayName,
                Gravatar           = Gravatar,
                MaskedEmail        = MaskedEmail,
                Token              = Token,
                Type               = Type,
                UserName           = userName,
                HashedPwd          = HashedPwd,
                NormalizedEmail    = NormalizedEmail,
                NormalizedUserName = NormalizedUserName,
                Email              = Email,
            };
        }

        /// <summary>
        /// 脱敏处理。
        /// </summary>
        /// <returns>返回脱敏之后的数据。</returns>
        /// <remarks>在返回客户端时执行此操作，届时客户端值能够获得公共数据。</remarks>
        public User Desensitization()
        {
            return new User
            {
                Id            = Id,
                CreatedTime   = CreatedTime,
                IsSoftDeleted = IsSoftDeleted,
                DisplayName   = DisplayName,
                Gravatar      = Gravatar,
                MaskedEmail   = MaskedEmail,
                Token         = Token,
                Type          = Type,
            };
        }
        
        public DateTime CreatedTime { get; init; }
        
        /// <summary>
        /// 正常状态的UserName。
        /// </summary>
        public string UserName { get; init; }

        /// <summary>
        /// 全大写的UserName。
        /// </summary>
        public string NormalizedUserName { get; init; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 全大写的邮箱。
        /// </summary>
        public string NormalizedEmail { get; set; }
        
        /// <summary>
        /// 打码的邮箱。
        /// </summary>
        public string MaskedEmail { get; set; }
        
        
        [BsonIgnore]
        public string Token { get; init; }
        
        /// <summary>
        /// 加密的密码
        /// </summary>
        public string HashedPwd { get; set; }
        
        
        /// <summary>
        /// 已经删除。
        /// </summary>

        /// <summary>
        /// 获取或设置 <see cref="IsSoftDeleted"/> 属性。
        /// </summary>
        public bool IsSoftDeleted
        {
            get => _isSoftDeleted;
            set => SetValue(ref _isSoftDeleted, value);
        }
        
        /// <summary>
        /// 用户名字
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => SetValue(ref _displayName, value);
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType Type
        {
            get => _type;
            set => SetValue(ref _type, value);
        }
        
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Gravatar
        {
            get => _gravatar;
            set => SetValue(ref _gravatar, value);
        }

        
        [GeneratedRegex(EmailValidatePattern)]
        private static partial Regex IsEmail();
        
        [GeneratedRegex(FindValidatePattern)]
        private static partial Regex GetMailParts();
    }
}