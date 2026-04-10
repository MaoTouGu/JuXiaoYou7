// ----------------------------------------------------------
//            文件：NewProjectViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 02:10
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class NewProjectViewModel : ObjectRoot<Project>
    {
        private readonly Project _target;

        private bool _isOnline;
        
        public NewProjectViewModel()
        {
            _target = new Project
            {
                Id          = ID.Get(),
                Credentials = new ViewList<Credential>(),
            };

            Pick = new DelegateCommand(DoPick);
        }

        public NewProjectViewModel(Project target)
        {
            _target   = target;
            _isOnline = target.IsOnline;
            Url       = target.Url;
            Name      = target.Name;

            Pick = new DelegateCommand(DoPick);
        }

        private void DoPick()
        {
            var r = Interop.OpenFolderBrowserAsync();

            if (!r.IsFinished)
            {
                return;
            }

            Url = r.Value;
        }


        protected override bool CanFinish() => !string.IsNullOrEmpty(Name) &&
                                               !string.IsNullOrEmpty(Url);

        protected override Project OnFinish(bool edit)
        {
            _target.IsOnline = _isOnline;
            return _target;
        }

        public ICommandEX Pick { get; }

        public string Name
        {
            get => _target.Name;
            set
            {
                TryFinish();
                RaiseUpdated();
                _target.Name = value;
            }
        }

        public string Url
        {
            get => _target.Url;
            set
            {
                TryFinish();
                RaiseUpdated();
                _target.Url = value;
            }
        }

        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                TryFinishAndSetValue(ref _isOnline, value);
            }
        }
    }
}