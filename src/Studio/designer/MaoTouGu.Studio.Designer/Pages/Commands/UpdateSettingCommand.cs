// ----------------------------------------------------------
//            文件：UpdateSettingCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 22:04
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class UpdateSettingCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        public override async void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(Context.Setting))
            {
                return;
            }

            var r = await Context.SingleLine("Add", string.Empty);

            if (!r.IsFinished)
            {
                return;
            }

            if (!Context.Moniker.Settings.TryAdd(Context.Setting, r.Value))
            {
                Context.Moniker.Settings[Context.Setting] = r.Value;

                Context.Setting = r.Value;
            }
        }
    }
}