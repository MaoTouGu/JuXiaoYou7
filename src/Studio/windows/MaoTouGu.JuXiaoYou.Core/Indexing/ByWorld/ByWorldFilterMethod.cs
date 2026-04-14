// ----------------------------------------------------------
//            文件：ByWorldFilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 17:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class ByWorldFilterMethod : FilterMethod
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var references = DatabaseManager.GetService<UniqueReferenceService>()
                                                                .Find(TopClass.Id, SubClass.Id);

                                var set = references.Select(x => x.DocumentID).ToHashSet();

                                originalSource.AddRange(collection.Where(x => set.Contains(x.Id) && !x.IsSoftDeleted));
                            });
        }

        public override async Task<Moniker> AddAsync(FilterViewModel viewModel)
        {
            var r = await AddMonikerAndUniqueReference(viewModel, TopClass, SubClass);

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
                await RemoveMonikerAndUniqueReference(x, TopClass, SubClass);
                viewModel.RemoveSuccess();
            }
            catch(Exception e)
            {
            }
        }

        public TopClass TopClass { get; init; }
        public SubClass SubClass { get; init; }

        public override string Name => TopClass?.Name;
    }
}