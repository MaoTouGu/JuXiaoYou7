// ----------------------------------------------------------
//            文件：FilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 01:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Keywords;
using Label = MaoTouGu.Studio.Database.References.Label;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class FilterMethod
    {
        public abstract Task Filter(List<Moniker> originalSource, IList<Moniker> collection);



        public static async Task<Result<Moniker>> Add(PageBase viewModel)
        {
            var r = await viewModel.SingleLine("新建", "创建一个设定");

            if (!r.IsFinished)
            {
                return Result<Moniker>.Failure;
            }

            var moniker = Moniker.Create(r.Value, GlobalSettings.User);

            if (moniker is null)
            {
                return Result<Moniker>.Failure;
            }

            await DatabaseManager.GetService<MonikerService>()
                                 .Add(moniker);

            return Result<Moniker>.Success(moniker);
        }

        public static async Task Remove(Moniker target)
        {
            await DatabaseManager.GetService<MonikerService>()
                                 .Remove(target);
        }

        /// <summary>
        /// 添加设定并附加到Folder当中去。
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="folder"></param>
        /// <param name="refService"></param>
        public static async Task<Result<Moniker>> AddMonikerAndReference(PageBase viewModel, Folder folder, ReferenceService refService = null)
        {
            var r = await Add(viewModel);

            if (r.IsFinished)
            {
                var keyword = new Reference
                {
                    Id         = ID.Get(),
                    DocumentID = r.Value.Id,
                    Name       = folder.Name,
                };

                refService ??= DatabaseManager.GetService<ReferenceService>();

                await refService.Add(keyword);
            }

            return r;
        }

        public static async Task RemoveMonikerAndReference(Moniker x, Folder folder, ReferenceService refService = null)
        {
            refService ??= DatabaseManager.GetService<ReferenceService>();

            await refService.Delete(x.Id, folder.Name);
            await Remove(x);
        }

        /// <summary>
        /// 添加设定并附加到标签当中去。
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="label"></param>
        /// <param name="keywordService"></param>
        public static async Task<Result<Moniker>> AddMonikerAndKeyword(PageBase viewModel, Label label, KeywordService keywordService = null)
        {
            var r = await Add(viewModel);

            if (r.IsFinished)
            {
                var keyword = new Keyword
                {
                    Id         = ID.Get(),
                    DocumentID = r.Value.Id,
                    Name       = label.Name,
                };

                keywordService ??= DatabaseManager.GetService<KeywordService>();

                await keywordService.Add(keyword);
            }

            return r;
        }

        /// <summary>
        /// 添加设定并附加到标签当中去。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="label"></param>
        /// <param name="keywordService"></param>
        public static async Task RemoveMonikerAndKeyword(Moniker x, Label label, KeywordService keywordService = null)
        {

            keywordService ??= DatabaseManager.GetService<KeywordService>();


            await keywordService.Delete(x.Id, label.Name);
            await Remove(x);
        }

        /// <summary>
        /// 添加唯一分类。
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="topClass"></param>
        /// <param name="subClass"></param>
        /// <param name="uRefService"></param>
        public static async Task<Result<Moniker>> AddMonikerAndUniqueReference(PageBase viewModel, TopClass topClass, SubClass subClass, UniqueReferenceService uRefService = null)
        {
            var r = await Add(viewModel);

            if (r.IsFinished)
            {
                var reference = new UniqueReference
                {
                    Id         = ID.Get(),
                    DocumentID = r.Value.Id,
                    TopClass   = topClass.Id,
                    SubClass   = subClass.Id,
                };

                uRefService ??= DatabaseManager.GetService<UniqueReferenceService>();

                await uRefService.Add(reference);
            }


            return r;
        }

        public static async Task RemoveMonikerAndUniqueReference(Moniker x, TopClass topClass, SubClass subClass, UniqueReferenceService uRefService = null)
        {

            uRefService ??= DatabaseManager.GetService<UniqueReferenceService>();

            await uRefService.Remove(x.Id, topClass.Id, subClass.Id);
            await Remove(x);
        }

        public abstract Task<Moniker> AddAsync(FilterViewModel viewModel);

        public abstract Task RemoveAsync(FilterViewModel viewModel, Moniker x);
        public abstract string Name { get; }
    }
}