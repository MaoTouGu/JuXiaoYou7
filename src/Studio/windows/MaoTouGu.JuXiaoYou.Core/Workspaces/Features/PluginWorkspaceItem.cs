// ----------------------------------------------------------
//            文件：PluginWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 01:55
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public class PluginWorkspaceItem : WorkspaceItem
    {
        public PluginWorkspaceItem()
        {
            Items = FeatureManager.Manifests;
        }

        public ViewList<IPluginManifest> Items { get; }
    }
}