// ----------------------------------------------------------
//            文件：IBackgroundDataSyncService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:46
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Core
{
    public interface IBackgroundDataSyncService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        bool CanHandle(string eventID);

        /// <summary>
        /// 处理该后台事件。。
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isSelfOperating"></param>
        /// <returns></returns>
        Task Handle(DataChangedSpot e, bool isSelfOperating);
    }
}