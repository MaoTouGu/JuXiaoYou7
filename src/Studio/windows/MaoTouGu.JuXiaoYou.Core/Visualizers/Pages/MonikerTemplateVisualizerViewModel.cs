// ----------------------------------------------------------
//            文件：MonikerTemplateVisualizerViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 00:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public sealed class MonikerTemplateVisualizerViewModel : JuXiaoYouPage
    {
        public MonikerTemplateVisualizerViewModel(Moniker moniker) : base(true, false)
        {
            Moniker    = moniker;
            InstanceID = moniker.Id;
            Title      = $"设卡{moniker.Name}";
            Pages      = new ViewList<TypographyPageVPO>();
            Load       = new DelegateCommand(DoLoadCommand);
        }

        void DoLoadCommand()
        {
            var r = Interop.OpenFileAsync(ExtFilters.TypographyTemplate);

            if (!r.IsFinished)
            {
                return;
            }

            try
            {
                var template = JSON2.FromFile<TypographyTemplate>(r.Value);

                foreach (var page in template.Pages)
                {
                    var vpo = new TypographyPageVPO
                    {
                        Width  = template.Width,
                        Height = page.Height,
                        Name   = page.Name,
                        Blocks = new ViewList<TypographyBlockVPO>(),
                    };

                    foreach (var block in page.Blocks)
                    {
                        vpo.Blocks.Add(TypographyBlockVPO.GetInstance(block, Moniker));
                    }

                    Pages.Add(vpo);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public Moniker Moniker { get; }

        public ViewList<TypographyPageVPO> Pages { get; }

        public ICommandEX Load { get; }
    }
}