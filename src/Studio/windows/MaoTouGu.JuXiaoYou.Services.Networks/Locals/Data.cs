// ----------------------------------------------------------
//            文件：Data.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月24日 12:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Net.Http;
using LiteDB;

namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class LocalApi
    {
        
        private const string Data_Add    = "/api/Data/add";
        private const string Data_Get    = "/api/Data/get";
        private const string Data_Update = "/api/Data/update";
        private const string Data_Remove = "/api/Data/remove";
        private const string Data_All    = "/api/Data/table";
        private const string Data_Query    = "/api/Data/query";
        //-------------------------------------------------------------
        //
        //                          Data
        //
        //-------------------------------------------------------------

        //$"{Url}?dbName={dbName}&colName={colName}"
        public Task<Result<IEnumerable<BsonDocument>>> GetCollectionAsync(string dbName, string colName)
        {
            var i = Array.Empty<BsonDocument>();
            var r = Result<IEnumerable<BsonDocument>>.Success(i);
            return Task.FromResult<Result<IEnumerable<BsonDocument>>>(r);
        }    
        
        public Task<Result<IEnumerable<BsonDocument>>> QueryAsync(string dbName, string query)
        {
            var i = Array.Empty<BsonDocument>();
            var r = Result<IEnumerable<BsonDocument>>.Success(i);
            return Task.FromResult<Result<IEnumerable<BsonDocument>>>(r);
        }
        

        public  Task<Result> AddAsync(string payload, string dbName, string colName)
        {
            return Task.FromResult<Result>(Result.Success());
        }      
        
        public Task<Result<string>> GetAsync(string dbName, string colName, string id)
        {
            return Task.FromResult<Result<string>>(Result<string>.Success(string.Empty));
        }

        public Task<Result> UpdateAsync(string payload, string dbName, string colName)
        {
            return Task.FromResult<Result>(Result.Success());
        }

        public Task<Result> RemoveAsync(string dbName, string colName, string id)
        {
            return Task.FromResult<Result>(Result.Success());
        }
    }
}