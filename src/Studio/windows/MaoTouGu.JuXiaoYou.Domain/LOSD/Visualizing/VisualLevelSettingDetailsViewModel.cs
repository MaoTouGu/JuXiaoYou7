// ----------------------------------------------------------
//            文件：VisualLevelSettingDetailsViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 19:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Visualizers.Core;
using MaoTouGu.JuXiaoYou.Visualizers.Layouts;
using MaoTouGu.Studio.Database.Entities.VisualBlocks;

namespace MaoTouGu.JuXiaoYou.LOSD.Visualizing
{
    public sealed class VisualLevelSettingDetailsViewModel : JuXiaoYouPage
    {
        public VisualLevelSettingDetailsViewModel()
        {
            Blocks  = new ViewList<VisualBlockVPO>();
        }

        protected override void OnStart()
        {
            base.OnStart();



        }

        public ViewList<VisualBlockVPO> Blocks  { get; }
    }
}