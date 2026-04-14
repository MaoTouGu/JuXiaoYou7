// ----------------------------------------------------------
//            文件：LauncherViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 02:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.IO;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class LauncherViewModel : JuXiaoYouPage, IHostedWindowNavigation
    {
        private TemplateProject _project;
        public LauncherViewModel() : base(false, true)
        {
            Projects = DesignSettings.Projects;

            Create = new DelegateCommand(DoCreateCommand);
            Load   = new DelegateCommand(DoLoadCommand);
            Open   = new DelegateCommand<TemplateProject>(DoOpenCommand, DBHelper.NotNull);
            Remove = new DelegateCommand<TemplateProject>(DoRemoveCommand, DBHelper.NotNull);
        }

        protected override void OnStart()
        {
            DesignSettings.Load();
        }

        async void DoCreateCommand()
        {

            try
            {
                var r = await this.SingleLine("新建", "名字");

                if (!r.IsFinished)
                {
                    return;
                }

                var template = new TypographyTemplate
                {
                    Id            = ID.Get(),
                    Name          = r.Value,
                    Pages         = new ViewList<TypographyPage>(),
                    OccupiedTable = new HashSet<string>(),
                    Base64Table   = new Dictionary<string, string>(),
                    Width         = (int)TypographyPageSize.Regular,
                };

                var project = new TemplateProject
                {
                    Id   = template.Id,
                    Name = template.Name,
                };

                if (await Navigate(new DesignViewModel(project, template)))
                {
                    DesignSettings.Save();
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        void DoLoadCommand()
        {
            var r = Interop.OpenFileAsync(ExtFilters.TypographyTemplate);

            if (!r.IsFinished)
            {
                return;
            }

            if (Projects.Any(x => x.FileName == r.Value))
            {
                return;
            }

            try
            {

                var template = JSON2.FromFile<TypographyTemplate>(r.Value);

                if (Projects.FirstOrDefault(x => x.Id == template.Id) is {} project)
                {
                    project.FileName = r.Value;
                    project.Name     = template.Name;
                }
                else
                {
                    Projects.Add(new TemplateProject
                    {
                        Id       = template.Id,
                        Name     = template.Name,
                        FileName = r.Value,
                    });
                }

                DesignSettings.Save();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        async void DoOpenCommand(TemplateProject target)
        {
            try
            {

                if (!File.Exists(target.FileName))
                {
                    this.Warning("警告", "文件不存在。");
                    return;
                }

                var template = JSON2.FromFile<TypographyTemplate>(target.FileName);
                await Navigate(new DesignViewModel(template));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        void DoRemoveCommand(TemplateProject target)
        {
            try
            {
                Projects.Remove(target);
                DesignSettings.Save();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public TemplateProject Project
        {
            get => _project;
            set => SetValue(ref _project, value);
        }

        public ICommandEX Open   { get; }
        public ICommandEX Load   { get; }
        public ICommandEX Create { get; }
        public ICommandEX Remove { get; }

        public ViewList<TemplateProject> Projects { get; }
    }

    public sealed class TemplateProject : ObservableObject
    {
        private string _name;
        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => SetValue(ref _fileName, value);
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }


        public required string Id { get; init; }
    }
}