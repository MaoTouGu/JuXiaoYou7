// ----------------------------------------------------------
//            文件：DataService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 13:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using System.Diagnostics;
using LiteDB;
using NLog;

namespace MaoTouGu.Studio.Database.Core
{
    public abstract partial class DataService : DisposableObservableObject
    {
        private static readonly IThreadingInvoker _invoker;

        static DataService()
        {
            _invoker = Ioc.Get<IThreadingInvoker>();
        }

        protected DataService(IDatabaseManager manager, string dbName, string colName)
        {
            CollectionName = colName;
            DatabaseName   = dbName;
            EventID        = $"{dbName}.{colName}";

            // KikakuID = manager.KikakuID;

            Database = manager.GetDatabase(dbName);
            DbSet    = Database.GetCollection(colName);
            Logger   = LoggerExt.GetLogger(this);
        }

        protected virtual void OnException(string methodName, Exception ex)
        {

        }

        /// <summary>
        /// 当前数据服务的数据量。
        /// </summary>
        public int Count => DbSet.Count();
        
        

        public string DatabaseName   { get; }
        public string CollectionName { get; }
        public string EventID        { get; }

        protected ILogger Logger { get; }

        protected internal ILiteDatabase                 Database { get; }
        protected internal ILiteCollection<BsonDocument> DbSet    { get; }


        public virtual IBsonMapper Mapper => BsonMapperBase.Global;

        /// <summary>
        /// 用于线程同步的接口。
        /// </summary>
        protected IThreadingInvoker Invoker => _invoker;

    }
}