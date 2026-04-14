// ----------------------------------------------------------
//            文件：WorkspaceViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 17:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Workspaces.Graphing;
using MaoTouGu.JuXiaoYou.Visualizers.Pages;
using MaoTouGu.Studio;

namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public sealed partial class WorkspaceViewModel : SystemPage, IHostedWindowNavigation
    {
        public WorkspaceViewModel()
        {
            Tabs      = new ViewList<NestedPage>();
            Feature   = new FeatureWorkspace();
            Graphing  = new GraphingWorkspace();
            WorldView = new WorldViewWorkspace();
            Moniker   = new MonikerWorkspace(WorldView);
            Teamspace = new TeamspaceWorkspace();
        }

        protected override async void OnStart()
        {

            //
            //
            Feature.Start();
            Graphing.Start();
            Moniker.Start();
            Teamspace.Start();
            WorldView.Start();
            

            //
            //
            BuildProperties();
        }

        private NestedPage _tab;

        public void Open<T>(T page) where T : NestedPage
        {
            if (Tabs.OfType<T>().FirstOrDefault() is {} item && item.InstanceID == page.InstanceID)
            {
                Tab = item;

            }
            else
            {
                Tab = page;
                Tabs.Add(page);
            }
        }

        public NestedPage Tab
        {
            get => _tab;
            set => SetValue(ref _tab, value);
        }

        public ViewList<NestedPage> Tabs { get; }
    }
}