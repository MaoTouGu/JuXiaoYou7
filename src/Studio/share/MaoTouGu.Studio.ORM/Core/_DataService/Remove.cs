// ----------------------------------------------------------
//            文件：Remove.cs
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
        /// 判断当前实体是否可以删除到数据库中？
        /// </summary>
        /// <param name="target">要删除的实体</param>
        /// <returns>如果支持删除到数据库中，则返回true，否则返回false。</returns>
        protected virtual bool OnEntityRemoving(T target) => true;

        protected override async Task EntityBackgroundRemoving(string handlerID, string documentID)
        {
            try
            {
                Invoker.RunOnUIThread(() =>
                                      {
                                          LocalScoped.Remove(documentID);
                                      });

                await Task.CompletedTask;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="target">要删除的实体</param>
        public async Task<bool> Remove(T target)
        {
            if (target is null || !OnEntityRemoving(target))
            {
                return false;
            }

            if (Api.IsOnline)
            {
                return await _DeleteAsync(target.Id);
            }
            
            try
            {
                return LocalScoped.Remove(target.Id);
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="id">要删除的实体</param>
        public Task<bool> Remove(string id) => Remove(LocalScoped.GetInstance(id));
    }
}