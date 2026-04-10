// ----------------------------------------------------------
//            文件：DataService.Local.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 15:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService
    {
        private static readonly Lazy<IUserService> _lazyUsrServiceValue = new Lazy<IUserService>(Ioc.SafeGet<IUserService>);

        /// <summary>
        /// 
        /// </summary>
        protected internal IUserService UserService => _lazyUsrServiceValue.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected internal User GetUser(string userID) => UserService.Dictionary.SafetyGet(userID);

        /// <summary>
        /// 获得所有的数据。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsonDocument> GetDocuments() => DbSet.FindAll();
    }
}