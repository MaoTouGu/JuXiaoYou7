// ----------------------------------------------------------
//            文件：KeywordService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 01:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.References
{
    public class KeywordService : DataService<Keyword>
    {
        public KeywordService() : base(EngineNames.System, CollectionNames.Keyword)
        {
            DbSet.EnsureIndex(nameof(Keyword.Name));
        }

        public IEnumerable<Keyword> Find(string name)
        {
            var r = Query.EQ(nameof(Keyword.Name), name);

            return DbSet.Find(r)
                        .Select(Deserialize);
        }
    }
}