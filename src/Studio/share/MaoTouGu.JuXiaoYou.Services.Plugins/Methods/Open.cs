// ----------------------------------------------------------
//            文件：Open.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 18:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public static partial class FeatureManager
    {
        static async Task NavigateByExternalNavigator(IAppModel appModel, string name, Feature feature, string options)
        {
            //
            // 判断是否存在。
            if (feature.ExternalNavigator is null)
            {
                appModel.Notify(new Notification
                {

                    Duration   = 10,
                    Title      = "警告",
                    Content    = "设置了UseExternalNavigator属性但是没有对应的ExternalNavigator。",
                    Background = "#a87200",
                    Color      = "#db9400",
                });
                return;
            }

            //
            // 判断类型是否正确
            if (!feature.ExternalNavigator.IsAssignableTo(typeof(IExternalToolsNavigator)))
            {
                appModel.Notify(new Notification
                {

                    Duration   = 10,
                    Title      = "警告",
                    Content    = "设置了ExternalNavigator属性但是该类型不实现IExternalToolsNavigator接口。",
                    Background = "#a87200",
                    Color      = "#db9400",
                });
                return;
            }

            //
            // 创建IExternalToolsNavigator。
            var navigator = (IExternalToolsNavigator)Activator.CreateInstance(feature.ExternalNavigator);

            //
            // 判空。
            if (navigator is null)
            {
                appModel.Notify(new Notification
                {

                    Duration   = 10,
                    Title      = "警告",
                    Content    = "无法创建ExternalNavigator实例。",
                    Background = "#a87200",
                    Color      = "#db9400",
                });
                return;
            }

            //
            // 导航。
            await navigator.Navigate(appModel, name, options);
        }
        
        /// <summary>
        /// 导航到指定的功能。
        /// </summary>
        /// <param name="name">入口所传递的名字。</param>
        /// <param name="featureID">功能的ID。</param>
        /// <param name="options">功能的选项，可为空。</param>
        public static async Task Navigate(string name, string featureID, string options)
        {
            //
            // 寻找此Feature是否存在。
            if (Features.FirstOrDefault(x => x.Id == featureID) is not {} feature)
            {
                //
                // 提示此功能不存在，可能是本机未安装对应的插件或者功能ID错误。
                return;
            }

            var appModel = Ioc.Get<IAppModel>();

            try
            {
                if (feature.UseExternalNavigator)
                {
                    await NavigateByExternalNavigator(appModel, name, feature, options);
                }
                else
                {
                    var page = (PageBase)Activator.CreateInstance(feature.Type);

                    if (page is null)
                    {
                        return;
                    }

                    await appModel.Navigate(page, options);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}