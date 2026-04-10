// ----------------------------------------------------------
//            文件：SearchCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 11:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Behaviors;

namespace MaoTouGu.JuXiaoYou.Common.Commands
{
    public class SearchCommand(ViewModelBase target) : _Command
    {
        public override void Execute(object parameter)
        {
            // if (target is not ISearchSupport)
            // {
            //     return;
            // }
            //
            // var v      = Ioc.Get<IAppModel>().GetViewCache(target);
            // var window = Xaml.FindVisualParent<MTGWindow>(v as FrameworkElement);
            //
            // WindowBehavior.FlyoutObject(window, new SearchPanel
            // {
            //     DataContext = target,
            // });
        }
    }
}