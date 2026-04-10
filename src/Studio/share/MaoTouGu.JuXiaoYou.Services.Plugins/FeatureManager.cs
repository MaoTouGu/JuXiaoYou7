using MaoTouGu.Foundation.Collections;
using MaoTouGu.Foundation.Core;
using MaoTouGu.Shells;
using MaoTouGu.Shells.Core;
using NLog;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public static partial class FeatureManager
    {
        public const string Classifier = "E7D67EC275C94F979755CD8178F745DA";

        private static readonly ILogger _Logger;

        static FeatureManager()
        {
            Features       = new ViewList<Feature>();
            VisualManagers = new ViewList<Feature>();
            Manifests      = new ViewList<IPluginManifest>();
            _Logger        = LoggerExt.GetLogger(nameof(FeatureManager));
        }


        public static IVisualManager GetVisualManager(string id)
        {
            if (VisualManagers.FirstOrDefault(x => x.Id == id) is {} provider)
            {
                return Activator.CreateInstance(provider.Type) as IVisualManager;
            }

            return null;
        }

        public static void AsVisualManager<T>(string id, string name) where T : IVisualManager
        {
            //
            // 一般来说VisualProvider本身也是一个feature
            //
            // 但是

            var f = new Feature
            {
                Id   = id,
                Type = typeof(T),
                Name = name,
            };

            VisualManagers.Add(f);
        }

        public static void UsePage<T>(string id, string name) where T : PageBase
        {
            var point = new Feature
            {
                Id   = id,
                Name = name,
                Type = typeof(T),
            };

            Features.Add(point);
        }

        public static void UseExternalNavigation<T>(string id, string name) where T : class, IExternalToolsNavigator
        {
            var point = new Feature
            {
                Id                   = id,
                Name                 = name,
                UseExternalNavigator = true,
                ExternalNavigator    = typeof(T),
            };

            Features.Add(point);
        }
        public static ViewList<IPluginManifest> Manifests { get; }

        /// <summary>
        /// 获得所有Feature。
        /// </summary>
        /// <remarks>
        /// 能够被继承的页面，称之为Feature。
        /// </remarks>
        public static ViewList<Feature> Features { get; }

        /// <summary>
        /// 可以作为VisualManager的Feature。
        /// </summary>
        public static ViewList<Feature> VisualManagers { get; }
    }
}