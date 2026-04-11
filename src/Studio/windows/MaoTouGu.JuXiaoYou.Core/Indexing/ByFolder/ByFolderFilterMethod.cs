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

        public override async Task<Moniker> AddAsync(FilterViewModel viewModel)
        {
            var r = await AddMonikerAndReference(viewModel, Folder);

            if (!r.IsFinished)
            {
                return null;
            }

            return r.Value;
        }

        public override async Task RemoveAsync(FilterViewModel viewModel, Moniker x)
        {
            try
            {
                await RemoveMonikerAndReference(x, Folder);
                viewModel.RemoveSuccess();
            }
            catch(Exception e)
            {
            }
        }

        public Folder Folder { get; init; }

        public override string Name => Folder?.Name;
    }
}