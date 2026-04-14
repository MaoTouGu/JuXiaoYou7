// ----------------------------------------------------------
//            文件：MonikerWrapperItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class MonikerWrapperItem : MonikerWorkspaceItem
    {

        public string Id => Moniker.Id;

        public Moniker Moniker { get; init; }
    }
}