// ----------------------------------------------------------
//            文件：RecentlyMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:03
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public class RecentlyMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
        public RecentlyMonikerWorkspaceItem()
        {
            CurrentDay = DateTime.Now;
            LastDay    = CurrentDay - TimeSpan.FromDays(7);
        }
        
        protected override void OnSetup()
        {
            //
            //
            MonikerService.Subject
                          .Subscribe(AddOrTrimmed)
                          .DisposeWith(DisposableCollection);
        }

        public override void Initialize(Moniker x)
        {
            if (!x.IsSoftDeleted && LastDay <= x.Modified && x.Modified <= CurrentDay)
            {
                AddOrTrimmed(x);
            }
        }
        
        public DateTime LastDay { get; }
        public DateTime CurrentDay { get; }
    }
}