// ----------------------------------------------------------
//            文件：SystemPage.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 17:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.AppModels
{
    public abstract class SystemPage : JuXiaoYouPage
    {
        protected SystemPage() : base(removable: true, singleton: true)
        {
            
        }
    }
}