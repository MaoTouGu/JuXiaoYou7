// ----------------------------------------------------------
//            文件：MonikerWorkspace.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 01:10
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Indexing;
using MaoTouGu.Studio.Database;
using Label = MaoTouGu.Studio.Database.References.Label;

namespace MaoTouGu.JuXiaoYou.Visualizers.Pages
{
    public sealed class MonikerWorkspace : SpecificWorkspace
    {
        private readonly MonikerService         _service;
        private readonly FolderService          _folderService;
        private readonly LabelService           _labelService;
        private readonly UniqueReferenceService _uniqueService;
        private readonly ReferenceService       _referenceService;
        private readonly KeywordService         _keywordService;
        private readonly FilterService          _filterService;


        //
        // +---- 星标设定
        // |
        // +---- 最近设定
        // |
        // +---- 自定义分类
        // |
        // +-------- 按分类查看
        // |
        // +-------- 按标签查看
        // |
        // +-------- 按分组查看
        // |
        // +---- 高级选项
        // |
        // +-------- 所有设定（123）
        // |
        // +-------- 已删除（23）
        // |
        // +-------- 开发者选项（23）

        public MonikerWorkspace(WorldViewWorkspace v5)
        {
            _service          = DatabaseManager.GetService<MonikerService>();
            _folderService    = DatabaseManager.GetService<FolderService>();
            _labelService     = DatabaseManager.GetService<LabelService>();
            _uniqueService    = DatabaseManager.GetService<UniqueReferenceService>();
            _referenceService = DatabaseManager.GetService<ReferenceService>();
            _keywordService   = DatabaseManager.GetService<KeywordService>();
            _filterService    = DatabaseManager.GetService<FilterService>();

            V5Workspace = v5;

            All       = new GlobalMonikerWorkspaceItem();
            Favorite  = new FavoriteMonikerWorkspaceItem();
            Recently  = new RecentlyMonikerWorkspaceItem();
            Deleted   = new DeletedMonikerWorkspaceItem();
            ByFolder  = new ByFolderMonikerWorkspaceItem();
            ByIndexer = new ByIndexerMonikerWorkspaceItem(v5.Worlds.Items);
            ByLabel   = new ByLabelMonikerWorkspaceItem();
            BySetting = new BySettingMonikerWorkspaceItem();


            Advanced = new MonikerFolder
            {
                Name = "高级选项",
                Items =
                {
                    All,
                    Deleted,
                },
            };

            //
            //
            Items.Add(Favorite);
            Items.Add(Recently);
            Items.Add(ByIndexer);
            Items.Add(ByFolder);
            Items.Add(ByLabel);
            Items.Add(BySetting);
            Items.Add(Advanced);
        }

        protected override async void OnStart()
        {

            //
            //
            await _service.Start();
            await _folderService.Start();
            await _filterService.Start();
            await _labelService.Start();
            await _uniqueService.Start();
            await _referenceService.Start();
            await _keywordService.Start();

            //
            //
            WorkspaceItems.ForEach(w => w.Setup(_service, WorkspaceItems, _folderService, _labelService, _filterService));

            //
            //
            foreach (var x in _service.Collection)
            {
                WorkspaceItems.ForEach(w => w.Initialize(x));
            }
        }

        /*******************************************************************
         *
         *
         *                      Label Methods
         *
         *
         *******************************************************************/


        public async Task Add(PageBase viewModel, TopClassWorkspaceItem instance)
        {
            var r = await FilterMethod.Add(viewModel);

            if (!r.IsFinished)
            {
                return;
            }
        }

        public async Task Add(PageBase viewModel, SubClassWorkspaceItem instance)
        {

            //
            // 寻找TopClass
            var topClassWI = V5Workspace.GetTopClassWorkspaceItem(instance.ParentID);

            await FilterMethod.AddMonikerAndUniqueReference(
                                                            viewModel,
                                                            topClassWI.Instance,
                                                            instance.Instance,
                                                            _uniqueService);
        }

        public Task Add(PageBase viewModel, LabelWrapperItem instance)
        {
            return FilterMethod.AddMonikerAndKeyword(viewModel, instance.Label, _keywordService);
        }

        public Task Add(PageBase viewModel, FolderWrapperItem instance) => FilterMethod.AddMonikerAndReference(viewModel, instance.Folder, _referenceService);

        /*******************************************************************
         *
         *
         *                      Label Methods
         *
         *
         *******************************************************************/
        public async Task AddLabel(WorkspaceViewModel viewModel, LabelWrapperItem item)
        {
            var r = await viewModel.SingleLine("新建", "创建一个标签");

            if (!r.IsFinished)
            {
                return;
            }

            var label = new Label
            {
                Id    = ID.Get(),
                Name  = r.Value,
                Color = "#808080",
            };

            var wrapper = new LabelWrapperItem
            {
                Label = label,
            };

            if (item is null)
            {
                label.Index = ByLabel.Items.Count;

                ByLabel.Items.Add(wrapper);
            }
            else
            {
                label.Index  = item.Items.Count;
                label.Parent = item.Id;
                item.Items.Add(wrapper);
            }

            await _labelService.Add(label);
        }

        public async Task EditLabel(WorkspaceViewModel viewModel, LabelWrapperItem item)
        {
            if (item is null)
            {
                return;
            }

            var r = await viewModel.SingleLine("新建", "编辑一个标签", item.Label.Name);

            if (!r.IsFinished)
            {
                return;
            }

            item.Label.Name = r.Value;

            //
            //
            await _labelService.Add(item.Label);
        }

