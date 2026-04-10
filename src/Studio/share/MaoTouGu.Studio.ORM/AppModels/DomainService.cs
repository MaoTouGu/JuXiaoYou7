// ----------------------------------------------------------
//            文件：DomainService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 23:19
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.AppModels;

namespace MaoTouGu.Studio.AppModels
{
    public class DomainService : AsyncCollectionService<Domain>
    {
        public DomainService() : base(EngineNames.System, CollectionNames.Domain)
        {
        }
    }
}