// ----------------------------------------------------------
//            文件：IndexingOptionViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 19:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Pages;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public sealed class IndexingOptionViewModel : ObjectRoot<IndexingOption>
    {
        private readonly IndexingOption _option;

        private bool _editDomainText;
        private bool _editSubjectText;
        
        public IndexingOptionViewModel() : this(null)
        {

        }

        public IndexingOptionViewModel(IndexingOption option)
        {
            _option = option ?? new IndexingOption();

            DomainCollection = DatabaseManager.GetService<DomainService>().Collection;

            PickDomain        = new DelegateCommand(DoPickDomainCommand);
            PickVisualManager = new DelegateCommand(DoPickVisualManagerCommand);

            //
            // 寻找Domain。
            DomainInstance = DomainCollection.FirstOrDefault(x => x.Name == _option.Name);
        }


        private async void DoPickDomainCommand()
        {
            var r = await this.Object(new GlobalObjectPicker<Domain>(DomainCollection, nameof(Nameable.Name)));

            if (!r.IsFinished)
            {
                return;
            }

            DomainInstance = r.Value;
        }
        
        private async void DoPickVisualManagerCommand()
        {
            var r = await this.Object(new GlobalObjectPicker<Domain>(DomainCollection, nameof(Nameable.Name)));

            if (!r.IsFinished)
            {
                return;
            }

            DomainInstance = r.Value;
        }
        
        protected override IndexingOption OnFinish(bool edit) => _option;

        private Domain _domain;

        public ICommandEX PickDomain { get; }
        public ICommandEX PickSubject { get; }
        public ICommandEX PickVisualManager { get; }


         
        public bool EditSubjectText
        {
            get => _editSubjectText;
            set => SetValue(ref _editSubjectText, value);
        }
        
        public bool EditDomainText
        {
            get => _editDomainText;
            set => SetValue(ref _editDomainText, value);
        }
        
        public string Domain
        {
            get => _option.Domain;
            set
            {
                _option.Domain = value;
                RaiseUpdated();
            }
        }

        public Domain DomainInstance
        {
            get => _domain;
            set
            {
                SetValue(ref _domain, value);
                Domain = _domain?.Name;
                RaiseUpdated(nameof(InstanceID));
            }
        }

        public string InstanceID => _option.GetInstanceID();

        public ViewList<Domain>  DomainCollection  { get; }
    }
}