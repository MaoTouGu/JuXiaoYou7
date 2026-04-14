// ----------------------------------------------------------
//            文件：TrimmedMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public sealed class TrimmedMonikerWorkspaceItem  : MonikerWorkspaceContainer
    {
        private int _count;
        

        
        /// <summary>
        /// 展示的内容，懒得写Binding了。
        /// </summary>
        public string Text => $"星标设定（{_count}）";

        /// <summary>
        /// 设定的数量。
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                SetValue(ref _count, value);
                RaiseUpdated(nameof(Count));
                RaiseUpdated(nameof(Text));
            }
        }
        
        public string Domain { get; init; }
        public string Subject { get; init; }
        
        public override string ToString() => "展示全部";
    }
}