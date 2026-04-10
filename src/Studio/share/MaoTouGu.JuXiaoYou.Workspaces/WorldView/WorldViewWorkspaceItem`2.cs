// ----------------------------------------------------------
//            文件：WorldViewWorkspaceItem`2.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:56
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.WorldView
{
    public abstract class WorldViewWorkspaceItem<TInstance, TWrapper> : WorldViewWorkspaceItem
        where TInstance : DatabaseObject
        where TWrapper : WorldViewWorkspaceItem
    {
        protected WorldViewWorkspaceItem()
        {
            Children = new ViewList<TWrapper>();
        }
        
        private string _text;
        private int    _count;

        public string Id => Instance.Id;

        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }

        public string Text
        {
            get => _text;
            set => SetValue(ref _text, value);
        }

        public TInstance Instance { get; init; }

        public ViewList<TWrapper> Children { get; }
    }
}