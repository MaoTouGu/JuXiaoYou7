// ----------------------------------------------------------
//            文件：RemoveCatalogCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 15:19
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.Commands
{
    sealed class RemoveCatalogCommand(CatalogViewModelBase target) : ContextCommand<Catalog, CatalogViewModelBase>(target)
    {
        protected override async void Execute(Catalog target)
        {
            if (!await Context.RemoveThis())
            {
                return;
            }

            await IndexSystem.CatalogService.Remove(target);

            Context.Catalogs.Remove(target);

            if (Context.Catalog == target)
            {
                var index = Context.Catalogs.IndexOf(target);

                if (index >= Context.Catalogs.Count)
                {
                    Context.Catalog = Context.Catalogs.LastOrDefault();
                }
                else
                {
                    Context.Catalog = Context.Catalogs[index];
                }

            }
        }
    }
}