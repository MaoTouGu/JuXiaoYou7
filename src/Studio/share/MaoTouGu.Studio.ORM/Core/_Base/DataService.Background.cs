// ----------------------------------------------------------
//            文件：DataService.Background.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService : IBackgroundDataSyncService
    {
        protected abstract Task EntityBackgroundAdding(string handlerID, string documentID);
        protected abstract Task EntityBackgroundRemoving(string handlerID, string documentID);
        protected abstract Task EntityBackgroundUpdating(string handlerID, string documentID, bool isSelfOperating);

        /// <summary>
        /// 判断是否可以处理
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        protected virtual bool CanHandleOverride(string eventID) => eventID == EventID;

        /// <summary>
        /// 判断是否可以处理
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        bool IBackgroundDataSyncService.CanHandle(string eventID) => CanHandleOverride(eventID);

        /// <summary>
        /// 处理。
        /// </summary>
        /// <param name="change"></param>
        /// <param name="isSelfOperating">是否为自身在操作。</param>
        /// <returns></returns>
        async Task IBackgroundDataSyncService.Handle(DataChangedSpot change, bool isSelfOperating)
        {
            if (change.Operation == DataOperation.Added)
            {
                await EntityBackgroundAdding(change.HandlerID, change.DocumentID);
            }
            else if (change.Operation == DataOperation.Removed)
            {
                await EntityBackgroundRemoving(change.HandlerID, change.DocumentID);
            }
            else
            {
                await EntityBackgroundUpdating(change.HandlerID, change.DocumentID, isSelfOperating);
            }
        }
    }
}