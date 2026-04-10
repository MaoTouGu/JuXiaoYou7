// ----------------------------------------------------------
//            文件：FeatureWorkspace.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 13:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public class FeatureWorkspace : SpecificWorkspace
    {
        protected override void OnStart()
        {
            Items.Add(new PluginWorkspaceItem());
            Items.Add(new FeatureWorkspaceItem());
            Items.Add(new VisualManagerWorkspaceItem());
        }
    }
}