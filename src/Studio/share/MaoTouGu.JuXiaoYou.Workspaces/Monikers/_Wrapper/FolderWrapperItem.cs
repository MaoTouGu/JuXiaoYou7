// ----------------------------------------------------------
//            文件：FolderWrapperItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 16:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public sealed class FolderWrapperItem : MonikerWorkspaceContainer
    {
        public override void Initialize(Moniker x)
        {
            
        }

        public string Id       => Folder?.Id;
        public string ParentID => Folder?.Parent;
        
        public Folder Folder { get; init; }
    }
}