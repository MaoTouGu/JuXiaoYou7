// ----------------------------------------------------------
//            文件：Get.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 03:00
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics;

namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService<T>
    {
        public T Get(string id)
        {
            var doc = DbSet.FindById(id);

            if (doc is null)
            {
                return null;
            }

            return Deserialize(doc);
        }

    }
}