namespace MaoTouGu.Shells.AppConfigs
{
    public class AppConfig : IAppConfig
    {
        private bool _isCompleted;

        void Verify()
        {
            if (_isCompleted)
            {
                throw new InvalidOperationException("无法在完成修改后，再次修改属性值。");
            }
        }

        internal void Finish()
        {
            Verify();
            _isCompleted = true;
        }

        public  bool   IsCompleted => _isCompleted;
        
        private string   _dirOfLogs;
        private string   _dirOfSettings;
        private string   _LCID;
        private object   _setting;
        private string   _settingFileName;
        private AppTheme _theme;

        private IReadOnlyList<ILanguageProvider> _languages;
        
        public string DirOfLogs
        {
            get => _dirOfLogs;
            set
            {
                Verify();
                _dirOfLogs = value;
            }
        }

        public string DirOfSettings
        {
            get => _dirOfSettings;
            set
            {
                Verify();
                _dirOfSettings = value;
            }
        }

        public string LCID
        {
            get => _LCID;
            set
            {
                Verify();
                _LCID = value;
            }
        }

        public object Setting
        {
            get => _setting;
            set
            {
                Verify();
                _setting = value;
            }
        } 
        
        /// <summary>
        /// 设置文件的位置。
        /// </summary>
        public string SettingFileName
        {
            get => _settingFileName;
            set
            {
                Verify();
                _settingFileName = value;
            }
        }

        public AppTheme Theme
        {
            get => _theme;
            set
            {
                Verify();
                _theme = value;
            }
        }


        public IReadOnlyList<ILanguageProvider> Languages
        {
            get => _languages;
            set
            {
                Verify();
                _languages = value;
            }
        }
    }
}