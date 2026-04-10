// ----------------------------------------------------------
//            文件：EditCatalogCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 15:19
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.Commands
{
    sealed class EditCatalogCommand(CatalogViewModelBase target) : ContextCommand<Catalog, CatalogViewModelBase>(target)
    {
        protected override async void Execute(Catalog target)
        {
            var r = await Context.SingleLine("编辑", "编辑目录", target.Name);

            if (!r.IsFinished)
            {
                return;
            }

            target.Name = r.Value;

            await IndexSystem.CatalogService.Update(target);
        }
    }
}