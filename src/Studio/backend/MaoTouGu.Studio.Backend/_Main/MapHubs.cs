// ----------------------------------------------------------
//            文件：MapHubs.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月23日 13:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Hubs;

namespace MaoTouGu.Studio
{

    partial class Program
    {
        static void MapHubs(WebApplication app)
        {
            app.MapHub<PushingHub>("hub/events");
            app.MapHub<IMHub>("hub/chat");
            // app.MapHub<PublicHub>("hub/channel");
        }
    }
}