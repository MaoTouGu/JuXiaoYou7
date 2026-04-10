// ----------------------------------------------------------
//            文件：VisualManagerBase`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 13:42
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class VisualManagerBase<TVisual> : VisualManagerBase where TVisual : Visualization
    {
        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="context">VM</param>
        /// <param name="target">设定</param>
        /// <param name="command">命令</param>
        public override void Execute(PageBase context, Moniker target, PseudoCommandItem command)
        {
            if (target is null)
            {
                return;
            }
            
            var visual = GetVisualization(target);

            if (visual is null)
            {
                return;
            }
            
            Execute(context, visual, command);
        }

        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="context">VM</param>
        /// <param name="target">视觉设定</param>
        /// <param name="command">命令</param>
        protected abstract void Execute(PageBase context, TVisual target, PseudoCommandItem command);
        
        protected string GetVisualizationKey(Moniker target)
        {
            if (!target.Settings.TryGetValue(VisualKey, out var setting) ||
                !Guid.TryParse(setting, out _))
            {
                return null;
            }


            return setting;
        }

        protected TVisual GetVisualization(Moniker target)
        {
            if (!target.Settings.TryGetValue(VisualKey, out var setting) ||
                !Guid.TryParse(setting, out _))
            {
                return null;
            }


            return GetVisualization(setting, Domain, Subject) as TVisual;
        }

        /// <summary>
        /// 创建指定视觉元素。
        /// </summary>
        /// <param name="id">视觉设定的ID。</param>
        /// <param name="catalogName">选择的目录名。</param>
        /// <param name="name">视觉设定的名字。</param>
        /// <param name="gravatar">视觉设定的头像。</param>
        /// <returns>返回新创建指定视觉元素对象实例。</returns>
        protected abstract TVisual CreateVisualization(string id, string catalogName, string name, string gravatar);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="visualization"></param>
        protected abstract void UpdateVisualization(Moniker target, TVisual visualization);
        
        /// <summary>
        /// 
        /// </summary>
        protected override string Subject => typeof(TVisual).Name;
    }
}