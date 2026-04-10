// ----------------------------------------------------------
//            文件：WorldViewEditorViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 16:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.WorldView
{
    public class WorldViewEditorViewModel : NestedPage
    {
        public WorldViewEditorViewModel(TopClassWorkspaceItem item, JuXiaoYouPage parent) :this(item.Id, parent)
        {
            WorkspaceItem = item;
            Title = item.Instance.Name;
        }
        
        public WorldViewEditorViewModel(SubClassWorkspaceItem item, JuXiaoYouPage parent) : this(item.Id, parent)
        {
            WorkspaceItem = item;
            Title = item.Instance.Name;
        }

        public WorldViewEditorViewModel(string id, JuXiaoYouPage parent) : base(id, parent)
        {
            
        }
        
        public WorldViewWorkspaceItem WorkspaceItem { get; }
    }
}