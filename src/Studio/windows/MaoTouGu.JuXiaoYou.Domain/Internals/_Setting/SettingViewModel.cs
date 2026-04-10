
using MaoTouGu.Shells.Inputs;

namespace MaoTouGu.JuXiaoYou.Internals
{
    [Obsolete("将使用*.Core项目中的SettingViewModel。")]
    public sealed class SettingViewModel : JuXiaoYouPage
    {
        private readonly AppSettings _Setting;

        public SettingViewModel()
        {
            _Setting = Ioc.Get<AppSettings>();

            LocateIdentifiers = new ViewList<LocateIdentifier>();

            PickCacheDir    = new DelegateCommand(DoPickCacheDirCommand);
            PickDownloadDir = new DelegateCommand(DoPickDownloadDirCommand);
        }

        protected override void OnStart()
        {
            LocateIdentifiers.Add(new LocateIdentifier { DisplayText = "简体中文", LCID    = "zh-CN" });
            LocateIdentifiers.Add(new LocateIdentifier { DisplayText = "繁体中文", LCID    = "zh-SC" });
            LocateIdentifiers.Add(new LocateIdentifier { DisplayText = "English", LCID = "en-US" });
        }

        protected override void OnPropertyChanged(string name, object value)
        {
            //Task.Run(() => _Setting.Save());
            this.SaveSuccess();
        }

        //-------------------------------------------------------------
        //
        //                  Command Handlers
        //
        //-------------------------------------------------------------
        private void DoPickCacheDirCommand()
        {
            var r = Interop.OpenFolderBrowserAsync();

            if (!r.IsFinished)
            {
                return;
            }

            CacheDir = r.Value;
        }

        private void DoPickDownloadDirCommand()
        {
            var r = Interop.OpenFolderBrowserAsync();

            if (!r.IsFinished)
            {
                return;
            }

            DownloadDir = r.Value;
        }

        //------------------------------------------------------------
        //
        //                    Nested Class
        //
        //------------------------------------------------------------
        public ICommandEX PickCacheDir    { get; }
        public ICommandEX PickDownloadDir { get; }

        //------------------------------------------------------------
        //
        //                    Nested Class
        //
        //------------------------------------------------------------
        public AppTheme Theme
        {
            get => (AppTheme)_Setting.Theme;
            set
            {
                _Setting.Theme = (int)value;
                OnPropertyChanged();
            }
        }   
        
        public StartupRouting StartupRouting
        {
            get =>_Setting.StartupRouting;
            set
            {
                _Setting.StartupRouting = value;
                OnPropertyChanged();
            }
        }

        public string Url
        {
            get => _Setting.Url;
            set
            {
                _Setting.Url = value;
                OnPropertyChanged();
            }
        }

        public string LCID
        {
            get => _Setting.LCID;
            set
            {
                _Setting.LCID = value;
                OnPropertyChanged();
            }
        }

        public string CacheDir
        {
            get => _Setting.CacheDir;
            set
            {
                _Setting.CacheDir = value;
                OnPropertyChanged();
            }
        }

        public string DownloadDir
        {
            get => _Setting.DownloadDir;
            set
            {
                _Setting.DownloadDir = value;
                OnPropertyChanged();
            }
        }

        public ViewList<LocateIdentifier> LocateIdentifiers { get; }
    }
}