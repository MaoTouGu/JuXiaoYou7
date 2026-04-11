// ----------------------------------------------------------
//            文件：AddPageCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 16:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Pages.Commands
{
    sealed class AddPageCommand(DesignViewModel target) : ContextCommand<DesignViewModel>(target)
    {
        public override void Execute(object parameter)
        {
            var name = $"页面{Context.Pages.Count + 1}";
            var page = new TypographyPage
            {
                Id   = ID.Get(),
                Name = name,
                Layers = new ViewList<TypographyLayer>
                {
                    new TypographyLayer
                    {
                        Name   = "图层1",
                        Blocks = new List<string>(),
                    },
                },
                Blocks = new ViewList<TypographyBlock>(),
            };

            Context.Pages.Add(page);
            Context.Template.Pages.Add(page);
            Context.Page = page;
            Context.SetDirtyState(true);
        }
    }
}