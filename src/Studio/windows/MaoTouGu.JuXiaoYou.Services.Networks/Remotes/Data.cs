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
    partial class RemoteApi
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
        public async Task<Result<IEnumerable<BsonDocument>>> GetCollectionAsync(string dbName, string colName)
        {
            return await GetBsonDocument($"{Data_All}?dbName={dbName}&colName={colName}");
        }    
        
        public async Task<Result<IEnumerable<BsonDocument>>> QueryAsync(string dbName, string query)
        {
            return await GetBsonDocument($"{Data_Query}?dbName={dbName}&query={query}");
        }
        

        public async Task<Result> AddAsync(string payload, string dbName, string colName)
        {
            return await PostJsonAndReturnResult($"{Data_Add}?dbName={dbName}&colName={colName}", payload);
        }      
        
        public async Task<Result<string>> GetAsync(string dbName, string colName, string id)
        {
            return await GetAndReturnJsonString($"{Data_Get}?dbName={dbName}&colName={colName}&id={id}");
        }

        public async Task<Result> UpdateAsync(string payload, string dbName, string colName)
        {
            return await PostJsonAndReturnResult($"{Data_Update}?dbName={dbName}&colName={colName}", payload);
        }

        public async Task<Result> RemoveAsync(string dbName, string colName, string id)
        {
            return await GetAndReturnResult($"{Data_Remove}?dbName={dbName}&colName={colName}&id={id}");
        }
    }
}