// ----------------------------------------------------------
//            文件：AsyncCollectionService`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月25日 17:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells;
using MaoTouGu.Shells.Core;
using MaoTouGu.Shells.Threadings;
using MaoTouGu.Studio.Database;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Core
{
    public abstract class AsyncCollectionService<T> : DataService<T> where T : DatabaseObject
    {

        protected AsyncCollectionService(string dbName, string colName) : this(dbName, colName, new ScopedCollection<T>())
        {
        }
        
        protected AsyncCollectionService(string dbName, string colName, ScopedService<T> service) : base(dbName, colName, service)
        {
            Collection = ((ScopedCollection<T>)LocalScoped).Collection;
        }
        
        private void OnStartImpl()
        {
            var ius = Ioc.Get<IUserService>();

            foreach (var item in DbSet.FindAll())
            {
               OnAssemblyItem(ius, item);
            }
        }

        protected virtual void OnAssemblyItem(IUserService ius, BsonDocument document)
        {
            var item = Deserialize(document);
            
            if (item is Authorable ao)
            {
                ao.CreatorName = ius.Dictionary.SafetyGet(ao.Creator)?.DisplayName;
            }

            Collection.Add(item);
        }


        protected override void OnEntityUpdated(T target)
        {
            var (idx, inside) = Collection.FindAndGet(x => x.Id == target.Id);

            if (inside is null)
            {
                Collection.Add(target);
            }
            else
            {
                if (!ReferenceEquals(target, inside))
                {
                    Collection.RemoveAt(idx);
                    Collection.Add(target);
                }

            }
        }

        protected override Task OnStart()
        {
            return Task.Run(() =>
                            {
                                Invoker.RunOnUIThread(OnStartImpl);
                            });
        }

        protected override Task OnStop()
        {
            return Task.Run(() =>
                            {
                                Invoker.RunOnUIThread(() =>
                                                      {
                                                          Collection.Clear();
                                                      });
                            });
        }

        public ViewList<T> Collection { get; }
    }
}