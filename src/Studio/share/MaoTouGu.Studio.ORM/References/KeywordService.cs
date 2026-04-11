// ----------------------------------------------------------
//            文件：KeywordService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 01:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.Studio.Keywords
{
    public class KeywordService : DataService<Keyword>
    {
        public KeywordService() : base(EngineNames.System, CollectionNames.Keyword)
        {
            DbSet.EnsureIndex(nameof(Keyword.Name));
        }

        public IEnumerable<Keyword> FindByName(string name)
        {
            var r = Query.EQ(nameof(Keyword.Name), name);

            return DbSet.Find(r)
                        .Select(Deserialize);
        }
        
        /// <summary>
        /// 给定一个DocumentID，寻找所有<see cref="Keyword.DocumentID"/>属性与之相同的数据。
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public IEnumerable<Keyword> FindByDocumentId(string documentID)
        {
            var r = Query.EQ(nameof(Keyword.DocumentID), documentID);

            return DbSet.Find(r)
                        .Select(Deserialize);
        }

        /// <summary>
        /// 给定一个DocumentID与<see cref="Keyword.Name"/>，寻找对应的关键字。
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public Keyword Find(string documentID, string folderName)
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
            var left     = Query.EQ(nameof(Keyword.DocumentID), documentID);
            var right    = Query.EQ(nameof(Keyword.Name), folderName);
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