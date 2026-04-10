// ----------------------------------------------------------
//            文件：TopClassService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 11:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.References;

namespace MaoTouGu.Studio.References
{
    public class TopClassService : AsyncCollectionService<TopClass>
    {
        public TopClassService() : base(EngineNames.Reference, CollectionNames.TopClass)
        {
            
        }
    }
}