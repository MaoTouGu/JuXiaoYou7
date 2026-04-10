// ----------------------------------------------------------
//            文件：AsyncTableService`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月02日 23:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.Studio.Database.Core
{
    public class AsyncTableService<T> : DataService<T> where T : DatabaseObject
    {
        protected AsyncTableService(string dbName, string colName) : base(dbName, colName, new ScopedHybridSet<T>())
        {
            var scoped  = ((ScopedHybridSet<T>)LocalScoped);
            Dictionary = scoped.Dictionary;
            Collection = scoped.Collection;
        }

        protected override Task OnStart()
        {
            return Task.Run(() =>
                            {
                                Invoker.RunOnUIThread(() =>
                                                      {
                                                          var ius = Ioc.Get<IUserService>();
                                                          
                                                          foreach (var item in DbSet.FindAll()
                                                                                    .Select(Deserialize))
                                                          {
                                                              if (item is Authorable ao &&
                                                                  !string.IsNullOrEmpty(ao.Creator) && 
                                                                  ius.Dictionary.TryGetValue(ao.Creator, out var usr))
                                                              {
                                                                  ao.CreatorName = usr.DisplayName;
                                                              }

                                                              if (Dictionary.TryAdd(item.Id, item))
                                                              {
                                                                  Collection.Add(item);
                                                              }
                                                          }
                                                      });
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

        public ViewList<T>           Collection { get; }
        public Dictionary<string, T> Dictionary { get; }
    }
}