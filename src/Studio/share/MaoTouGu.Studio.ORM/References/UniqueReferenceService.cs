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
        
        
        public BsonDocument FindDocument(string documentID, string topClassId, string subClassId)
        {
            var l        = Query.EQ(nameof(UniqueReference.TopClass), topClassId);
            var r        = Query.EQ(nameof(UniqueReference.SubClass), subClassId);
            var b        = Query.EQ(nameof(UniqueReference.SubClass), documentID);
            var a        = Query.And(l, r);
            var document = DbSet.FindOne(Query.And(a, b));

            return document;
        }
        
        public UniqueReference Find(string documentID, string topClassId, string subClassId)
        {
            var document = FindDocument(documentID, topClassId, subClassId);

            if (document is not null)
            {
                return Deserialize(document);
            }

            return null;
        }

        public async Task Remove(string documentID, string topClassId, string subClassId)
        {
            var document = FindDocument(documentID, topClassId, subClassId);

            if (document is null || !document.TryGetValue(DBHelper.Field_ID, out var value))
            {
                return;
            }
            await Remove(value.AsString);
        }
    }
}