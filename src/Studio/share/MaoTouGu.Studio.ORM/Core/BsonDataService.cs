// ----------------------------------------------------------
//            文件：BsonDataService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 15:35
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    public abstract class BsonDataService : DataService
    {
        protected BsonDataService(string dbName, string colName) : base(Ioc.Get<IDatabaseManager>(), dbName, colName)
        {
            
        }
        
        
        #region Add

        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="target">要添加的实体</param>
        public async Task<bool> Add(BsonDocument target)
        {
            if (target is null || !OnEntityAdding(target))
            {
                return false;
            }

            try
            {

                Database.BeginTrans();
                DbSet.Insert(target);
                OnEntityAdded(target);
                Database.Commit();

                if (Ioc.SafeGet<IDataApiContract>() is {} api)
                {
                    var json = JsonSerializer.Serialize(target);
                    await api.AddAsync(json, DatabaseName, CollectionName);
                }

                return true;

            }
            catch(Exception e)
            {
                Logger.Warn(e.Message);
                Database.Rollback();
            }

            return false;
        }

        /// <summary>
        /// 判断当前实体是否可以添加到数据库中？
        /// </summary>
        /// <param name="target">要添加的实体</param>
        /// <returns>如果支持添加到数据库中，则返回true，否则返回false。</returns>
        protected virtual bool OnEntityAdding(BsonDocument target) => true;

        /// <summary>
        /// 当实体已经添加到数据库时的操作。
        /// </summary>
        /// <param name="target">要添加的实体</param>
        protected virtual void OnEntityAdded(BsonDocument target)
        {

        }

        #endregion

        #region Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public async Task<bool> Update(BsonDocument target)
        {
            if (target is null || !OnEntityUpdating(target))
            {
                return false;
            }

            try
            {
                Database.BeginTrans();
                DbSet.Update(target);
                Database.Commit();
                OnEntityUpdated(target);


                if (Ioc.SafeGet<IDataApiContract>() is {} api)
                {
                    var json = JsonSerializer.Serialize(target);
                    await api.UpdateAsync(json, DatabaseName, CollectionName);
                }

                return true;
            }
            catch(Exception e)
            {
                Logger.Warn(e.Message);
                Database.Rollback();
            }

            return false;
        }


        /// <summary>
        /// 判断当前实体是否可以更新到数据库中？
        /// </summary>
        /// <param name="target">要更新的实体</param>
        /// <returns>如果支持更新到数据库中，则返回true，否则返回false。</returns>
        protected virtual bool OnEntityUpdating(BsonDocument target) => true;

        /// <summary>
        /// 当实体已经更新到数据库时的操作。
        /// </summary>
        /// <param name="target">要更新的实体</param>
        protected virtual void OnEntityUpdated(BsonDocument target)
        {

        }

        #endregion

        #region Remove

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="target">要删除的实体</param>
        public async Task<bool> Remove(BsonDocument target)
        {
            if (target is null || !OnEntityRemoving(target))
            {
                return false;
            }

            if (!target.TryGetValue(DBHelper.Field_ID, out var id))
            {
                return false;
            }
            
            try
            {
                Database.BeginTrans();
                DbSet.Delete(id);
                Database.Commit();
                OnEntityRemoved(target);


                if (Ioc.SafeGet<IDataApiContract>() is {} api)
                {
                    await api.RemoveAsync(id, DatabaseName, CollectionName);
                }

                return true;
            }
            catch(Exception e)
            {
                Logger.Warn(e.Message);
                Database.Rollback();
            }

            return false;
        }

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="id">要删除的实体</param>
        public async Task<bool> Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            
            var value = DbSet.FindById(id);

            return await Remove(value);
        }


        /// <summary>
        /// 判断当前实体是否可以删除到数据库中？
        /// </summary>
        /// <param name="target">要删除的实体</param>
        /// <returns>如果支持删除到数据库中，则返回true，否则返回false。</returns>
        protected virtual bool OnEntityRemoving(BsonDocument target) => true;

        /// <summary>
        /// 当实体已经删除到数据库时的操作。
        /// </summary>
        /// <param name="target">要删除的实体</param>
        protected virtual void OnEntityRemoved(BsonDocument target)
        {

        }

        #endregion
    }
}