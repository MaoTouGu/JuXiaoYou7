// ----------------------------------------------------------
//            文件：MonikerTransitViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 20:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public class MonikerTransitViewModel : NestedPage
    {
        public MonikerTransitViewModel(Moniker item, JuXiaoYouPage parent) : base(item, parent)
        {
            Moniker = item;
        }
        
        public Moniker Moniker { get; }
    }
}