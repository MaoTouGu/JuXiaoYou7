// ----------------------------------------------------------
//            文件：ByFolderMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:31
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public class ByFolderMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
        public ByFolderMonikerWorkspaceItem()
        {
            Dictionary = new Dictionary<string, FolderWrapperItem>();
            
        }

        protected override void OnSetup()
        {
            foreach (var folder in FolderService.Collection)
            {
                Dictionary.TryAdd(folder.Id, new FolderWrapperItem { Folder = folder });
            }

            foreach (var folder in Dictionary.Values)
            {
                if (string.IsNullOrEmpty(folder.ParentID))
                {
                    Items.Add(folder);
                }
                else
                {
                    if (Dictionary.TryGetValue(folder.ParentID, out var parent))
                    {
                        parent.Items.Add(folder);
                    }
                    else
                    {
                        Items.Add(folder);
                    }
                }
            }
        }
        
        public Dictionary<string ,FolderWrapperItem> Dictionary { get; }
    }
}