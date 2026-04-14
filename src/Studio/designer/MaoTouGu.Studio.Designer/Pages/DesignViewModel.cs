// ----------------------------------------------------------
//            文件：DesignViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 02:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


using System.Globalization;
using System.Windows.Data;
using MaoTouGu.JuXiaoYou.Pages.Commands;
using MaoTouGu.JuXiaoYou.Visualizers.Core;
using MaoTouGu.Studio.Database;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Objects;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public partial class DesignViewModel : JuXiaoYouPage, IValueConverter
    {
        private readonly TypographyTemplate _template;

        private double _pageWidth;
        private double _pageHeight;
        private string _setting;

        private bool _layerBlockOnly;
        private bool _isCustomSize;

        private TypographyPageSize _pageSize;
        private TypographyBlockVPO _block;
        private TypographyLayerVPO _layer;
        private TypographyPage     _page;

        public DesignViewModel(TemplateProject project, TypographyTemplate template) : this(template)
        {
            _templateProject = project;
        }

        public DesignViewModel(TypographyTemplate template = null) : base(true, false)
        {
            _template = template ??
                        new TypographyTemplate
                        {
                            Id = ID.Get(),
                            Pages = new ViewList<TypographyPage>
                            {
                                new TypographyPage
                                {
                                    Id     = ID.Get(),
                                    Blocks = new ViewList<TypographyBlock>(),
                                    Layers = new ViewList<TypographyLayer>
                                    {
                                        new TypographyLayer
                                        {
                                            Id     = ID.Get(),
                                            Name   = "图层1",
                                            Blocks = new List<string>(),

                                        }
                                    },
                                    Name = "页面1",
                                },
                            },
                            OccupiedTable = new HashSet<string>(),
                            Base64Table   = new Dictionary<string, string>(),
                            Width         = (int)TypographyPageSize.Regular,
                        };

            Pages      = new ViewList<TypographyPage>();
            Blocks     = new ViewList<TypographyBlockVPO>();
            Layers     = new ViewList<TypographyLayerVPO>();
            Dictionary = new Dictionary<string, TypographyBlockVPO>();
            Bitmaps    = new ViewList<NamedBitmap>();
            PageSizes  = new List<TypographyPageSize>(ClassStatic.GetEnums<TypographyPageSize>());

            Moniker      = Moniker.Create(string.Empty, new User { Id = ID.Get(), DisplayName = "Test" });
            Moniker.Name = "测试";

            Moniker.Settings["Color"] = "#007ACC";

            //
            // 设置一个初始值，避免异常。
            PageWidth  = 600;
            PageHeight = 600;

            LayerBlockOnly = true;

            InstanceID = _template.Id;

            AddPage    = new AddPageCommand(this);
            ExportPage = null;
            ImportPage = null;
            RenamePage = null;
            RemovePage = new RemovePageCommand(this);

            AddLayer    = new AddLayerCommand(this);
            RenameLayer = null;
            ExportLayer = null;
            ImportLayer = null;
            RemoveLayer = new RemoveLayerCommand(this);

            AddVisualizer = new AddVisualizerCommand(this);
            AddImage      = new AddImageCommand(this);
            AddRectangle  = new AddRectangleCommand(this);
            AddText       = new AddTextCommand(this);
            RemoveBlock   = new RemoveBlockCommand(this);

            AddSetting    = new AddSettingCommand(this);
            UpdateSetting = new UpdateSettingCommand(this);

            Save   = new DelegateCommand(DoSaveCommand);
            SaveAs = new DelegateCommand(DoSaveAsCommand);
        }


        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        protected override void OnStart()
        {
            foreach (var (id, base64) in _template.Base64Table)
            {
                var buffer = System.Convert.FromBase64String(base64);
                var bi     = Xaml.ToBitmap(buffer);

                Bitmaps.Add(new NamedBitmap
                {
                    Image = bi,
                    Name  = id,
                });
            }

            foreach (var page in _template.Pages)
            {
                Pages.Add(page);

                foreach (var block in page.Blocks)
                {
                    var vpo = TypographyBlockVPO.GetInstance(block, Moniker);

                    if (Dictionary.TryAdd(block.Id, vpo))
                    {

                    }
                }
            }

            Page = Pages.FirstOrDefault();

            PageWidth = _template.Width;

            PageSize = _template.Width switch
            {
                800  => TypographyPageSize.Small,
                1000 => TypographyPageSize.Regular,
                1440 => TypographyPageSize.Large,
                2000 => TypographyPageSize.UltraLarge,
                _    => TypographyPageSize.Custom,
            };
        }

        void OnPageChanged(TypographyPage page)
        {
            Layers.AddMany(page.Layers.Select(x => new TypographyLayerVPO
            {
                Layer = x,
                Blocks = x.Blocks
                          .Select(y => Dictionary.SafetyGet(y))
                          .Where(DBHelper.NotNull)
                          .ToList(),
            }), true);

            //
            //
            Layer = Layers.FirstOrDefault();

            //
            //
            PageHeight = Math.Max(600, page.Height);
        }

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value?.ToString();
            return Bitmaps.FirstOrDefault(x => x.Name == v)?.Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
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

        public IEnumerable<TypographyBlockVPO> ObservableBlocks
        {
            get
            {
                if (LayerBlockOnly)
                {
                    return Blocks;
                }

                return Dictionary.Values;
            }
        }

        public IEnumerable<TypographyPageSize> PageSizes { get; }


        public bool IsCustomSize
        {
            get => _isCustomSize;
            set => SetValue(ref _isCustomSize, value);
        }

        public TypographyPageSize PageSize
        {
            get => _pageSize;
            set
            {
                SetValue(ref _pageSize, value);

                if (_pageSize == TypographyPageSize.Custom)
                {

                    IsCustomSize = true;
                    PageWidth    = Math.Max(PageWidth, 1440);
                }
                else
                {
                    IsCustomSize = false;
                    PageWidth    = (int)_pageSize;
                }
            }
        }

        /// <summary>
        /// 是否只呈现当前图层的元素。
        /// </summary>
        public bool LayerBlockOnly
        {
            get => _layerBlockOnly;
            set
            {
                SetValue(ref _layerBlockOnly, value);

                if (value)
                {
                    Layer = Layers.FirstOrDefault();
                    if (Layer is not null)
                    {
                        Blocks.AddMany(Layer.Blocks, true);
                    }
                }
                else
                {
                    Layer = null;
                }
                RaiseUpdated(nameof(ObservableBlocks));
            }
        }

        public string Setting
        {
            get => _setting;
            set => SetValue(ref _setting, value);
        }

        public TypographyLayerVPO Layer
        {
            get => _layer;
            set
            {
                SetValue(ref _layer, value);

                if (_layer is null)
                {
                    Blocks.Clear();

                }
                else
                {
                    Blocks.AddMany(_layer.Blocks, true);
                }

                if (LayerBlockOnly)
                {
                    RaiseUpdated(nameof(ObservableBlocks));
                }
            }
        }

        public TypographyPage Page
        {
            get => _page;
            set
            {
                SetValue(ref _page, value);

                if (_page is null)
                {
                    Layer = null;
                    Layers.Clear();
                }
                else
                {
                    OnPageChanged(_page);
                }
            }
        }

        public double PageHeight
        {
            get => _pageHeight;
            set
            {
                SetValue(ref _pageHeight, value);

                if (Page is null)
                {
                    return;
                }

                Page.Height = (int)Math.Clamp(_pageHeight, 600, 4000);
            }
        }

        public double PageWidth
        {
            get => _pageWidth;
            set
            {
                SetValue(ref _pageWidth, value);
                _template.Width = (int)Math.Clamp(_pageWidth, 600, 4000);
            }
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

        public Dictionary<string, TypographyBlockVPO> Dictionary { get; }

        public ViewList<NamedBitmap> Bitmaps { get; }

        public TypographyTemplate Template => _template;

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        public ICommandEX AddText       { get; }
        public ICommandEX AddImage      { get; }
        public ICommandEX AddRectangle  { get; }
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

        public ICommandEX AddSetting    { get; }
        public ICommandEX UpdateSetting { get; }
    }
}