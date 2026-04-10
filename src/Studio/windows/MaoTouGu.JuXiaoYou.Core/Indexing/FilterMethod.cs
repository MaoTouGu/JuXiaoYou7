// ----------------------------------------------------------
//            文件：FilterMethod.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 01:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class FilterMethod
    {
        public abstract Task Filter(List<Moniker> originalSource, IList<Moniker> collection);
    }
}