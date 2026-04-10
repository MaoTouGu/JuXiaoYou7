// ----------------------------------------------------------
//            文件：UniqueReferenceService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 01:31
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.Studio.References
{
    public class UniqueReferenceService : DataService<UniqueReference>
    {
        public UniqueReferenceService() : base(EngineNames.Reference, CollectionNames.Unique)
        {

        }

        public IEnumerable<UniqueReference> Find(string topClass, string subClass)
        {
            var l = Query.EQ(nameof(UniqueReference.TopClass), topClass);
            var r = Query.EQ(nameof(UniqueReference.SubClass), subClass);

            return DbSet.Find(Query.And(l, r))
                        .Select(Deserialize);
        }
    }
}