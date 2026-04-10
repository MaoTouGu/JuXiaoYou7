// ----------------------------------------------------------
//            文件：IPrivateHub.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 21:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.Studio.Database.IM
{
    public interface IPrivateHub
    {
        Task SendC2CAsync(MSG msg);
    }
}