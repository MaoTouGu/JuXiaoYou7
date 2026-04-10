// ----------------------------------------------------------
//            文件：DocumentLockService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年01月27日 18:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Services.Backgrounds
{
    public class DocumentLockService(IResourceLockingService _Service) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //
                // 延迟60s检测一次。
                await Task.Delay(60 * 1000, stoppingToken);

                //
                // 释放所有过期的锁。
                _Service.ReleaseInvalidatedLocks();
            }
        }
    }
}