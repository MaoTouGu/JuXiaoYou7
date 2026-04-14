// ----------------------------------------------------------
//            文件：AddFavoriteCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 21:29
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    sealed class AddFavoriteCommand(JuXiaoYouPage target) : ContextCommand<Moniker, JuXiaoYouPage>(target)
    {

        protected override async void Execute(Moniker target)
        {
            target.IsStar = !target.IsStar;

            var service = DatabaseManager.GetService<MonikerService>();

            await service.Update(target);
        }
    }
}