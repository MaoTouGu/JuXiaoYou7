// ----------------------------------------------------------
//            文件：LabelWrapperItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 16:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class LabelWrapperItem : MonikerWorkspaceContainer
    {
        public string Id       => Label?.Id;
        public string ParentID => Label?.Parent;
        
        public Label Label { get; init; }
    }
}