using System.Windows;
using System.Windows.Input;
using MaoTouGu.Shells.Attributes;
using MaoTouGu.Shells.Controls;
using MaoTouGu.Shells.Core;
using MaoTouGu.Shells.Runer.ViewModels;

namespace MaoTouGu.Shells.Runer.Views
{

    [Associate(View = typeof(GuideTestView), ViewModel = typeof(GuideTestViewModel))]
    public partial class GuideTestView : ForestPage
    {
        public GuideTestView()
        {
            InitializeComponent();
            MouseDoubleClick += OnMouseDoubleClick;
        }
        
        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowGuide();
        }

        protected override IEnumerable<GuideObject> BuildGuideWizards(string hint)
        {
            if (hint == "Play")
            {
                return new[]
                {

                    new GuideObject
                    {
                        Title   = "准备步骤",
                        Content = "当你准备好一切的时候就可以游玩了。",
                        Color   = "#CF4747",
                    },
                    new GuideObject
                    {
                        Title   = "开始游玩",
                        Content = "当你准备好一切的时候就可以游玩了。",
                        Color   = "#CF4747",
                    },
                };
            }
            return base.BuildGuideWizards(hint);
        }

        protected override GuideObject BuildGuideWizard(string hint)
        {
            return hint switch
            {
                "Capture" => new GuideObject
                {
                    Title   = "截图",
                    Content = "点击这个按钮就可以截图当前页面了。",
                },
                _ => null,
            };
        }
    }
}