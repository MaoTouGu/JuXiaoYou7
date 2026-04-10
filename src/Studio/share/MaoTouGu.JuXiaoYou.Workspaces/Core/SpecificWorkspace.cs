// ----------------------------------------------------------
//            文件：SpecificWorkspace.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 11:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Core
{
    public abstract class SpecificWorkspace : Lifetime
    {
        protected SpecificWorkspace()
        {
            Items = new ViewList<WorkspaceItem>();
        }
        
        public ViewList<WorkspaceItem> Items { get; }
    }
}