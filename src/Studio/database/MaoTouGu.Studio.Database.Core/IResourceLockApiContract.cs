// ----------------------------------------------------------
//            文件：IResourceLockApiContract.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 09:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Database
{
    public interface IResourceLockApiContract
    {
        /// <summary>
        /// 判断是否存在锁。
        /// </summary>
        Task<Result<string>> HasLockAsync(string id);
        
        /// <summary>
        /// 获得文档锁。
        /// </summary>
        Task<Result> GetLockAsync(string id);
        
        /// <summary>
        /// 刷新文档锁。
        /// </summary>
        Task<Result> RefreshLockAsync(string id);
        
        /// <summary>
        /// 释放文档锁。
        /// </summary>
        Task<Result> ReleaseLockAsync(string id);
        
        /// <summary>
        /// 释放自己所有拥有的文档锁。
        /// </summary>
        Task<Result> RemoveAsync();
        
        /// <summary>
        /// 移除所有文档锁
        /// </summary>
        Task<Result> RemoveLocksAsync();
    }
}