// ----------------------------------------------------------
//            文件：WorldViewWorkspacePanel.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 11:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public partial class WorldViewWorkspacePanel : UserControl
    {
        public WorldViewWorkspacePanel()
        {
            InitializeComponent();
        }

        /*******************************************************************
         *
         *
         *                      TreeView Methods
         *
         *
         *******************************************************************/
        private void TreeView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not WorkspaceViewModel viewModel)
            {
                return;
            }

            if (e.OriginalSource is FrameworkElement fe && Xaml.FindVisualParent<TreeViewItem>(fe) is {} item)
            {
                if (item.DataContext is TopClassWorkspaceItem topClassWI)
                {
                    viewModel.Open(new WorldViewEditorViewModel(topClassWI, viewModel));
                }
                else if (item.DataContext is SubClassWorkspaceItem subClassWI)
                {
                    viewModel.Open(new WorldViewEditorViewModel(subClassWI, viewModel));
                }
            }
        }

        /*******************************************************************
         *
         *
         *                      TopClass Methods
         *
         *
         *******************************************************************/
        private async void MenuItem_AddTopClass(object sender, RoutedEventArgs e)
        {
            if (DataContext is not WorkspaceViewModel viewModel)
            {
                return;
            }

            var workspace = viewModel.WorldView;

            await workspace.AddTopClass(viewModel);
        }

        private async void MenuItem_EditTopClass(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetWorldViewWorkspace(sender,
                                               out var viewModel,
                                               out var item,
                                               out var workspace))
            {
                return;
            }

            await workspace.EditTopClass(viewModel, item);
        }

        private async void MenuItem_RemoveTopClass(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetWorldViewWorkspace(sender,
                                               out var viewModel,
                                               out var item,
                                               out var workspace))
            {
                return;
            }

            await workspace.RemoveTopClass(viewModel, item);
        }

        /*******************************************************************
         *
         *
         *                      SubClass Methods
         *
         *
         *******************************************************************/
        private async void MenuItem_AddSubClass(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetWorldViewWorkspace(sender,
                                               out var viewModel,
                                               out var item,
                                               out var workspace))
            {
                return;
            }

            if (item is SubClassWorkspaceItem parentSubClassWI)
            {
                await workspace.AddSubClass(viewModel, parentSubClassWI);
            }
            else if (item is TopClassWorkspaceItem parentTopClassWI)
            {
                await workspace.AddSubClass(viewModel, parentTopClassWI);
            }

        }

        private async void MenuItem_EditSubClass(object sender, RoutedEventArgs e)
        {

            if (!this.TryGetWorldViewWorkspace(sender,
                                               out var viewModel,
                                               out var item,
                                               out var workspace))
            {
                return;
            }

            await workspace.EditSubClass(viewModel, item);
        }

        private async void MenuItem_RemoveSubClass(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetWorldViewWorkspace(sender,
                                               out var viewModel,
                                               out var item,
                                               out var workspace))
            {
                return;
            }

            await workspace.RemoveSubClass(viewModel, item);
        }
    }
}