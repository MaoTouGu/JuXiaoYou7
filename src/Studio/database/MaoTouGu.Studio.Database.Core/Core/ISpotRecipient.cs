// ----------------------------------------------------------
//            文件：ISpotRecipient.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.Studio.Database.Core
{
    public interface ISpotRecipient
    {
        Task WhenDataChanged(Spot dataEvent);
    }
}