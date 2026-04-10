// ----------------------------------------------------------
//            文件：FilterViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 17:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Indexing.BySetting;
using MaoTouGu.JuXiaoYou.Visualizers;
using MaoTouGu.Studio.Database;
using Label = MaoTouGu.Studio.Database.References.Label;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class FilterViewModel : NestedPage
    {
        protected readonly List<Moniker>  OriginalSource;
        protected readonly MonikerService MonikerService;
        
        private Moniker _moniker;

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
            OriginalSource = new List<Moniker>();
            Monikers       = new ViewList<Moniker>();
            MonikerService = GetService<MonikerService>();

            Edit        = new DelegateCommand<Moniker>(DoEditCommand, DBHelper.NotNull);
            SetGravatar = new SelectGravatarCommand(this);
        }

        private async void DoEditCommand(Moniker target)
        {
            if (target is null)
            {
                return;
            }

            await Navigate(new SimpleMonikerSettingViewModel(target));
        }

        protected override async void OnStart()
        {
            await MonikerService.Start();
            await Method.Filter(OriginalSource, MonikerService.Collection);
            
            Monikers.AddMany(OriginalSource, true);
        }

        public FilterMethod Method { get; }

        public ViewList<Moniker> Monikers { get; }

        
        public ICommandEX Edit        { get; }
        public ICommandEX SetGravatar { get; }

        public Moniker Moniker
        {
            get => _moniker;
            set
            {
                SetValue(ref _moniker, value);
                Edit.RaiseUpdate();
            }
        }
    }
}