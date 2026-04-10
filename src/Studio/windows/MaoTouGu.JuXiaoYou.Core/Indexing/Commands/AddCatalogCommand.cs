// ----------------------------------------------------------
//            文件：AddCatalogCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 15:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.Commands
{
    sealed class AddCatalogCommand(CatalogViewModelBase target) : ContextCommand<CatalogViewModelBase>(target)
    {
        public override async void Execute(object parameter)
        {
            if (!Context.Options.AllowCatalogOperation)
            {
                Context.Warning("错误", "无法修改目录。");
                return;
            }

            var r = await Context.SingleLine("新建", "新建一个目录");

            if (!r.IsFinished)
            {
                return;
            }

            if (string.IsNullOrEmpty(r.Value))
            {
                Context.Warning("错误", "目录名不能为空。");
                return;
            }

            var catalog = new Catalog
            {
                Id      = ID.Get(),
                Name    = r.Value,
                Domain  = Context.Options.Domain,
                Subject = Context.Options.Subject,
            };

            await IndexSystem.CatalogService.Add(catalog);
            
            
            Context.Catalogs.Add(catalog);
            Context.Catalog = catalog;
        }
    }
}