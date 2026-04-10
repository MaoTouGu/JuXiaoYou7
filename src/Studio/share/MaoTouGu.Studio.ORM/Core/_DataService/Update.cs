// ----------------------------------------------------------
//            文件：Update.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:52
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService<T>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public async Task<bool> Update(T target)
        {
            if (target is null || !OnEntityUpdating(target))
            {
                return false;
            }

            var document = Serialize(target);
            try
            {
                OnEntityUpdated(target);


                if (Api.IsOnline)
                {
                    await _UpdateAsync(document);
                }
                else
                {
                    Database.BeginTrans();
                    DbSet.Update(document);
                    Database.Commit();
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
        protected virtual bool OnEntityUpdating(T target) => true;

        /// <summary>
        /// 当实体已经更新到数据库时的操作。
        /// </summary>
        /// <param name="target">要更新的实体</param>
        protected virtual void OnEntityUpdated(T target)
        {

        }
    }
}