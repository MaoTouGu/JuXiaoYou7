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
        protected override async Task EntityBackgroundUpdating(string handlerID, string documentID, bool isSelfOperating)
        {
            try
            {
                if (isSelfOperating)
                {
                    return;
                }
                
                //
                // 不是远端的为了避免麻烦，直接删除后添加。
                await EntityBackgroundRemoving(handlerID, documentID);
                await EntityBackgroundAdding(handlerID, documentID);
                
                //
                //
                // 有的时候需要通知用户关闭，并重新打开。
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
                //
                // 直接更新。
                Database.BeginTrans();
                DbSet.Update(document);
                Database.Commit();
                
                //
                // 更新到Remote设备。
                if (Api.IsOnline)
                {
                    await _UpdateAsync(document);
                }
                
                //
                // 派生类做额外操作。
                OnEntityUpdated(target);
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