        public async Task RemoveLabel(WorkspaceViewModel viewModel, LabelWrapperItem item)
        {
            if (item is null)
            {
                return;
            }

            if (!await viewModel.RemoveThis())
            {
                return;
            }

            if (string.IsNullOrEmpty(item.ParentID))
            {
                ByLabel.Items.Remove(item);
            }
            else if (ByLabel.Dictionary.TryGetValue(item.ParentID, out var parent))
            {
                parent.Items.Remove(item);
            }
            await _labelService.Remove(item.Label);
        }

        /*******************************************************************
         *
         *
         *                      Folder Methods
         *
         *
         *******************************************************************/
        public async Task AddFolder(WorkspaceViewModel viewModel, FolderWrapperItem item)
        {
            var r = await viewModel.SingleLine("新建", "创建一个目录");

            if (!r.IsFinished)
            {
                return;
            }

            var folder = new Folder
            {
                Id   = ID.Get(),
                Name = r.Value,
            };

            var wrapper = new FolderWrapperItem
            {
                Folder = folder,
            };

            if (item is null)
            {
                folder.Index = ByFolder.Items.Count;

                ByFolder.Items.Add(wrapper);
            }
            else
            {
                folder.Index  = item.Items.Count;
                folder.Parent = item.Id;
                item.Items.Add(wrapper);
            }

            await _folderService.Add(folder);
        }

        public async Task EditFolder(WorkspaceViewModel viewModel, FolderWrapperItem item)
        {
            if (item is null)
            {
                return;
            }

            var r = await viewModel.SingleLine("新建", "编辑一个目录", item.Folder.Name);

            if (!r.IsFinished)
            {
                return;
            }

            item.Folder.Name = r.Value;

            //
            //
            await _folderService.Add(item.Folder);
        }

        public async Task RemoveFolder(WorkspaceViewModel viewModel, FolderWrapperItem item)
        {
            if (item is null)
            {
                return;
            }

            if (!await viewModel.RemoveThis())
            {
                return;
            }

            if (string.IsNullOrEmpty(item.ParentID))
            {
                ByFolder.Items.Remove(item);
            }
            else if (ByFolder.Dictionary.TryGetValue(item.ParentID, out var parent))
            {
                parent.Items.Remove(item);
            }

            await _folderService.Remove(item.Folder);
        }


        /*******************************************************************
         *
         *
         *                      Folder Methods
         *
         *
         *******************************************************************/
        public async Task AddFilter(WorkspaceViewModel viewModel)
        {
            var r = await viewModel.Object(new CustomFilterPickerViewModel());

            if (!r.IsFinished)
            {
                return;
            }

            var filter   = r.Value;
            var filterMI = new BySettingFilterMethodItem(filter);

            BySetting.Items.Add(filterMI);

            //
            //
            await viewModel.Object(new CustomFilterViewModel(filter));
            await _filterService.Add(filter);
        }

        public async Task EditFilter(WorkspaceViewModel viewModel, BySettingFilterMethodItem target)
        {
            if (target?.Filter is null)
            {
                return;
            }

            var filter = target.Filter;
            var result = await viewModel.Object(new CustomFilterViewModel(filter));

            await _filterService.Update(filter);

        }
        public async Task RemoveFilter(WorkspaceViewModel viewModel, BySettingFilterMethodItem target)
        {
            if (target?.Filter is null)
            {
                return;
            }

            var filter = target.Filter;

            if (!await viewModel.RemoveThis())
            {
                return;
            }

            await _filterService.Remove(filter);
            BySetting.Items.Remove(target);
        }

        public void ExportFilter(WorkspaceViewModel viewModel, BySettingFilterMethodItem item)
        {
            var r = Interop.SaveFileAsync(ExtFilters.CustomFilter, ExtFilters.CustomFilterExt);

            if (!r.IsFinished)
            {
                return;
            }

            JSON2.ToFile(r.Value, item.Filter);
            viewModel.SaveSuccess();
        }

        public async Task ImportFilter(WorkspaceViewModel viewModel, BySettingFilterMethodItem item)
        {
            var r = Interop.OpenFileAsync(ExtFilters.CustomFilter);

            if (!r.IsFinished)
            {
                return;
            }

            try
            {
                var filter   = JSON2.FromFile<CustomFilter>(r.Value);
                var filterMI = new BySettingFilterMethodItem(filter);

                if (_filterService.Has(filter.Id))
                {
                    await _filterService.Update(filter);
                }
                else
                {
                    await _filterService.Add(filter);
                }
                BySetting.Items.Add(filterMI);

                viewModel.SaveSuccess("提示", "导入成功");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /*******************************************************************
         *
         *
         *                      TopClass Methods
         *
         *
         *******************************************************************/
        public IEnumerable<MonikerWorkspaceContainer> WorkspaceItems
        {
            get
            {
                yield return All;
                yield return Deleted;
                yield return Favorite;
                yield return Recently;
                yield return BySetting;
                yield return ByFolder;
                yield return ByLabel;
            }
        }

        public WorldViewWorkspace V5Workspace { get; }
        public WorkspaceFolder    Advanced    { get; }

        public GlobalMonikerWorkspaceItem   All      { get; }
        public DeletedMonikerWorkspaceItem  Deleted  { get; }
        public FavoriteMonikerWorkspaceItem Favorite { get; }
        public RecentlyMonikerWorkspaceItem Recently { get; }

        public ByFolderMonikerWorkspaceItem  ByFolder  { get; }
        public ByIndexerMonikerWorkspaceItem ByIndexer { get; }
        public ByLabelMonikerWorkspaceItem   ByLabel   { get; }
        public BySettingMonikerWorkspaceItem BySetting { get; }

    }
}