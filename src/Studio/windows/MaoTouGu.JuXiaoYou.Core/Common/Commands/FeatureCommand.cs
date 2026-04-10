// ----------------------------------------------------------
//            文件：FeatureCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 14:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Common.Commands
{
    public sealed class FeatureCommand(JuXiaoYouPage target) : ContextCommand<InheritedFeature, JuXiaoYouPage>(target)
    {

        protected override async void Execute(InheritedFeature target)
        {
            await FeatureManager.Navigate(target.Name, target.FeatureID, target.Options);
        }
    }
}