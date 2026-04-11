// ----------------------------------------------------------
//            文件：ReferenceService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 16:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database;

namespace MaoTouGu.Studio.References
{
    public class ReferenceService : DataService<Reference>
    {
        public ReferenceService() : base(EngineNames.Reference, nameof(Reference))
        {
            DbSet.EnsureIndex(nameof(Reference.Name));
        }

        public IEnumerable<Reference> Find(string folderName) => DbSet.Find(Query.EQ(nameof(Reference.Name), folderName))
                                                                      .Select(Deserialize);
        
        public Reference Find(string documentID, string folderName)
        {
            var document = FindDocument(documentID, folderName);

            if (document is not null)
            {
                return Deserialize(document);
            }

            return null;
        }
        
        
        public BsonDocument FindDocument(string documentID, string folderName)
        {
            var left     = Query.EQ(nameof(Reference.DocumentID), documentID);
            var right    = Query.EQ(nameof(Reference.Name), folderName);
            var document = DbSet.FindOne(Query.And(left, right));

            return document;
        }

        public async Task<bool> Delete(string documentID, string folderName)
        {
            var document = FindDocument(documentID, folderName);

            if (document is null || !document.TryGetValue(DBHelper.Field_ID, out var value))
            {
                return false;
            }

            return await Remove(value.AsString);
        }
    }
}