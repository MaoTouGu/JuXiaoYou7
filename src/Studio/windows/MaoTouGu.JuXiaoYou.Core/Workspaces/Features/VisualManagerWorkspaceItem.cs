// ----------------------------------------------------------
//            文件：VisualManagerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 01:55
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public class VisualManagerWorkspaceItem : WorkspaceItem
    {
        public VisualManagerWorkspaceItem()
        {
            Items = new ViewList<IVisualizerGenerator>(FeatureManager.Visualizers.Values);
        }

        public ViewList<IVisualizerGenerator> Items { get; }

    }
}