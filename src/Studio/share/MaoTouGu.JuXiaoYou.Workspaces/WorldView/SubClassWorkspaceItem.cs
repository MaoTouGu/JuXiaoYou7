// ----------------------------------------------------------
//            文件：SubClassWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:52
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Workspaces.WorldView
{
    public sealed class SubClassWorkspaceItem : WorldViewWorkspaceItem<SubClass, SubClassWorkspaceItem>
    {
        public SubClassWorkspaceItem()
        {
        }

        public string ParentID => Instance.Parent;
    }
}