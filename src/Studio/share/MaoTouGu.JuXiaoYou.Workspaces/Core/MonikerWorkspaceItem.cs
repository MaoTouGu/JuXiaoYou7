// ----------------------------------------------------------
//            文件：MonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:00
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Workspaces.Monikers;

namespace MaoTouGu.JuXiaoYou.Core
{
    public abstract class MonikerWorkspaceItem : WorkspaceItem
    {
        protected readonly DisposableCollection DisposableCollection;

        protected MonikerWorkspaceItem()
        {
            DisposableCollection = new DisposableCollection();
        }

        public void Setup(
            MonikerService service,
            IEnumerable<MonikerWorkspaceContainer> containers,
            FolderService folderService,
            LabelService labelService,
            FilterService filterService)
        {
            Containers     = containers;
            MonikerService = service;
            FolderService  = folderService;
            LabelService   = labelService;
            FilterService  = filterService;
            OnSetup();
        }

        protected virtual void OnSetup()
        {

        }

        public virtual void Initialize(Moniker x)
        {
            
        }

        public MonikerService MonikerService { get; private set; }
        public FolderService  FolderService  { get; private set; }
        public LabelService   LabelService   { get; private set; }
        public FilterService  FilterService  { get; private set; }

        protected IEnumerable<MonikerWorkspaceContainer> Containers { get; private set; }

        protected override void ReleaseManagedResources()
        {
            DisposableCollection.Dispose();
        }
    }
}