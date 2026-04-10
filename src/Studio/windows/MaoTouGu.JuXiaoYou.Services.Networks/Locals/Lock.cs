// ----------------------------------------------------------
//            文件：Lock.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月27日 18:56
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Networks
{
    partial class LocalApi
    {
        private const string Lock_Get       = "/api/Lock/get";
        private const string Lock_Open      = "/api/Lock/open";
        private const string Lock_Refresh   = "/api/Lock/refresh";
        private const string Lock_Release   = "/api/Lock/release";
        private const string Lock_Remove    = "/api/Lock/remove";
        private const string Lock_RemoveAll = "/api/Lock/removeAll";


        //-------------------------------------------------------------
        //
        //                         Lock 
        //
        //-------------------------------------------------------------

        /// <summary>
        /// 判断是否存在锁。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  Task<Result<string>> HasLockAsync(string id)
        {
            var r = Result<string>.Success(UserID);
            return Task.FromResult(r);
        }

        /// <summary>
        /// 获得文档锁。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result> GetLockAsync(string id)
        {
            return Task.FromResult(Result.Success());
        }
        
        /// <summary>
        /// 刷新文档锁。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result> RefreshLockAsync(string id)
        {
            return Task.FromResult(Result.Success());
        }       
        
        /// <summary>
        /// 释放文档锁。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result> ReleaseLockAsync(string id)
        {
            return Task.FromResult(Result.Success());
        }   
        
        /// <summary>
        /// 释放自己所有拥有的文档锁。
        /// </summary>
        /// <returns></returns>
        public Task<Result> RemoveAsync()
        {
            return Task.FromResult(Result.Success());
        }
        
        /// <summary>
        /// 移除所有文档锁
        /// </summary>
        /// <returns></returns>
        public Task<Result> RemoveLocksAsync()
        {
            return Task.FromResult(Result.Success());
        }
    }
}