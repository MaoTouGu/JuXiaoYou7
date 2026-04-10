// ----------------------------------------------------------
//            文件：IDatabaseDumpService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 19:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Core
{
    public interface IDatabaseDumpService
    {
        /// <summary>
        /// 初始化加载数据。
        /// </summary>
        void Initialize();

        /// <summary>
        /// 备份。
        /// </summary>
        /// <param name="incrementMode"></param>
        Task Dump(bool incrementMode);

        /// <summary>
        /// 获得上次全量备份的时间。
        /// </summary>
        DateTime GetLastFullDumpTime();
        
        /// <summary>
        /// 获得上次增量备份的时间。
        /// </summary>
        DateTime GetLastIncrementDumpTime();
    }
}