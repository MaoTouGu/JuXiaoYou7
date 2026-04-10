// ----------------------------------------------------------
//            文件：IUserService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 09:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Identity
{
    public interface IUserService : ILifetime
    {
        Task Handle(Spot dataEvent);
        
        IReadOnlyList<User> Collection { get; }

        IReadOnlyDictionary<string, User> Dictionary { get; }
    }
}