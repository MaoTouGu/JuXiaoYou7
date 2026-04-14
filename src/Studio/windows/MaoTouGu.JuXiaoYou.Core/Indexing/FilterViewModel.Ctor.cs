// ----------------------------------------------------------
//            文件：FilterViewModel.Ctor.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 16:33
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database;
using Label = MaoTouGu.Studio.Database.References.Label;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    partial class FilterViewModel
    {
        public FilterViewModel(TopClass topClass, SubClass subClass, JuXiaoYouPage parent) : this($"{topClass.Id}+{subClass.Id}", parent)
        {
            Method = new ByWorldFilterMethod { TopClass = topClass, SubClass = subClass };
        }

        public FilterViewModel(CustomFilter filter, JuXiaoYouPage parent) : this(filter.Id, parent)
        {
            Method = BySettingFilterMethod.Get(filter);
        }

        public FilterViewModel(Label filter, JuXiaoYouPage parent) : this(filter.Id, parent)
        {
            Method = new ByLabelFilterMethod { Label = filter };
        }

        public FilterViewModel(Folder filter, JuXiaoYouPage parent) : this(filter.Id, parent)
        {
            Method = new ByFolderFilterMethod { Folder = filter };
        }

        private FilterViewModel(string id, JuXiaoYouPage parent) : base(id, parent)
        {
            OriginalSource       = new List<Moniker>();
            Monikers             = new ViewList<Moniker>();
            DisposableCollection = new DisposableCollection();

            MonikerService = GetService<MonikerService>();

            Add    = new DelegateCommand(DoAddCommand);
            Edit   = new DelegateCommand<Moniker>(DoEditCommand, DBHelper.NotNull);
            Remove = new DelegateCommand<Moniker>(DoRemoveCommand, DBHelper.NotNull);

            SetGravatar       = new SelectGravatarCommand(this);
            TemplateVisualize = new DelegateCommand<Moniker>(DoTemplateVisualizeCommand, DBHelper.NotNull);
        }

        protected override async void OnStart()
        {
            // MonikerService.Subject
            //               .Subscribe(OnBackgroundEntityAdding)
            //               .DisposeWith(DisposableCollection);
            //
            // MonikerService.Deleted
            //               .Subscribe(OnBackgroundEntityRemoving)
            //               .DisposeWith(DisposableCollection);

            //
            //
            await MonikerService.Start();
            await Method.Filter(OriginalSource, MonikerService.Collection);

            Monikers.AddMany(OriginalSource, true);

            Title = $"筛选：{Method.Name}";
        }

        protected override void OnStop()
        {
            DisposableCollection.Dispose();
        }
    }
}