// ----------------------------------------------------------
//            文件：HasID.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService<T>
    {
        /// <summary>
        /// 判断指定ID的实体是否存在。
        /// </summary>
        /// <param name="id">实体的ID。</param>
        /// <returns>返回指定ID的实体是否存在，true表示存在，否则为false。</returns>
        public bool Has(string id) => DbSet.HasID(id);
    }
}