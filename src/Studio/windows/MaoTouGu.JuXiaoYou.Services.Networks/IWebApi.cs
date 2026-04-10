using System.IO;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    public interface IWebApi :  IUserApiContract, IDataApiContract, IResourceLockApiContract
    {
        Task<Result> SignUpAsync(string displayName, string email, string name, string pwd, bool hashed);

        Task<Result<User>> LoginAsync(string name, string pwd, bool hashed);

        Task<Result> LogoutAsync();

        Task<Result> UploadFile(string id, Stream stream);

        Task<Result> UploadEmoji(string id, Stream stream);

        Task<Result> UploadImage(string id, Stream stream);

        Task<Result> UploadGravatar(string id, Stream stream);

        Task<Result> UploadIcon(string id, Stream stream);

        User   User      { get; }
        string UserID    { get; }
        string Url       { get; }
        string SafetyUrl { get; }
    }
}