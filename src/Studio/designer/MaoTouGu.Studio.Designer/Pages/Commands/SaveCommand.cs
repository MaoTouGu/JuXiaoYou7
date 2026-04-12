// ----------------------------------------------------------
//            文件：SaveCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 13:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class SaveCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        public override void Execute(object parameter)
        {
        }
    }
}