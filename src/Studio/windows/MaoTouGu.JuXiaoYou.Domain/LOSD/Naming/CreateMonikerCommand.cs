// ----------------------------------------------------------
//            文件：CreateMonikerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 00:56
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using MaoTouGu.Studio.Database.Objects;

namespace MaoTouGu.JuXiaoYou.LOSD.Naming
{
    public sealed class CreateMonikerCommand(NamingLevelSettingDetailsViewModel target) : ContextCommand<NamingLevelSettingDetailsViewModel>(target)
    {
        public override async void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(Context.Text))
            {
                Context.Warning("错误", "名字为空。");
                return;
            }

            var time    = DateTime.Now;
            var moniker = Moniker.Create(Context.Text, GlobalSettings.User);

            try
            {
                await Context.MonikerService.Add(moniker);
                Context.Text = null;
            }
            catch
            {
            }
        }
    }
}