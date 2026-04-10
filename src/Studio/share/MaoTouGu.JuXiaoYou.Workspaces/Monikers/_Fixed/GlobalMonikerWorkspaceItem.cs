// ----------------------------------------------------------
//            文件：GlobalMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:07
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public class GlobalMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
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
            if (x.IsSoftDeleted)
            {
                return;
            }

            AddOrTrimmed(x);
        }
    }
}