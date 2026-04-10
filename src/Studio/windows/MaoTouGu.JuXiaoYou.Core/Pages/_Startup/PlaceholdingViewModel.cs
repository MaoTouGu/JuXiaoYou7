// ----------------------------------------------------------
//            文件：PlaceholdingViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 17:53
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    public class PlaceholdingViewModel : JuXiaoYouPage, IHostedWindowNavigation
    {
        public PlaceholdingViewModel() : base(false, true)
        {
            //
            // PlaceholdingViewModel。
            Title = "应用加载中……";
        }
    }
}