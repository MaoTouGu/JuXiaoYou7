using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public sealed class SignUpCredentialViewModel : ObjectRoot<Credential>
    {
        private RemoteApi  _api;
        private Credential _credential;
        private string     _url;
        private string     _userName;
        private string     _displayName;
        private string     _password;
        private string     _repeatPwd;
        private string     _email;


        public SignUpCredentialViewModel(Project project)
        {
            Url    = project.Url;
            SignUp = new DelegateCommand(DoSignUpCommand, CanSignUp);
        }
        //-------------------------------------------------------------
        //
        //                  Private Methods
        //
        //-------------------------------------------------------------
        bool CanSignUp()
        {
            return CanNextStep()                    &&
                   !string.IsNullOrEmpty(Password)  &&
                   !string.IsNullOrEmpty(RepeatPwd) &&
                   User.IsEmail(Email)              &&
                   Password == RepeatPwd;
        }

        bool CanNextStep()
        {
            return !string.IsNullOrEmpty(Url)         &&
                   !string.IsNullOrEmpty(UserName)    &&
                   UserName.Length > 1                &&
                   UserName.Length <= 8               &&
                   UserName.All(char.IsLetterOrDigit) &&
                   !string.IsNullOrEmpty(DisplayName);
        }
        //-------------------------------------------------------------
        //
        //                  Private Methods
        //
        //-------------------------------------------------------------

        protected override void ReleaseUnmanagedResources()
        {
            _api?.Dispose();
        }

        protected override Credential OnFinish(bool edit)
        {
            return _credential;
        }
        //-------------------------------------------------------------
        //
        //                  Private Methods
        //
        //-------------------------------------------------------------
        private async void DoSignUpCommand()
        {

            try
            {
                //
                // 创建
                _api = new RemoteApi(Url);

                //
                //
                var r = await _api.SignUpAsync(DisplayName, Email, UserName, Password, false);

                if (!r.IsFinished)
                {
                    this.Warning("注册失败", r.Reason);
                    return;
                }

                _credential = new Credential
                {
                    Account  = UserName,
                    Password = Password,
                };

                // 跳转。
                this.Success("操作成功", "注册成功");
                Complete();
            }
            catch(Exception e)
            {
                this.Warning("注册失败", e.Message);
                Logger.Warn($"注册失败 = {e.Message}");
            }
        }


        public string Email
        {
            get => _email;
            set => TryFinishAndSetValue(ref _email, value);
        }

        public string RepeatPwd
        {
            get => _repeatPwd;
            set => TryFinishAndSetValue(ref _repeatPwd, value);
        }

        public string Password
        {
            get => _password;
            set => TryFinishAndSetValue(ref _password, value);
        }

        public string DisplayName
        {
            get => _displayName;
            set => TryFinishAndSetValue(ref _displayName, value);
        }

        public string UserName
        {
            get => _userName;
            set => TryFinishAndSetValue(ref _userName, value);
        }

        public string Url
        {
            get => _url;
            set => TryFinishAndSetValue(ref _url, value);
        }



        public ICommandEX SignUp { get; }
    }
}