using System.Reflection;

namespace MaoTouGu.Shells.AppConfigs
{
    public interface IViewConfigBuilder
    {
        /// <summary>
        /// 自动发现所有Views
        /// </summary>
        /// <returns></returns>
        IAppConfigBuilder UseViews(params Assembly[] assemblies);

        IAppConfigBuilder UseViews(params IViewBundleStateProvider[] providers);
        
        
    }
}