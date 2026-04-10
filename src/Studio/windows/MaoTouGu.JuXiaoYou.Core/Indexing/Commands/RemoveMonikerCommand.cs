// ----------------------------------------------------------
//            文件：RemoveMonikerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 20:50
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing.Commands
{
    sealed class RemoveMonikerCommand(MonikerViewModelBase target) : ContextCommand<Moniker, MonikerViewModelBase>(target)
    {

        protected override async void Execute(Moniker target)
        {
            if (!await Context.RemoveThis())
            {
                return;
            }


            
            if (Context.VisualManager is not null)
            {
                var options = Context.Options;
                await Context.VisualManager.Remove(target, options.Domain, options.Subject);
            }
            
            target.IsSoftDeleted = true;
            target.Modified      = DateTime.Now;

            //
            //
            await IndexSystem.MonikerService.Update(target);
            await IndexSystem.RemoveSubordinate(target);
            Context.RemoveInternal(target);
        }
    }
}