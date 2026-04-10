// ----------------------------------------------------------
//            文件：OpenMonikerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 20:49
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.Commands
{
    sealed class OpenMonikerCommand(MonikerViewModelBase target) : ContextCommand<Moniker, MonikerViewModelBase>(target)
    {

        protected override async void Execute(Moniker target)
        {

            var options       = Context.Options;
            var visualManager = Context.VisualManager;

            if (visualManager is null)
            {
                return;
            }

            var visualization = visualManager.GetVisualization(target.Id, options.Domain, options.Subject);

            //
            //
            visualization ??= await visualManager.Create(
                                                         target,
                                                         Context.CatalogName,
                                                         options.Domain,
                                                         options.Subject);

            //
            //
            await Context.VisualManager.Open(target, visualization, options.Domain, options.Subject);
        }
    }
}