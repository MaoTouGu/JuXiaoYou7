// ----------------------------------------------------------
//            文件：MonikerViewModelBase.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 14:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class MonikerViewModelBase : JuXiaoYouPage, IPushingEventHandler
    {
        protected readonly List<Moniker>     OriginalSource;
        protected readonly List<Subordinate> Subordinates;

        private Moniker _moniker;

        protected MonikerViewModelBase(IndexingOption option) : base(true, false)
        {
            Options    = option ?? throw new ArgumentNullException(nameof(option));
            InstanceID = option.GetInstanceID();

            //
            //
            Monikers       = new ViewList<Moniker>();
            MoreCommands   = new ViewList<PseudoCommandItem>();
            OriginalSource = new List<Moniker>();
            Subordinates   = new List<Subordinate>();

            //
            //
            VisualManager = FeatureManager.GetVisualManager(option.VisualManager);

            //
            //
            Add    = new AddMonikerCommand(this);
            Open   = new OpenMonikerCommand(this);
            Remove = new RemoveMonikerCommand(this);
        }

        #region IPushingEventHandler Interface Implements

        bool IPushingEventHandler.CanHandle(string eventID)
        {
            return eventID == IndexSystem.MonikerService.EventID ||
                   eventID == IndexSystem.UniqueReferenceService.EventID;
        }

        async Task IPushingEventHandler.Handle(string documentID, string handlerName, string eventID, DataOperation op)
        {
            await OnSync(documentID, handlerName, eventID, op);
        }

        protected virtual Task OnSync(string documentID, string handlerName, string eventID, DataOperation op)
        {
            this.Info("通知", $"{handlerName}修改了数据。");
            return Task.CompletedTask;
        }

        #endregion

        #region Adding

        /// <summary>
        /// 获得SubordinateName，如果有的话。
        /// </summary>
        /// <returns></returns>
        protected internal virtual bool CanAdding() => true;

        protected internal virtual async Task OnAdding(Moniker moniker)
        {
            //
            // 添加Subordinate
            var subordinate = new Subordinate
            {
                Id         = ID.Get(),
                Domain     = Options.Domain,
                Subject    = Options.Subject,
                DocumentID = moniker.Id,
            };

            //
            // 添加Subordinate
            await IndexSystem.UniqueReferenceService.Add(subordinate);
            Subordinates.Add(subordinate);


            OriginalSource.Add(moniker);
            Monikers.Add(moniker);
        }

        #endregion


        protected override void OnStart()
        {
            if (Options.Commands is not null)
            {
                MoreCommands.AddMany(Options.Commands);
            }
        }

        #region Internal Methods

        internal void RemoveInternal(Moniker target)
        {
            if (OriginalSource.Remove(target))
            {
                //
                //
                var index = Monikers.IndexOf(target);

                if (index < 0 || Moniker != target)
                {
                    return;
                }

                if (index >= Monikers.Count)
                {
                    Moniker = Monikers.LastOrDefault();
                }
                else
                {
                    Moniker = Monikers[index];
                }

                //
                //
                Monikers.RemoveAt(index);
            }

            Subordinates.Remove(x => x.DocumentID == target.Id);

            OnRemove(target);
        }

        protected virtual void OnRemove(Moniker moniker)
        {

        }

        internal string CatalogName => (this as CatalogViewModelBase)?.Catalog?.Name;

        #endregion

        public IEnumerable<Moniker> GetOriginalSource() => OriginalSource;

        public Moniker Moniker
        {
            get => _moniker;
            set => SetValue(ref _moniker, value);
        }

        public IVisualManager VisualManager { get; }
        public IndexingOption Options       { get; }

        public ICommandEX Add    { get; }
        public ICommandEX Open   { get; }
        public ICommandEX Remove { get; }

        public ViewList<Moniker> Monikers { get; }

        public ViewList<PseudoCommandItem> MoreCommands { get; }
    }
}