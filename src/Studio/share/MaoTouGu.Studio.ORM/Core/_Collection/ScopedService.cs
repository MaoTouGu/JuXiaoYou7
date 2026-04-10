// ----------------------------------------------------------
//            文件：ScopedService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 14:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.Studio.Database.Core
{
    public class ScopedService<T> where T : DatabaseObject
    {

        /// <summary>
        /// 当实体已经添加到数据库时的操作。
        /// </summary>
        /// <param name="target">要添加的实体</param>
        protected virtual void OnAdded(T target)
        {

        }

        /// <summary>
        /// 当实体已经删除到数据库时的操作。
        /// </summary>
        /// <param name="target">要删除的实体</param>
        protected virtual void OnRemoved(T target)
        {

        }

        public void Add(User usr, BsonDocument document)
        {
            var entity = Impl.Deserialize(document);

            if (entity is Authorable authorable)
            {
                authorable.CreatorName = usr.DisplayName;
            }

            try
            {
                Database.BeginTrans();
                DbSet.Insert(document);
                Database.Commit();

                OnAdded(entity);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public BsonDocument Get(string documentId) => DbSet.FindById(documentId);

        public virtual T GetInstance(string id)
        {
            var document = Get(id);
            return Impl.Deserialize(document);
        }


        public virtual bool Remove(string documentID)
        {
            try
            { 
                Database.BeginTrans();

                //
                //
                var r = DbSet.Delete(documentID);
                OnRemoved(GetInstance(documentID));

                //
                //
                Database.Commit();

                return r;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        internal DataService<T> Impl { get; set; }

        protected ILiteDatabase                 Database => Impl.Database;
        protected ILiteCollection<BsonDocument> DbSet    => Impl.DbSet;
    }
}