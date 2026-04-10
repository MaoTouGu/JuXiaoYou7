// ----------------------------------------------------------
//            文件：IVisualManager.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 19:35
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Objects;
using MaoTouGu.Studio.Database.References;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public interface IVisualManager
    {
        /// <summary>
        /// 初始化目录。
        /// </summary>
        /// <returns></returns>
        void InitializeCatalogs(Action<Folder> folderCreateExpr);

        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="context">视图模型。</param>
        /// <param name="target">目标。</param>
        /// <param name="command">命令。</param>
        void Execute(PageBase context, Moniker target, PseudoCommandItem command);

        /// <summary>
        /// 获得指定的Visualization。
        /// </summary>
        /// <param name="id">指定的id。</param>
        /// <param name="domain">指定的domain。</param>
        /// <param name="subject">指定的subject。</param>
        /// <returns>返回一个Visualization。</returns>
        Visualization GetVisualization(string id, string domain, string subject);


        /// <summary>
        /// 打开指定的Visualization编辑器页面。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="visualization"></param>
        /// <param name="domain"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        Task Open(Moniker target, Visualization visualization, string domain, string subject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="catalogName"></param>
        /// <param name="domain"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        Task<Visualization> Create(Moniker target, string catalogName, string domain, string subject);

        Task Remove(Moniker target, string domain, string subject);

        Task Update(Moniker target, string domain, string subject);
    }
}