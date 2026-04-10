// ----------------------------------------------------------
//            文件：IPushingEventHandler.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 18:16
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services
{
    public interface IPushingEventHandler
    {
        bool CanHandle(string eventID);

        Task Handle(string documentID, string handlerName, string eventID, DataOperation argsOperation);
    }
}