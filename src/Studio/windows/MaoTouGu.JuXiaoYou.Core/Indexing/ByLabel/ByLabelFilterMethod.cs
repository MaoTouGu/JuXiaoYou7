// ----------------------------------------------------------
//            文件：ByLabelFilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 17:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using Label = MaoTouGu.Studio.Database.References.Label;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class ByLabelFilterMethod : FilterMethod
    {
        public override Task Filter(List<Moniker> originalSource, IList<Moniker> collection)
        {
            return Task.Run(() =>
                            {
                                var references = DatabaseManager.GetService<KeywordService>()
                                                                .FindByName(Label.Name);

                                var set = references.Select(x => x.DocumentID).ToHashSet();

                                originalSource.AddRange(collection.Where(x => set.Contains(x.Id) && !x.IsSoftDeleted));
                            });
        }


        public override async Task<Moniker> AddAsync(FilterViewModel viewModel)
        {
            var r = await AddMonikerAndKeyword(viewModel, Label);

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
                await RemoveMonikerAndKeyword(x, Label);
                viewModel.RemoveSuccess();
            }
            catch(Exception e)
            {
            }
        }

        public Label Label { get; init; }

        public override string Name => Label?.Name;
    }
}