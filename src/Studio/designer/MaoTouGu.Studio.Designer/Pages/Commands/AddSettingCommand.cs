// ----------------------------------------------------------
//            文件：AddSettingCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 22:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddSettingCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        public override async void Execute(object parameter)
        {
            var r = await Context.SingleLine("添加喵喵咒语", string.Empty);

            if (!r.IsFinished)
            {
                return;
            }

            if (Context.Moniker.Settings.TryAdd(r.Value, string.Empty))
            {
                Context.Setting = r.Value;
            }

            var r2 = await Context.SingleLine("填写数值", string.Empty);

            if (!r2.IsFinished)
            {
                return;
            }

            Context.Moniker.Settings[r.Value] = r2.Value;
        }
    }
}