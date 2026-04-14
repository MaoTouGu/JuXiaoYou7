// ----------------------------------------------------------
//            文件：MonikerWorkspacePanel.xaml.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 00:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Indexing;

namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public partial class MonikerWorkspacePanel : UserControl
    {
        public MonikerWorkspacePanel()
        {
            InitializeComponent();
        }
        /*******************************************************************
         *
         *
         *                      TreeView
         *
         *
         *******************************************************************/

        private async void TreeView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not WorkspaceViewModel viewModel)
            {
                return;
            }

            if (e.OriginalSource is not FrameworkElement fe || Xaml.FindVisualParent<TreeViewItem>(fe) is not {} item)
            {
                return;
            }


            if (item.DataContext is SubClassWorkspaceItem subClassWI)
            {
                var topClassWI = viewModel.WorldView
                                          .GetTopClassWorkspaceItem(subClassWI.ParentID);
                
                viewModel.Open(new FilterViewModel(topClassWI.Instance, subClassWI.Instance, viewModel));
                return;
            }


            if (item.DataContext is MonikerWrapperItem wrapper)
            {
                await viewModel.Navigate(new MonikerEditorViewModel(wrapper.Moniker));
                viewModel.Open(new MonikerTransitViewModel(wrapper.Moniker, viewModel));
                return;
            }

            if (item.DataContext is LabelWrapperItem labelWrapper)
            {
                viewModel.Open(new FilterViewModel(labelWrapper.Label, viewModel));
                return;
            }

            if (item.DataContext is FolderWrapperItem folderWrapper)
            {
                viewModel.Open(new FilterViewModel(folderWrapper.Folder, viewModel));
                return;
            }


            if (item.DataContext is BySettingFilterMethodItem method && method.Filter is {} filter)
            {
                viewModel.Open(new FilterViewModel(filter, viewModel));
                return;
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

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

        /*******************************************************************
         *
         *
         *                      Label Methods
         *
         *
         *******************************************************************/
        private async void MenuItem_Add(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetMonikerWorkspace(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            if (item is TopClassWorkspaceItem topClassWI)
            {
                await workspace.Add(viewModel, topClassWI);
                return;
            }

            if (item is SubClassWorkspaceItem subClassWI)
            {
                await workspace.Add(viewModel, subClassWI);
                return;
            }

            if (item is FolderWrapperItem folderWrapper)
            {
                await workspace.Add(viewModel, folderWrapper);
                return;
            }

            if (item is LabelWrapperItem labelWrapper)
            {
                await workspace.Add(viewModel, labelWrapper);
                return;
            }

        }
        
        private async void MenuItem_AddExists(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetMonikerWorkspace(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            if (item is TopClassWorkspaceItem topClassWI)
            {
                await workspace.Add(viewModel, topClassWI);
                return;
            }

            if (item is SubClassWorkspaceItem subClassWI)
            {
                await workspace.Add(viewModel, subClassWI);
                return;
            }

            if (item is FolderWrapperItem folderWrapper)
            {
                await workspace.Add(viewModel, folderWrapper);
                return;
            }

            if (item is LabelWrapperItem labelWrapper)
            {
                await workspace.Add(viewModel, labelWrapper);
                return;
            }

        }

        private void MenuItem_Edit(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Recovery(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
        }

        /*******************************************************************
         *
         *
         *                      Label Methods
         *
         *
         *******************************************************************/
        private async void MenuItem_AddLabel(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetLabelContext(sender,
                                         out var viewModel,
                                         out var item,
                                         out var workspace))
            {
                return;
            }

            await workspace.AddLabel(viewModel, item);
        }

        private async void MenuItem_EditLabel(object sender, RoutedEventArgs e)
        {

            if (!this.TryGetLabelContext(sender,
                                         out var viewModel,
                                         out var item,
                                         out var workspace))
            {
                return;
            }

            await workspace.EditLabel(viewModel, item);
        }

        private async void MenuItem_RemoveLabel(object sender, RoutedEventArgs e)
        {

            if (!this.TryGetLabelContext(sender,
                                         out var viewModel,
                                         out var item,
                                         out var workspace))
            {
                return;
            }

            await workspace.RemoveLabel(viewModel, item);
        }


        /*******************************************************************
         *
         *
         *                      Folder Methods
         *
         *
         *******************************************************************/
        private async void MenuItem_AddFolder(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetFolderContext(sender,
                                          out var viewModel,
                                          out var item,
                                          out var workspace))
            {
                return;
            }

            await workspace.AddFolder(viewModel, item);
        }

        private async void MenuItem_EditFolder(object sender, RoutedEventArgs e)
        {

            if (!this.TryGetFolderContext(sender,
                                          out var viewModel,
                                          out var item,
                                          out var workspace))
            {
                return;
            }

            await workspace.EditFolder(viewModel, item);
        }

        private async void MenuItem_RemoveFolder(object sender, RoutedEventArgs e)
        {

            if (!this.TryGetFolderContext(sender,
                                          out var viewModel,
                                          out var item,
                                          out var workspace))
            {
                return;
            }

            await workspace.RemoveFolder(viewModel, item);
        }

        /*******************************************************************
         *
         *
         *                      Folder Methods
         *
         *
         *******************************************************************/
        private async void MenuItem_AddFilter(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetFilterContext(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            await workspace.AddFilter(viewModel);
        }

        private async void MenuItem_EditFilter(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetFilterContext(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            await workspace.EditFilter(viewModel, item);
        }

        private void MenuItem_ExportFilter(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetFilterContext(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            workspace.ExportFilter(viewModel, item);
        }

        private async void MenuItem_ImportFilter(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetFilterContext(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            await workspace.ImportFilter(viewModel, item);
        }

        private async void MenuItem_RemoveFilter(object sender, RoutedEventArgs e)
        {
            if (!this.TryGetFilterContext(sender, out var viewModel, out var item, out var workspace))
            {
                return;
            }

            await workspace.RemoveFilter(viewModel, item);
        }
    }
}