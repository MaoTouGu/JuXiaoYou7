// ----------------------------------------------------------
//            文件：DataService.Local.Find.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 03:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService<T>
    {
        /// <summary>
        /// 查询指定的内容
        /// </summary>
        /// <param name="expression"></param>
        [Obsolete("尚未测试完成。")]
        protected async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            var expr      = Mapper.GetExpression<T, bool>(expression);
            var queryable = DbSet.Query();
            var query     = queryable.Where(expr).ToSQL(CollectionName);

            var r = await Ioc.SafeGet<IDataApiContract>()
                             .QueryAsync(DatabaseName, query);
            
            

            if (r.IsFinished)
            {
                return Array.Empty<T>();
            }

            return r.Value
                    .Select(Deserialize);
        }
    }
}