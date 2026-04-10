// ----------------------------------------------------------
//            文件：ByIndexerMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using NetTopologySuite.Index.HPRtree;

namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public class ByIndexerMonikerWorkspaceItem : MonikerWorkspaceItem
    {
        public ByIndexerMonikerWorkspaceItem(ViewList<WorkspaceItem> collection)
        {
            Items = collection;
        }
        
        public ViewList<WorkspaceItem> Items { get; }
    }
}