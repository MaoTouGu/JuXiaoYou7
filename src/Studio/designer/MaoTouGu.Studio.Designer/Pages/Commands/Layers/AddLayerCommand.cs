// ----------------------------------------------------------
//            文件：AddLayerCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 17:11
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddLayerCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        public override void Execute(object parameter)
        {
            var name = $"图层{Context.Pages.Count + 1}";
            var layer = new TypographyLayer
            {
                Id   = ID.Get(),
                Name = name,
               Blocks = new List<string>(),
            };

            var vpo = new TypographyLayerVPO
            {
                Layer  = layer,
                Blocks = new List<TypographyBlockVPO>(),
            };
            
            //
            //
            Context.Layers.Add(vpo);
            Context.Page.Layers.Add(layer);
            Context.SetDirtyState(true);
        }
    }
}