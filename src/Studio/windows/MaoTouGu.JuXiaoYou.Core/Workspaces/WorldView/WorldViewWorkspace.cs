// ----------------------------------------------------------
//            文件：V5Workspace.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 11:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Workspaces.WorldView
{
    public class WorldViewWorkspace : SpecificWorkspace
    {
        private readonly TopClassService _topClassService;
        private readonly SubClassService _subClassService;

        private readonly Dictionary<string, WorldViewWorkspaceItem> _dictionary;

        public WorldViewWorkspace()
        {
            _topClassService = DatabaseManager.GetService<TopClassService>();
            _subClassService = DatabaseManager.GetService<SubClassService>();
            _dictionary      = new Dictionary<string, WorldViewWorkspaceItem>();

            Worlds     = new WorldViewFolder { Name = "世界" };
            Uncensored = new WorldViewFolder { Name = "未分类" };

            Items.Add(Worlds);
            Items.Add(Uncensored);
        }

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/
        protected override async void OnStart()
        {
            //
            //
            await _topClassService.Start();
            await _subClassService.Start();

            var topClassTable = new Dictionary<string, TopClassWorkspaceItem>();
            var subClassTable = new Dictionary<string, SubClassWorkspaceItem>();

            //
            //
            foreach (var topClass in _topClassService.Collection)
            {
                var topClassWI = new TopClassWorkspaceItem
                {
                    Instance = topClass,
                };

                if (topClassTable.TryAdd(topClass.Id, topClassWI))
                {
                    Worlds.Items.Add(topClassWI);

                    _dictionary.TryAdd(topClass.Id, topClassWI);
                }
            }

            //
            //
            foreach (var subClass in _subClassService.Collection)
            {
                var subClassWI = new SubClassWorkspaceItem
                {
                    Instance = subClass,
                };

                if (subClassTable.TryAdd(subClass.Id, subClassWI))
                {
                    _dictionary.TryAdd(subClass.Id, subClassWI);
                }
            }

            //
            //
            foreach (var subClassWI in subClassTable.Values)
            {
                var subClass = subClassWI.Instance;

                if (string.IsNullOrEmpty(subClass.Parent))
                {
                    Uncensored.Items.Add(subClassWI);
                }
                else if (topClassTable.TryGetValue(subClass.Parent, out var topClassParent))
                {
                    topClassParent.Children.Add(subClassWI);
                }
                else if (subClassTable.TryGetValue(subClass.Parent, out var subClassParent))
                {
                    subClassParent.Children.Add(subClassWI);
                }
            }
        }


        public TopClassWorkspaceItem GetTopClassWorkspaceItem(string subClassParentID)
        {
            var detph = 0;
            
            while (!string.IsNullOrEmpty(subClassParentID))
            {
                if (!_dictionary.TryGetValue(subClassParentID, out var wi))
                {
                    break;
                }

                if (wi is TopClassWorkspaceItem tcWI)
                {
                    return tcWI;
                }
                
                if (wi is SubClassWorkspaceItem scWI)
                {
                    subClassParentID = scWI.ParentID;
                }

                detph++;

                if (detph > 256)
                {
                    break;
                }
            }

            return null;
        }

        /*******************************************************************
         *
         *
         *                      TopClass Methods
         *
         *
         *******************************************************************/
        public async Task AddTopClass(WorkspaceViewModel viewModel)
        {
            var r = await viewModel.SingleLine("新建", "创建一个新的世界。");

            if (!r.IsFinished)
            {
                return;
            }

            var topClass = new TopClass
            {
                Id    = ID.Get(),
                Name  = r.Value,
                Index = Worlds.Items.Count,
            };

            var topClassWI = new TopClassWorkspaceItem { Instance = topClass };

            await _topClassService.Add(topClass);

            Worlds.Items.Add(topClassWI);
            viewModel.SaveSuccess();
        }

        public async Task EditTopClass(WorkspaceViewModel viewModel, WorldViewWorkspaceItem item)
        {

            if (item is not TopClassWorkspaceItem topClassWI)
            {
                return;
            }

            var r = await viewModel.SingleLine("编辑", "编辑这个世界。", topClassWI.Instance.Name);

            if (!r.IsFinished)
            {
                return;
            }
            //
            //
            topClassWI.Instance.Name = r.Value;

            await _topClassService.Update(topClassWI.Instance);
            viewModel.SaveSuccess();
        }

        public async Task RemoveTopClass(WorkspaceViewModel viewModel, WorldViewWorkspaceItem item)
        {
            if (item is not TopClassWorkspaceItem topClassWI)
            {
                return;
            }

            if (!await viewModel.RemoveThis())
            {
                return;
            }

            try
            {

                await _topClassService.Remove(topClassWI.Instance);

                Worlds.Items.Remove(topClassWI);

                viewModel.RemoveSuccess();
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
        public async Task AddSubClass(WorkspaceViewModel viewModel, TopClassWorkspaceItem target)
        {
            var r = await viewModel.SingleLine("新建", "创建一个新的小世界。");

            if (!r.IsFinished)
            {
                return;
            }

            var subClass = new SubClass
            {
                Id     = ID.Get(),
                Name   = r.Value,
                Parent = target?.Id,
            };

            var subClassWI = new SubClassWorkspaceItem { Instance = subClass };

            if (target is null)
            {
                Uncensored.Items.Add(subClassWI);
            }
            else
            {
                //
                //
                subClass.Index = target.Children.Count;

                //
                // 添加父级。
                target.Children.Add(subClassWI);
            }

            await _subClassService.Add(subClass);
            viewModel.SaveSuccess();
        }

        public async Task AddSubClass(WorkspaceViewModel viewModel, SubClassWorkspaceItem target)
        {
            var r = await viewModel.SingleLine("编辑", "编辑这个小世界。");

            if (!r.IsFinished)
            {
                return;
            }

            var subClass = new SubClass
            {
                Id     = ID.Get(),
                Name   = r.Value,
                Parent = target?.Id,
            };

            var subClassWI = new SubClassWorkspaceItem { Instance = subClass };

            if (target is null)
            {
                Uncensored.Items.Add(subClassWI);
            }
            else
            {
                //
                //
                subClass.Index = target.Children.Count;

                //
                // 添加父级。
                target.Children.Add(subClassWI);
            }

            await _subClassService.Add(subClass);

            viewModel.SaveSuccess();
        }

        public async Task EditSubClass(WorkspaceViewModel viewModel, WorldViewWorkspaceItem item)
        {
            if (item is not SubClassWorkspaceItem subClassWI)
            {
                return;
            }

            var r = await viewModel.SingleLine("新建", "创建一个新的世界。", subClassWI.Instance.Name);

            if (!r.IsFinished)
            {
                return;
            }

            //
            //
            subClassWI.Instance.Name = r.Value;

            await _subClassService.Update(subClassWI.Instance);
            viewModel.SaveSuccess();
        }

        public async Task RemoveSubClass(WorkspaceViewModel viewModel, WorldViewWorkspaceItem item)
        {
            if (item is not SubClassWorkspaceItem subClassWI)
            {
                return;
            }

            if (!await viewModel.RemoveThis())
            {
                return;
            }

            try
            {

                await _subClassService.Remove(subClassWI.Instance);

                if (string.IsNullOrEmpty(subClassWI.ParentID))
                {
                    Uncensored.Items.Remove(subClassWI);
                }
                else if(_dictionary.TryGetValue(subClassWI.ParentID, out var parent))
                {
                    if (parent is TopClassWorkspaceItem tcWI)
                    {
                        tcWI.Children.Remove(subClassWI);
                    }
                    else if (parent is SubClassWorkspaceItem scWI)
                    {
                        scWI.Children.Remove(subClassWI);
                    }
                }

                viewModel.RemoveSuccess();
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
         *
         *
         *
         *******************************************************************/
        public WorkspaceFolder Worlds     { get; }
        public WorkspaceFolder Uncensored { get; }
    }
}