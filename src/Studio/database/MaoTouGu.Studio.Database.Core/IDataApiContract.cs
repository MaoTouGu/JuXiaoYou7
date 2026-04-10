// ----------------------------------------------------------
//            文件：IDataApiContract.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 22:35
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database
{
    public interface IDataApiContract
    {
        Task<Result<IEnumerable<BsonDocument>>> GetCollectionAsync(string dbName, string colName);

        Task<Result<IEnumerable<BsonDocument>>> QueryAsync(string dbName, string query);
        
        Task<Result> AddAsync(string payload, string dbName, string colName);

        Task<Result<string>> GetAsync(string dbName, string colName, string id);

        Task<Result> UpdateAsync(string payload, string dbName, string colName);

        Task<Result> RemoveAsync(string dbName, string colName, string id);
        
        bool IsOnline { get; }
    }
}