// ----------------------------------------------------------
//            文件：FolderService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 01:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.References
{
    public class FolderService : AsyncCollectionService<Folder>
    {
        public FolderService() : base(EngineNames.Reference, CollectionNames.Folder)
        {
            DbSet.EnsureIndex(nameof(Folder.Parent));
        }


    }
}