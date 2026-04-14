// ----------------------------------------------------------
//            文件：MonikerWorkspaceContainer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public abstract class MonikerWorkspaceContainer : MonikerWorkspaceItem
    {
        protected MonikerWorkspaceContainer()
        {
            Items = new ViewList<MonikerWorkspaceItem>();
        }

        protected void AddOrTrimmed(Moniker x)
        {
            if (Items.Count > 18)
            {
                if (Items.FirstOrDefault(w => w is TrimmedMonikerWorkspaceItem) is TrimmedMonikerWorkspaceItem trimmed)
                {
                    trimmed.Count += 1;
                }
                else
                {
                    trimmed = new TrimmedMonikerWorkspaceItem
                    {
                        Count = Items.Count,
                    };
                    
                    Items.Add(trimmed);
                }
            }
            else
            {
                Items.Add(new MonikerWrapperItem{ Moniker = x});
            }
        }
        
        protected internal void RemoveOrTrimmed(Moniker x)
        {
            var index  = Items.IndexOf(y => y is TrimmedMonikerWorkspaceItem);
            var index2 = Items.IndexOf(y => y is MonikerWrapperItem i && i.Id == x.Id);
            
            if (index >= 0)
            {
                var trimmed = (TrimmedMonikerWorkspaceItem)Items[index];
                
                trimmed.Count -= 1;

                if (trimmed.Count == 0)
                {
                    Items.RemoveAt(index);
                }
            }

            if (index2 >= 0)
            {
                Items.RemoveAt(index2);
            }
        }

        public ViewList<MonikerWorkspaceItem> Items { get; }
    }
}