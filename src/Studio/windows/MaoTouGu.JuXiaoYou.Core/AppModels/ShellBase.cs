// ----------------------------------------------------------
//            文件：ShellBase.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows.Shell;
using MaoTouGu.JuXiaoYou.Pages;
using MaoTouGu.JuXiaoYou.Services;
using MaoTouGu.JuXiaoYou.Services.Imaging;
using MaoTouGu.JuXiaoYou.Services.Imaging.Caching;
using MaoTouGu.Studio.Database;

namespace MaoTouGu.JuXiaoYou.AppModels
{

    public abstract partial class ShellBase<TMainWindow, THostWindow> : MultipleWindowModel<TMainWindow, THostWindow>, IShellBase, IViewModelProvider
        where TMainWindow : MTGWindow, new()
        where THostWindow : MTGWindow, new()
    {
        public const string URI_Gravatar = "pack://application:,,,/MaoTouGu.JuXiaoYou.Core;component/Assets/gravatar.png";
        public const string URI_Icon     = "pack://application:,,,/MaoTouGu.JuXiaoYou.Core;component/Assets/gravatar.png";
        public const string URI_Image    = "pack://application:,,,/MaoTouGu.JuXiaoYou.Core;component/Assets/bg_7.png";

        IEnumerable<ViewModelBase> IViewModelProvider.GetContextList() => InstanceTable.Values
                                                                                       .Select(x => x.ViewModel);


        protected void ClosePlaceholdingView()
        {

            if (InstanceTable.Values
                             .FirstOrDefault(x => x.ViewModel is PlaceholdingViewModel) is
                {
                    ViewModel: PlaceholdingViewModel landing
                })
            {
                landing.Stop();
            }
        }

        protected Task InitializeGlobalServices()
        {
            // await DatabaseManager.GetService<MonikerService>().Start();
            // await DatabaseManager.GetService<FolderService>().Start();
            // await DatabaseManager.GetService<LabelService>().Start();
            // await DatabaseManager.GetService<UniqueReferenceService>().Start();
            // await DatabaseManager.GetService<ReferenceService>().Start();
            // await DatabaseManager.GetService<KeywordService>().Start();
            // await DatabaseManager.GetService<FilterService>().Start();
            return Task.CompletedTask;
        }

        public void Startup()
        {
            //
            // Initialize
            ImageSystem.Gravatar = new BitmapImage(new Uri(URI_Gravatar));
            ImageSystem.Icon     = new BitmapImage(new Uri(URI_Icon));
            ImageSystem.Image    = new BitmapImage(new Uri(URI_Image));

            //
            //
            if (GlobalSettings.OnlineMode)
            {
                ImageSystem.RootPath = GlobalSettings.CacheDir;
                Ioc.Use<
                    IWebApi,
                    IImageDownloadService,
                    IDataApiContract,
                    IUserApiContract,
                    IResourceLockApiContract,
                    RemoteApi>((RemoteApi)GlobalSettings.Api);
            }
            else
            {
                ImageSystem.RootPath = DirectoryExt.Combine(GlobalSettings.Url);


                Ioc.Use<
                    IWebApi,
                    IDataApiContract,
                    IUserApiContract,
                    IResourceLockApiContract,
                    LocalApi>((LocalApi)GlobalSettings.Api);
            }

            //
            // Register WebApi
            Ioc.Use<IImageCacheService, ImageCacheService>(new ImageCacheService()).Start();
            Ioc.Use<IPushingBackgroundService, PushingBackgroundService>(new PushingBackgroundService()).Start();
            Ioc.Use<IUserService, UserService>(new UserService()).Start();
            Ioc.Use<ResourceLockRefreshService>(new ResourceLockRefreshService()).Start();

            //
            //
            OnStartup();
        }

        protected virtual void OnStartup()
        {
        }


        #region JumpTask

        public static JumpTask CreateNavigateToSettingTask()
        {
            var setting = Ioc.Get<IAppConfig>().DirOfSettings;

            return new JumpTask
            {
                Title            = "设置目录",
                Description      = "打开应用存放设置的目录",
                ApplicationPath  = "explorer.exe",
                Arguments        = setting,
                IconResourcePath = "explorer.exe",
            };
        }

        public static JumpTask CreateNavigateToLogsTask()
        {
            var setting = Ioc.Get<IAppConfig>().DirOfLogs;

            return new JumpTask
            {
                Title            = "日志目录",
                Description      = "打开应用存放日志的目录",
                ApplicationPath  = "explorer.exe",
                Arguments        = setting,
                IconResourcePath = "explorer.exe",
            };
        }

        #endregion
    }
}