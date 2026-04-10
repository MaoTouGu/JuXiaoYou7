// ----------------------------------------------------------
//            文件：Add.cs
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
        /// 判断当前实体是否可以添加到数据库中？
        /// </summary>
        /// <param name="target">要添加的实体</param>
        /// <returns>如果支持添加到数据库中，则返回true，否则返回false。</returns>
        protected internal virtual bool OnEntityAdding(T target) => true;
        

        protected override async Task EntityBackgroundAdding(string handlerID, string documentID)
        {
            //
            // 避免网络包堵塞时，重新尝试申请进而导致接收到两个相同的包
            // 而触发Duplicate问题
            if (DbSet.HasID(documentID))
            {
                return;
            }

            try
            {
                var document = await GetAsync(documentID);
                var usr      = GetUser(handlerID);

                if (document is null)
                {
                    return;
                }

                Invoker.RunOnUIThread(() =>
                                      {
                                          LocalScoped.Add(usr, document);
                                      });
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="target">要添加的实体</param>
        public async Task Add(T target)
        {
            if (target is null || !OnEntityAdding(target))
            {
                return;
            }

            try
            {
                //
                // 序列化。
                var document = Serialize(target);

                if (Api.IsOnline)
                {
                    await _AddAsync(document);
                    return;
                }
                
                try
                {

                    //
                    // 开始事务。
                    Database.BeginTrans();
                    DbSet.Insert(document);
                    //
                    // 提交事务更改。
                    Database.Commit();

                    var a   = target as Authorable;
                    var usr = GetUser(a?.Creator);

                    LocalScoped.Add(usr, document);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            catch(Exception e)
            {
                //
                // 回滚事务。
                Logger.Warn(e.Message);
                Database.Rollback();
            }
        }
    }
}