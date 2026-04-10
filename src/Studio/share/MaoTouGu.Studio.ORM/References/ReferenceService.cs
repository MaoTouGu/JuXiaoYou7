// ----------------------------------------------------------
//            文件：ReferenceService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 16:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
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
    }
}