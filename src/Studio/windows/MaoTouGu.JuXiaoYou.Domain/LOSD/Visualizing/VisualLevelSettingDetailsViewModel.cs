// ----------------------------------------------------------
//            文件：VisualLevelSettingDetailsViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 19:17
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Core;
using MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Layouts;
using MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Models;
using MaoTouGu.Studio.Database.Entities.VisualBlocks;

namespace MaoTouGu.JuXiaoYou.LOSD.Visualizing
{
    public sealed class VisualLevelSettingDetailsViewModel : JuXiaoYouPage
    {
        public VisualLevelSettingDetailsViewModel()
        {
            Blocks  = new ViewList<VisualBlockVPO>();
            Layouts = new ViewList<LayoutUnitVPO>();
        }

        protected override void OnStart()
        {
            base.OnStart();

            var radar = new Radar
            {
                Id   = ID.Get(),
                Name = "test",
            };
            var text = new TextGroup
            {
                Id   = ID.Get(),
                Name = "test",
            };

            var vpo     = VisualBlockBuilder.GetVisualBlockVPO(radar);
            var textvpo = VisualBlockBuilder.GetVisualBlockVPO(text);
            
            Blocks.Add(vpo);
            Blocks.Add(textvpo);
        }

        public ViewList<VisualBlockVPO> Blocks  { get; }
        public ViewList<LayoutUnitVPO>  Layouts { get; }
    }
}