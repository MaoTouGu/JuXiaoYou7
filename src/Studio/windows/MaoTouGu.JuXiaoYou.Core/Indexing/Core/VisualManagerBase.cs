// ----------------------------------------------------------
//            文件：VisualManagerBase.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 13:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using NLog;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class VisualManagerBase : IVisualManager
    {
        protected VisualManagerBase()
        {
            Logger = LoggerExt.GetLogger(GetType().Name);
        }

        /// <summary>
        /// 初始化目录。
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<Catalog> InitializeCatalogs();

        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="context">VM</param>
        /// <param name="target">设定</param>
        /// <param name="command">命令</param>
        public abstract void Execute(PageBase context, Moniker target, PseudoCommandItem command);

        /// <summary>
        /// 获得指定的视觉设定。
        /// </summary>
        /// <param name="id">视觉设定的ID。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        /// <returns>返回指定的视觉设定，可能为null。</returns>
        public abstract Visualization GetVisualization(string id, string domain, string subject);

        /// <summary>
        /// 获得指定的视觉设定。
        /// </summary>
        /// <param name="target">设定。</param>
        /// <param name="visualization">视觉设定。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        /// <returns>返回指定的视觉设定，可能为null。</returns>
        public abstract Task Open(Moniker target, Visualization visualization, string domain, string subject);


        /// <summary>
        /// 创建指定视觉元素。
        /// </summary>
        /// <param name="target">关联的设定。</param>
        /// <param name="catalogName">选择的目录名。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        public abstract Task<Visualization> Create(Moniker target, string catalogName, string domain, string subject);


        /// <summary>
        /// 删除指定视觉元素。
        /// </summary>
        /// <param name="target">关联的设定。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        public abstract Task Remove(Moniker target, string domain, string subject);

        /// <summary>
        /// 更新视觉元素。
        /// </summary>
        /// <param name="target">关联的设定。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        public abstract Task Update(Moniker target, string domain, string subject);

        protected ILogger Logger { get; }

        protected abstract string VisualKey { get; }

        protected abstract string Domain  { get; }
        protected abstract string Subject { get; }
    }
}