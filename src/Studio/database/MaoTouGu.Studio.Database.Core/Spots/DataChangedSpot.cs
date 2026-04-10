// ----------------------------------------------------------
//            文件：DataChangedSpot.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 23:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Spots
{
    public class DataChangedSpot : Spot
    {
        public static string GetEventID(string dbName, string collectionName) => $"{dbName}.{collectionName}";
        
        /// <summary>
        /// 修改的文档对象ID。
        /// </summary>
        public string DocumentID { get; init; }

        /// <summary>
        /// 修改者的ID。
        /// </summary>
        public string HandlerID { get; init; }

        /// <summary>
        /// 事件源。
        /// </summary>
        public string EventID { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        public DataOperation Operation { get; init; }
    }
}