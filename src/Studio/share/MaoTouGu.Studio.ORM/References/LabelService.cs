// ----------------------------------------------------------
//            文件：LabelService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 16:10
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.References
{
    public class LabelService : AsyncCollectionService<Label>
    {
        public LabelService() : base(EngineNames.Reference, CollectionNames.Label)
        {
            DbSet.EnsureIndex(nameof(Reference.Name));
        }

    }
}