// ----------------------------------------------------------
//            文件：CatalogViewModelBase.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 15:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public abstract class CatalogViewModelBase : MonikerViewModelBase
    {
        private Catalog _catalog;

        protected CatalogViewModelBase(IndexingOption option) : base(option)
        {
            Catalogs = new ViewList<Catalog>();

            AddCatalog    = new AddCatalogCommand(this);
            EditCatalog   = new EditCatalogCommand(this);
            RemoveCatalog = new RemoveCatalogCommand(this);
        }

        protected override async Task OnSync(string documentID, string handlerName, string eventID, DataOperation op)
        {

            // if (eventID == IndexSystem.UniqueReferenceService.EventID)
            // {
            //     var service = IndexSystem.UniqueReferenceService;
            //
            //     //
            //     // Update和Removed操作都需要删除
            //     if (op is DataOperation.Removed or DataOperation.Updated)
            //     {
            //         var old         = service.LocalRemove(documentID);
            //         var subordinate = Subordinates.Remove(x => x.Id == old.Id);
            //
            //         if (op is DataOperation.Removed && subordinate.Id == Catalog.Id)
            //         {
            //             GUI.RunOnUIThread(() =>
            //                               {
            //                                   var moniker = Monikers.FirstOrDefault(x => x.Id == subordinate.DocumentID);
            //
            //                                   if (moniker is null)
            //                                   {
            //                                       return;
            //                                   }
            //
            //                                   if (subordinate.Name == _catalog?.Name)
            //                                   {
            //                                       Monikers.Remove(moniker);
            //                                   }
            //                               });
            //         }
            //     }
            //
            //     //
            //     // 其后添加。
            //     if (op is DataOperation.Added or DataOperation.Updated)
            //     {
            //         var subordinate = await service.LocalAdd(documentID);
            //
            //         //
            //         //
            //         Subordinates.Add(subordinate);
            //
            //         GUI.RunOnUIThread(() =>
            //                           {
            //                               var moniker = Monikers.FirstOrDefault(x => x.Id == subordinate.DocumentID);
            //
            //                               if (moniker is null)
            //                               {
            //                                   return;
            //                               }
            //
            //                               OriginalSource.Add(moniker);
            //
            //                               if (subordinate.Name == _catalog?.Name)
            //                               {
            //                                   Monikers.Add(moniker);
            //                               }
            //                           });
            //     }
            //
            // }
            // else if (eventID == IndexSystem.FolderService.EventID)
            // {
            //     var service = IndexSystem.FolderService;
            //
            //     //
            //     // Update和Removed操作都需要删除
            //     if (op is DataOperation.Removed or DataOperation.Updated)
            //     {
            //         var old     = service.LocalRemove(documentID);
            //         var catalog = Catalogs.Remove(x => x.Id == old.Id);
            //
            //         if (op is DataOperation.Removed && catalog.Id == Catalog.Id)
            //         {
            //             GUI.RunOnUIThread(() => Catalog = Catalogs.FirstOrDefault());
            //         }
            //     }
            //
            //     //
            //     // 其后添加。
            //     if (op is DataOperation.Added or DataOperation.Updated)
            //     {
            //         var catalog = await service.LocalAdd(documentID);
            //
            //         GUI.RunOnUIThread(() => Catalogs.Add(catalog));
            //     }
            // }
            // else if (eventID == IndexSystem.MonikerService.EventID)
            // {
            //     var service = IndexSystem.MonikerService;
            //
            //     //
            //     // Update和Removed操作都需要删除
            //     if (op is DataOperation.Removed or DataOperation.Updated)
            //     {
            //         var moniker = service.LocalRemove(documentID);
            //         var index   = OriginalSource.IndexOf(x => x.Id == moniker.Id);
            //
            //         if (index > 0)
            //         {
            //             GUI.RunOnUIThread(() => Monikers.Remove(x => x.Id == moniker.Id));
            //         }
            //     }
            //
            //     //
            //     // 其后添加。
            //     if (op is DataOperation.Added or DataOperation.Updated)
            //     {
            //         await service.LocalAdd(documentID);
            //     }
            // }

            await base.OnSync(documentID, handlerName, eventID, op);
        }

        #region Adding

        protected internal override async Task OnAdding(Moniker moniker)
        {
            var subordinate = new Subordinate
            {
                Id         = ID.Get(),
                Domain     = Options.Domain,
                Subject    = Options.Subject,
                Name       = Catalog.Name,
                DocumentID = moniker.Id,
            };

            //
            // 添加Subordinate
            await IndexSystem.UniqueReferenceService.Add(subordinate);
            Subordinates.Add(subordinate);


            //
            //
            foreach (var catalog in Catalogs)
            {
                catalog.TotalCount += 1;

                if (catalog == Catalog)
                {
                    catalog.Count += 1;
                }
            }

            OriginalSource.Add(moniker);
            Monikers.Add(moniker);
        }

        protected internal override bool CanAdding() => Catalog is not null;

        #endregion

        protected override void OnRemove(Moniker moniker)
        {
            //
            // 统计数据。
            foreach (var catalog in Catalogs)
            {
                var count = Subordinates.Count(x => x.Name == catalog.Name);

                GUI.RunOnUIThread(() =>
                                  {

                                      catalog.TotalCount = OriginalSource.Count;
                                      catalog.Count      = count;
                                  });
            }
        }

        /// <summary>
        /// 加载所有分类。
        /// </summary>
        /// <remarks>切记，这个方法应该在<see cref="LoadMonikerAsync"/>之前调用。</remarks>
        protected async Task LoadCatalogAsync()
        {
            //
            //
            await IndexSystem.InitializeAsync();

            //
            //
            var catalogs = IndexSystem.CatalogService
                                      .Find(Options.Domain, Options.Subject);

            //
            //
            Catalogs.AddMany(catalogs, true);

            //
            //
            if (Catalogs.Count == 0)
            {
                var initializeItems = VisualManager.InitializeCatalogs();

                foreach (var item in initializeItems)
                {
                    //
                    //
                    await IndexSystem.CatalogService.Add(item);

                    //
                    //
                    Catalogs.Add(item);
                }
            }
            
            Catalogs.Sort();
        }

        /// <summary>
        /// 加载所有设定。
        /// </summary>
        /// <remarks>切记，这个方法应该在<see cref="OnCatalogChanged"/>之前调用。</remarks>
        protected Task LoadMonikerAsync()
        {
            return Task.Run(() =>
                            {
                                Subordinates.Clear();
                                OriginalSource.Clear();

                                //
                                // 获得所有Subordinate
                                var iterator = IndexSystem.UniqueReferenceService.Find(Options.Domain, Options.Subject);

                                //
                                // 添加Subordinate
                                Subordinates.AddRange(iterator);

                                //
                                // 获得包含设定ID的Hashset
                                var set = Subordinates.Select(x => x.DocumentID).ToHashSet();

                                //
                                // 添加到原始数据。
                                OriginalSource.AddRange(IndexSystem.MonikerService
                                                                   .Collection
                                                                   .Where(x => !x.IsSoftDeleted && set.Contains(x.Id)));

                                //
                                // 统计数据。
                                foreach (var catalog in Catalogs)
                                {
                                    var count = Subordinates.Count(x => x.Name == catalog.Name);

                                    GUI.RunOnUIThread(() =>
                                                      {

                                                          catalog.TotalCount = OriginalSource.Count;
                                                          catalog.Count      = count;
                                                      });
                                }
                            });
        }

        /// <summary>
        /// 给定指定的目录获得Moniker。
        /// </summary>
        /// <param name="catalog">目录，不为空。</param>
        /// <returns>返回获得的Moniker。</returns>
        protected IEnumerable<Moniker> GetMonikers(Catalog catalog)
        {
            var subordinates = Subordinates.Where(x => x.Name == catalog.Name);
            var set          = subordinates.Select(x => x.DocumentID).ToHashSet();
            return OriginalSource.Where(x => !x.IsSoftDeleted && set.Contains(x.Id));
        }

        /// <summary>
        /// 当目录变更时，筛选对应的设定。
        /// </summary>
        /// <param name="catalog">目录，不为空。</param>
        protected virtual void OnCatalogChanged(Catalog catalog)
        {
            Monikers.AddMany(GetMonikers(catalog), true);
        }

        /// <summary>
        /// 当前选择的分类。
        /// </summary>
        public Catalog Catalog
        {
            get => _catalog;
            set
            {
                SetValue(ref _catalog, value);

                //
                // 通知按钮的更改。
                EditCatalog.RaiseUpdate();
                RemoveCatalog.RaiseUpdate();

                if (_catalog is null)
                {
                    Monikers.Clear();
                }
                else
                {
                    OnCatalogChanged(_catalog);
                }
            }
        }

        public ICommandEX AddCatalog    { get; }
        public ICommandEX EditCatalog   { get; }
        public ICommandEX RemoveCatalog { get; }

        public ViewList<Catalog> Catalogs { get; }
    }
}