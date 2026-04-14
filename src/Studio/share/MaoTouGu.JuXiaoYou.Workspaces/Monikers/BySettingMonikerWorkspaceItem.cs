// ----------------------------------------------------------
//            文件：BySettingMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 14:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Visualizers.Pages;

namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class BySettingMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
        protected override void OnSetup()
        {
            Items.AddMany(FilterService.Collection
                                       .Select(x => new BySettingFilterMethodItem(x)), true);
        }
    }


    public sealed class BySettingFilterMethodItem(CustomFilter _filter) : MonikerWorkspaceItem
    {
        public CustomFilter Filter => _filter;
    }
}