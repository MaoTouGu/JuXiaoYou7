// ----------------------------------------------------------
//            文件：IDataSource.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 11:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    /// <summary>
    /// 作为 <see cref="IDataSource"/> 的数据服务必须在应用开始时注册。
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// 用一个GUID作为数据源。
        /// </summary>
        string DataSource { get; }
    }
}