// ----------------------------------------------------------
//            文件：DesignViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 02:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using MaoTouGu.JuXiaoYou.Pages.Commands;
using MaoTouGu.JuXiaoYou.Visualizers.Core;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Objects;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class DesignViewModel : JuXiaoYouPage
    {
        private readonly TypographyTemplate _template;

        private double _pageWidth;
        private double _pageHeight;

        private TypographyBlockVPO _block;
        private TypographyLayerVPO _layer;
        private TypographyPage     _page;

        public DesignViewModel(TypographyTemplate template = null)
        {
            _template = template ??
                        new TypographyTemplate
                        {
                            Pages         = new ViewList<TypographyPage>(),
                            OccupiedTable = new HashSet<string>(),
                            Base64Table   = new Dictionary<string, string>(),
                        };

            Pages  = new ViewList<TypographyPage>();
            Blocks = new ViewList<TypographyBlockVPO>();
            Layers = new ViewList<TypographyLayerVPO>();



            Moniker                   = Moniker.Create(string.Empty, new User { Id = ID.Get(), DisplayName = "Test" });
            Moniker.Name              = "测试";
            Moniker.Gravatar          = @"C:\Users\Luoyisi\Pictures\Character.png";
            Moniker.Settings["Color"] = "#007ACC";

            AddPage    = new AddPageCommand(this);
            RemovePage = new RemovePageCommand(this);

            AddLayer = new AddLayerCommand(this);

            AddVisualizer = new AddVisualizerCommand(this);
        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public TypographyBlockVPO Block
        {
            get => _block;
            set => SetValue(ref _block, value);
        }


        public TypographyLayerVPO Layer
        {
            get => _layer;
            set => SetValue(ref _layer, value);
        }
        public TypographyPage Page
        {
            get => _page;
            set => SetValue(ref _page, value);
        }

        public double PageHeight
        {
            get => _pageHeight;
            set => SetValue(ref _pageHeight, value);
        }

        public double PageWidth
        {
            get => _pageWidth;
            set => SetValue(ref _pageWidth, value);
        }

        public Moniker Moniker { get; }

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public ViewList<TypographyPage>     Pages  { get; }
        public ViewList<TypographyLayerVPO> Layers { get; }
        public ViewList<TypographyBlockVPO> Blocks { get; }

        public TypographyTemplate Template => _template;

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public ICommandEX AddTextBlock  { get; }
        public ICommandEX AddImageBlock { get; }
        public ICommandEX AddVisualizer { get; }
        public ICommandEX RemoveBlock   { get; }

        public ICommandEX AddLayer    { get; }
        public ICommandEX ExportLayer { get; }
        public ICommandEX ImportLayer { get; }
        public ICommandEX RenameLayer { get; }
        public ICommandEX RemoveLayer { get; }

        public ICommandEX AddPage    { get; }
        public ICommandEX RenamePage { get; }
        public ICommandEX RemovePage { get; }
        public ICommandEX ExportPage { get; }
        public ICommandEX ImportPage { get; }

        public ICommandEX Save   { get; }
        public ICommandEX SaveAs { get; }
        public ICommandEX Import { get; }
        public ICommandEX Export { get; }
    }
}