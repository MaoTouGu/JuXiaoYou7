// ----------------------------------------------------------
//            文件：DeletedMonikerWorkspaceItem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces.Monikers
{
    public class DeletedMonikerWorkspaceItem : MonikerWorkspaceContainer
    {
        protected override void OnSetup()
        {
            //
            //
            MonikerService.Deleted
                          .Subscribe(Initialize)
                          .DisposeWith(DisposableCollection);
        }

        public override void Initialize(Moniker x)
        {
            if (!x.IsSoftDeleted)
            {
                return;
            }

            AddOrTrimmed(x);
        }
    }
}