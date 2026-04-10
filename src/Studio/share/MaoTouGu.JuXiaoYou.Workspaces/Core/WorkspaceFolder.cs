// ----------------------------------------------------------
//            文件：WorkspaceFolder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core
{
    public abstract class WorkspaceFolder : WorkspaceItem
    {
        private string _name;
        
        protected WorkspaceFolder()
        {
            Items = new ViewList<WorkspaceItem>();
        }


        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }
        
        public ViewList<WorkspaceItem> Items { get; }
    }
}