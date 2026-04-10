// ----------------------------------------------------------
//            文件：UserIDProvider.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月31日 23:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Hubs
{
    public class UserIDProvider : IUserIdProvider
    {

        public string GetUserId(HubConnectionContext connection)
        {
            var context = connection.GetHttpContext();
            var id      = context?.Request.Query["userId"];
            return id;
        }
    }
}