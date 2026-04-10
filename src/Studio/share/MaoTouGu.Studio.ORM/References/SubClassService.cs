// ----------------------------------------------------------
//            文件：SubClassService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 11:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.References;

namespace MaoTouGu.Studio.References
{
    public class SubClassService : AsyncCollectionService<SubClass>
    {
        
        public SubClassService() : base(EngineNames.Reference, CollectionNames.SubClass)
        {
            
        }
    }
}