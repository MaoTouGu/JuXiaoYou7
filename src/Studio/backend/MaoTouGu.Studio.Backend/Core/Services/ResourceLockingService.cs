// ----------------------------------------------------------
//            文件：ResourceLockingService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 19:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Collections.Concurrent;

namespace MaoTouGu.Studio.Services
{
    public class ResourceLockingService(ILogger<ResourceLockingService> _Logger) : IResourceLockingService
    {
        private readonly ConcurrentDictionary<string, ResourceLock> _Table = new();

        public void Add(string id, string owner)
        {
            var now = DateTime.Now;
            
            if (!_Table.TryAdd(id, new ResourceLock
                {
                    DocumentID    = id,
                    OwnerID       = owner,
                    TimeOfCreated = now,
                    TimeOfExpires = now + TimeSpan.FromMinutes(30),
                }))
            {
                _Logger.LogWarning($"无法为用户(ID = {owner})创建文档(ID = {id})的锁。");
            }
        }
        
        public void Release(string id)
        {
            _Table.Remove(id, out _);
        }

        public void Refresh(string id)
        {
            if (!_Table.TryGetValue(id, out var dl))
            {
                return;
            }
            
            dl.Refresh();
        }
        
        public ResourceLock Has(string id) => _Table.GetValueOrDefault(id);

        public void ReleaseInvalidatedLocks()
        {
            var expiredItems = _Table.Where(x => x.Value.IsExpired)
                                     .Select(x => x.Key)
                                     .ToList();
            
            expiredItems.ForEach(x => _Table.TryRemove(x, out _));
        }
        
        public void ReleaseAll(string id)
        {
            var expiredItems = _Table.Where(x => x.Value.OwnerID == id)
                                     .Select(x => x.Key)
                                     .ToList();
            
            expiredItems.ForEach(x => _Table.TryRemove(x, out _));
        }
        
        public void ReleaseAll()
        {
            _Table.Clear();
        }
    }
}