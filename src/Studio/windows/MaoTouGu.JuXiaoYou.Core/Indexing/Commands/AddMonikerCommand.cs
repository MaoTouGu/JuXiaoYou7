// ----------------------------------------------------------
//            文件：AddMonikerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 20:49
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.Commands
{
    sealed class AddMonikerCommand(MonikerViewModelBase target) : ContextCommand<MonikerViewModelBase>(target)
    {
        public override async void Execute(object parameter)
        {

            if (!Context.CanAdding())
            {
                return;
            }

            var r = await Context.SingleLine("新建", "新建设定");

            if (!r.IsFinished)
            {
                return;
            }

            //
            //
            var usr           = Ioc.Get<IWebApi>()?.User;
            var moniker       = Moniker.Create(r.Value, usr);
            var options       = Context.Options;
            var visualManager = Context.VisualManager;

            try
            {
                if (visualManager is not null)
                {
                    //
                    // 添加时需要判断VisualManager是否提供了Enum。
                    await visualManager.Create(moniker, Context.CatalogName, options.Domain, options.Subject);
                }

                //
                // 添加Moniker
                await IndexSystem.MonikerService.Add(moniker);

                //
                // 添加关系。
                await Context.OnAdding(moniker);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}