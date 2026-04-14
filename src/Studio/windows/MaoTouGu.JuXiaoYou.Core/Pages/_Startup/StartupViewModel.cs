// ----------------------------------------------------------
//            文件：StartupViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Pages
{
    public sealed class StartupViewModel : SystemPage, IHostedWindowNavigation
    {
        private Credential _credential;
        private Project    _project;

        public StartupViewModel()
        {

            Search    = new SearchCommand(this);
            Tutorials = new OpenLinkCommand { Url = "explorer.exe", Arguments = I18N.GetText("App.Tutorials") };
            Dotate    = new OpenLinkCommand { Url = "explorer.exe", Arguments = I18N.GetText("App.Dotate") };
            Settings  = new NavigationCommand<SettingViewModel>(this);

            AddProject    = new AddProjectCommand(this);
            CreateProject = new CreateProjectCommand(this);
            EditProject   = new EditProjectCommand(this);
            RemoveProject = new RemoveProjectCommand(this);
            ShareProject  = new ShareProjectCommand(this);
            OpenProject   = new OpenProjectCommand(this);

            AsDefaultProject = new AsDefaultProjectCommand(this);


            AddCredential    = new AddCredentialCommand(this);
            SignUpCredential = new SignUpCredentialCommand(this);
            EditCredential   = new EditCredentialCommand(this);
            RemoveCredential = new RemoveCredentialCommand(this);

            Projects    = new ViewList<Project>();
            Credentials = new ViewList<Credential>();

            Title = "起始页";
        }

        internal void UpdateDefaultProject()
        {
            //
            //
            Projects.AddMany(GlobalSettings.ProjectSettings.Projects, true);
        }

        protected override void OnStart()
        {
            //
            //
            UpdateDefaultProject();
        }


        public Credential Credential
        {
            get => _credential;
            set
            {
                SetValue(ref _credential, value);

                //
                //
                EditCredential?.RaiseUpdate();
                RemoveCredential?.RaiseUpdate();
            }
        }

        public Project Project
        {
            get => _project;
            set
            {
                SetValue(ref _project, value);

                //
                //
                if (_project is not null)
                {
                    Credentials.AddMany(_project.Credentials, true);
                }

                //
                //
                AddCredential?.RaiseUpdate();
                SignUpCredential?.RaiseUpdate();
                EditCredential?.RaiseUpdate();
                RemoveCredential?.RaiseUpdate();

                //
                //
                AsDefaultProject?.RaiseUpdate();
                SignUpCredential?.RaiseUpdate();
                EditProject?.RaiseUpdate();
                RemoveProject?.RaiseUpdate();
                ShareProject?.RaiseUpdate();
            }
        }

        public ViewList<Project>    Projects    { get; }
        public ViewList<Credential> Credentials { get; }

        public ICommandEX AddProject    { get; }
        public ICommandEX CreateProject { get; }
        public ICommandEX ShareProject  { get; }
        public ICommandEX OpenProject   { get; }
        public ICommandEX EditProject   { get; }
        public ICommandEX RemoveProject { get; }

        //public ICommandEX AsDefaultCredentials { get; }
        public ICommandEX AsDefaultProject { get; }

        public ICommandEX AddCredential    { get; }
        public ICommandEX SignUpCredential { get; }
        public ICommandEX EditCredential   { get; }
        public ICommandEX RemoveCredential { get; }

        public ICommandEX Tutorials { get; }
        public ICommandEX Dotate    { get; }
        public ICommandEX Search    { get; }
        public ICommandEX Settings  { get; }
    }
}