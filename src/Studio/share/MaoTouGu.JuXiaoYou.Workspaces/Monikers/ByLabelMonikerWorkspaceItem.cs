// ----------------------------------------------------------
//            文件：ByLabelMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public class ByLabelMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
        public ByLabelMonikerWorkspaceItem()
        {
            Dictionary = new Dictionary<string, LabelWrapperItem>();
        }

        protected override void OnSetup()
        {
            foreach (var label in LabelService.Collection)
            {
                Dictionary.TryAdd(label.Id, new LabelWrapperItem { Label = label });
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
        
        public Dictionary<string ,LabelWrapperItem> Dictionary { get; }
    }
}