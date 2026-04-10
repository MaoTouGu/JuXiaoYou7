// ----------------------------------------------------------
//            文件：IResourceLockingService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 15:16
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Core
{
    public interface IResourceLockingService
    {
        void Add(string id, string owner);
        void Release(string id);
        void Refresh(string id);
        ResourceLock Has(string id);
        
        void ReleaseInvalidatedLocks();
        void ReleaseAll(string id);
        void ReleaseAll();
    }
}