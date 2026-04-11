// ----------------------------------------------------------
//            文件：DataService.Local.FindByName.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 15:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService
    {
        /// <summary>
        /// 根据类型信息寻找对象。
        /// </summary>
        /// <param name="type">数据的类型。</param>
        /// <returns></returns>
        protected IEnumerable<BsonDocument> FindByType(Type type)
        {
            var name = DefaultTypeNameBinder.Instance.GetName(type);

            return DbSet.Find(Query.EQ(DBHelper.Field_Type, name));
        }

        /// <summary>
        /// 根据类型信息寻找对象。
        /// </summary>
        /// <typeparam name="T">数据的类型。</typeparam>
        /// <returns></returns>
        protected IEnumerable<BsonDocument> FindByType<T>() => FindByType(typeof(T));
    }
}