// ----------------------------------------------------------
//            文件：IDataService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 11:50
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.Studio.Database.Core
{
    public interface IDataService : IDisposable
    {
        Task Start();
        Task Stop();
        
        string DatabaseName   { get; }
        string CollectionName { get; }
        bool   IsStarted      { get; }
    }
}