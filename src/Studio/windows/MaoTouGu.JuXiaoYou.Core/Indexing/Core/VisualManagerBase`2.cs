// ----------------------------------------------------------
//            文件：VisualManagerBase`2.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 13:43
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class VisualManagerBase<TVisual, TService> : VisualManagerBase<TVisual> where TVisual : Visualization
                                                                                            where TService : DataService<TVisual>
    {
        protected VisualManagerBase()
        {
            Service = DatabaseManager.GetService<TService>();
        }

        /// <summary>
        /// 获得指定的视觉设定。
        /// </summary>
        /// <param name="id">视觉设定的ID。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        /// <returns>返回指定的视觉设定，可能为null。</returns>
        public override Visualization GetVisualization(string id, string domain, string subject) => Service.Get(id);

        /// <summary>
        /// 创建指定视觉元素。
        /// </summary>
        /// <param name="target">关联的设定。</param>
        /// <param name="catalogName">选择的目录名。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        public override async Task<Visualization> Create(Moniker target, string catalogName, string domain, string subject)
        { 
            TVisual visualization;

            if (target.Settings.TryGetValue(VisualKey, out var setting))
            {
                //
                // 如果设定的设定项中存在，则判定是否正确。
                if (!Guid.TryParse(setting, out _) || GetVisualization(setting, domain, subject) is not TVisual visual)
                {

                    //
                    // 不存在则创建。
                    visualization = await NewVisual(catalogName);

                    //
                    //
                    target.Settings.Add(VisualKey, visualization.Id);
                }
                else
                {
                    return visual;
                }


            }
            else
            {
                visualization = await NewVisual(catalogName);
            }

            return visualization;

            async Task<TVisual> NewVisual(string name)
            {
                var id     = ID.Get();
                var visual = CreateVisualization(id, name, target.Name, target.Gravatar);

                //
                // 编写设定。
                target.Settings.Add(VisualKey, id);

                //
                // 添加
                await Service.Add(visual);

                return visual;
            }

        }

        /// <summary>
        /// 更新视觉元素。
        /// </summary>
        /// <param name="target">关联的设定。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        public override async Task Update(Moniker target, string domain, string subject)
        {
            if (GetVisualization(target) is not {} visualization)
            {
                return;
            }

            UpdateVisualization(target, visualization);
            await Service.Update(visualization);
        }

        /// <summary>
        /// 删除指定视觉元素。
        /// </summary>
        /// <param name="target">关联的设定。</param>
        /// <param name="domain">世界或Domain。</param>
        /// <param name="subject">子世界或Subject。</param>
        public override async Task Remove(Moniker target, string domain, string subject)
        {
            if (target is null)
            {
                Logger.Debug($"删除{typeof(TVisual).Name}的视觉设定时因为给定的Moniker（设定）为空，提前结束操作。");
                return;
            }

            if (string.IsNullOrEmpty(VisualKey))
            {
                Logger.Debug($"删除{typeof(TVisual).Name}的视觉设定时因为VisualFeature配置为空，提前结束操作。");
                return;
            }

            if (!target.Settings.TryGetValue(VisualKey, out var setting) || !Guid.TryParse(setting, out _))
            {
                Logger.Debug($"删除{typeof(TVisual).Name}的视觉设定时因为无法找到对应的对象，提前结束操作。");
                return;
            }

            if (GetVisualization(setting, domain, subject) is not TVisual visualization)
            {

                Logger.Debug($"删除{typeof(TVisual).Name}的视觉设定为空，提前结束操作。");
                return;
            }

            await Service.Remove(visualization);

            //
            // 删除，但是不提交更新。
            target.Settings.Remove(VisualKey);
        }

        protected TService Service { get; }
    }
}