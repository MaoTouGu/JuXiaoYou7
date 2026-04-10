// ----------------------------------------------------------
//            文件：IndexingNavigator.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 20:24
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class IndexingNavigator : IExternalToolsNavigator
    {
        public async Task Navigate(IAppModel model, string name, string payload)
        {
            try
            {
                var options = JSON2.FromBase64<IndexingOption>(payload);

                if (options is null)
                {
                    //
                    // 参数不完整，不允许导航。
                    var dialogHost = Xaml.FindVisualChild<DialogHost>(Application.Current.MainWindow, true);

                    dialogHost?.Notify(new Notification
                    {
                        Duration   = Math.Clamp(10, 5, 100),
                        Title      = "错误",
                        Content    = "参数不完整，不允许导航",
                        Background = "#a87200",
                        Color      = "#db9400",
                    });

                    return;
                }

                options.Name = name;

                PageBase vm = options.Type switch
                {
                    IndexingType.List => new ListViewModel(options),
                    _                 => null,
                };

                if (vm is null)
                {
                    return;
                }

                await model.Navigate(vm);

            }
            catch(Exception e)
            {

            }
        }
    }
}