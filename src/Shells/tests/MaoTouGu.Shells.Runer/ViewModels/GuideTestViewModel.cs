using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.Runer.ViewModels
{
    public class GuideTestViewModel : PageBase
    {
        protected override void OnStart()
        {
            this.SingleLine("Title", "asdadad");
            this.QueryWithDanger("Query Danger", "123123");
            base.OnStart();
        }
    }
}