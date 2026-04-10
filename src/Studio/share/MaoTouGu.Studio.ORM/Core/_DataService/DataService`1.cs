// ----------------------------------------------------------
//            文件：DataService`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 13:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics;
using System.Reactive.Subjects;
using MaoTouGu.Studio.Database;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Core
{
    public abstract partial class DataService<T> : DataService, IObservable<T> where T : DatabaseObject
    {
        private readonly Subject<T> _subject;
        
        /*
         *              [用户操作]
         *                 |
         *                 |
         *                 |    本地Api
         *                 +---[后台数据更新到前台]---+
         *                 |                      |
         *                 |                      |
         *                 |                      |
         *           [发送到服务器]                 |
         *                 |                      |
         *                 |                      |
         *                 |                      |
         *                 +------[服务器推送]------+
         */

        protected DataService(string dbName, string colName, ScopedService<T> localScoped = null) : base(Ioc.Get<IDatabaseManager>(), dbName, colName)
        {
            _subject    = new Subject<T>();
            LocalScoped = localScoped ?? new ScopedService<T>();
            
            
            InitializeEntityMapper();
            LocalScoped.Impl = this;
        }
        
        /*******************************************************************
         *
         *
         *
         *
         * 
         *******************************************************************/

        void InitializeEntityMapper()
        {
            if (Mapper is not BsonMapperBase bmb)
            {
                return;
            }

            EntityMapper = bmb.TypeMapper[typeof(T)];
        }


        protected sealed override async Task StartBefore()
        {
            if (Ioc.SafeGet<IDataApiContract>() is not {} api)
            {
                return;
            }

            //
            //
            if (!api.IsOnline)
            {
                return;
            }

            //
            // 在访问前，初始化集合。
            var r = await api.GetCollectionAsync(DatabaseName, CollectionName);

            if (r is null)
            {
                return;
            }

            DbSet.DeleteAll();
            DbSet.Insert(r.Value);
        }

        protected override Task StopAfter()
        {
            _subject.Dispose();
            return base.StopAfter();
        }

        /*******************************************************************
         *
         *
         *                      Public Methods
         *
         *
         *******************************************************************/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<T> observer) => _subject.Subscribe(observer);
        
        /// <summary>
        /// 所有数据。
        /// </summary>
        public IEnumerable<T> GetEntities() => DbSet.FindAll()
                                                    .Select(Deserialize);
        
        /*******************************************************************
         *
         *
         *                      Protected Properties
         *
         *
         *******************************************************************/
        
        protected EntityMapper EntityMapper { get; private set; }
        
        /*******************************************************************
         *
         *
         *                      Public Properties
         *
         *
         *******************************************************************/

        public Subject<T> Subject => _subject;
        
        /// <summary>
        /// 
        /// </summary>
        public ScopedService<T> LocalScoped { get; }
    }
}