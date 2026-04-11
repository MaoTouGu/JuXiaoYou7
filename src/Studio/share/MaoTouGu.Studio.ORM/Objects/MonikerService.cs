// ----------------------------------------------------------
//            文件：MonikerService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 01:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using System.Reactive.Subjects;

namespace MaoTouGu.Studio
{
    public class MonikerService : AsyncCollectionService<Moniker>
    {
        public MonikerService() : base(EngineNames.Symbol, CollectionNames.Moniker, new ScopedSubjectCollection())
        {
            Favorite = new Subject<Moniker>();
            Recently = new Subject<Moniker>();
            Deleted  = new Subject<Moniker>();
        }

        sealed class ScopedSubjectCollection : ScopedCollection<Moniker>
        {
            protected override void OnAdded(Moniker target)
            {
                var ms = ((MonikerService)Impl);

                if (target.IsSoftDeleted)
                {
                    ms.Deleted.OnNext(target);
                    Collection.Remove(target);
                }
                else
                {
                    ms.Subject.OnNext(target);
                    ms.Recently.OnNext(target);
                    
                    if (target.IsStar)
                    {
                        ms.Favorite.OnNext(target);
                    }

                    Collection.Add(target);
                }
            }

            protected override void OnRemoved(Moniker target)
            {
                ((MonikerService)Impl).Deleted.OnNext(target);

                Collection.Remove(target);
            }

            public override bool Remove(string documentID)
            {
                bool r;
                try
                {
                    Database.BeginTrans();

                    var moniker = GetInstance(documentID);

                    moniker.IsSoftDeleted = true;
                    moniker.Modified      = DateTime.Now;

                    var document = Impl.Serialize(moniker);

                    r = DbSet.Update(document);

                    OnRemoved(moniker);

                    //
                    //
                    Database.Commit();

                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return r;
            }
        }

        protected override void OnEntityUpdated(Moniker target)
        {
            Favorite.OnNext(target);
        }

        public bool IsDeleted(string id)
        {
            if (!Has(id))
            {
                return true;
            }

            return Get(id).IsSoftDeleted;
        }

        public Subject<Moniker> Favorite { get; }
        public Subject<Moniker> Recently { get; }
        public Subject<Moniker> Deleted  { get; }
    }
}