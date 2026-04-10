// ----------------------------------------------------------
//            文件：ByFolderFilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 17:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class ByFolderFilterMethod : FilterMethod
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var references = DatabaseManager.GetService<ReferenceService>()
                                                                .Find(Folder.Name);

                                var set = references.Select(x => x.DocumentID).ToHashSet();

                                originalSource.AddRange(collection.Where(x => set.Contains(x.Id) && !x.IsSoftDeleted));
                            });
        }

        public Folder Folder { get; init; }
    }
}