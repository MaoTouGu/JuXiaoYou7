// ----------------------------------------------------------
//            文件：IMetadataSourceProvider.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 13:06
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Templates
{
    public interface IMetadataSourceProvider
    {
        IEnumerable<string> GetMetadataSources();
    }
}