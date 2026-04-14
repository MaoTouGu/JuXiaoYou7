// ----------------------------------------------------------
//            文件：NewCredentialViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 02:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    public class NewCredentialViewModel : ObjectRoot<Credential>
    {
        private string _account;
        private bool   _isDefault;
        private string _password;

        public NewCredentialViewModel()
        {

        }

        public NewCredentialViewModel(Credential credential)
        {
            IsEditing = true;
            Account   = credential.Account;
            Result    = Result<Credential>.Success(credential);
        }
        //-------------------------------------------------------------
        //
        //                  Private Methods
        //
        //-------------------------------------------------------------

        protected override bool CanFinish() => !string.IsNullOrEmpty(Account) &&
                                               !string.IsNullOrEmpty(Password);

        protected override void OnStart()
        {
            if (IsEditing)
            {
                Account   = Result.Value.Account;
                Password  = Result.Value.Password;
                IsDefault = Result.Value.IsDefault;
            }
        }

        protected override Credential OnFinish(bool edit)
        {
            if (edit)
            {
                var val = Result.Value;

                val.Account   = Account;
                val.Password  = Password;
                val.IsDefault = IsDefault;

                return val;
            }

            return new Credential
            {
                Account   = Account,
                Password  = Password,
                IsDefault = IsDefault,
            };
        }

        //-------------------------------------------------------------
        //
        //                  Private Methods
        //
        //-------------------------------------------------------------

        public bool IsDefault
        {
            get => _isDefault;
            set => TryFinishAndSetValue(ref _isDefault, value);
        }
        public string Account
        {
            get => _account;
            set => TryFinishAndSetValue(ref _account, value);
        }


        public string Password
        {
            get => _password;
            set => TryFinishAndSetValue(ref _password, value);
        }
    }
}