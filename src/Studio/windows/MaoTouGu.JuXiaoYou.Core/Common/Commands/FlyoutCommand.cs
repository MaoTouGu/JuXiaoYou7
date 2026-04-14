// ----------------------------------------------------------
//            文件：FlyoutCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 11:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Common.Commands
{
    public class FlyoutCommand<T>(ViewModelBase target) : _Command where T : FlyoutRoot
    {
        public override void Execute(object parameter)
        {
            var dialog = ClassStatic.CreateInstance<T>();

            if (target is PageBase page)
            {
                page.Flyout(dialog);
            }
            else if (target is DialogBase dialog2)
            {
                dialog2.Flyout(dialog);
            }
        }
    }
}