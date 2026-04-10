// ----------------------------------------------------------
//            文件：FilterService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 20:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.References
{
    public class FilterService : AsyncCollectionService<CustomFilter>
    {
        public FilterService() : base(EngineNames.Reference, CollectionNames.Filter)
        {
            
        }
    }
}