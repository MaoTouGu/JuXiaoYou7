// ----------------------------------------------------------
//            文件：WorkspaceTreeViewHelper.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月07日 22:57
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Workspaces
{
    static class WorkspaceTreeViewHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="sender"></param>
        /// <param name="viewModel"></param>
        /// <param name="item"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        internal static bool TryGetWorldViewWorkspace(
            this UserControl userControl,
            object sender,
            out WorkspaceViewModel viewModel,
            out WorldViewWorkspaceItem item,
            out WorldViewWorkspace workspace)
        {
            if (userControl.DataContext is not WorkspaceViewModel model ||
                sender is not MenuItem { CommandParameter: WorldViewWorkspaceItem item2 })
            {
                viewModel = null;
                item      = null;
                workspace = null;
                return false;
            }

            viewModel = model;
            workspace = model.WorldView;
            item      = item2;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="sender"></param>
        /// <param name="viewModel"></param>
        /// <param name="item"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        internal static bool TryGetMonikerWorkspace(
            this UserControl userControl,
            object sender,
            out WorkspaceViewModel viewModel,
            out WorkspaceItem item,
            out MonikerWorkspace workspace)
        {
            if (userControl.DataContext is not WorkspaceViewModel model ||
                sender is not MenuItem { CommandParameter: WorkspaceItem item2 })
            {
                viewModel = null;
                item      = null;
                workspace = null;
                return false;
            }

            viewModel = model;
            workspace = model.Moniker;
            item      = item2;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="sender"></param>
        /// <param name="viewModel"></param>
        /// <param name="item"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        internal static bool TryGetFolderContext(
            this UserControl userControl,
            object sender,
            out WorkspaceViewModel viewModel,
            out FolderWrapperItem item,
            out MonikerWorkspace workspace)
        {
            if (userControl.DataContext is not WorkspaceViewModel model)
            {
                viewModel = null;
                item      = null;
                workspace = null;
                return false;
            }

            viewModel = model;
            workspace = model.Moniker;

            if (sender is not MenuItem { CommandParameter: FolderWrapperItem item2 })
            {
                item = null;
            }
            else
            {
                item = item2;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="sender"></param>
        /// <param name="viewModel"></param>
        /// <param name="item"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        internal static bool TryGetLabelContext(
            this UserControl userControl,
            object sender,
            out WorkspaceViewModel viewModel,
            out LabelWrapperItem item,
            out MonikerWorkspace workspace)
        {
            if (userControl.DataContext is not WorkspaceViewModel model)
            {
                viewModel = null;
                item      = null;
                workspace = null;
                return false;
            }

            viewModel = model;
            workspace = model.Moniker;

            if (sender is not MenuItem { CommandParameter: LabelWrapperItem item2 })
            {
                item = null;
            }
            else
            {
                item = item2;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="sender"></param>
        /// <param name="viewModel"></param>
        /// <param name="item"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        internal static bool TryGetFilterContext(
            this UserControl userControl,
            object sender,
            out WorkspaceViewModel viewModel,
            out BySettingFilterMethodItem item,
            out MonikerWorkspace workspace)
        {
            if (userControl.DataContext is not WorkspaceViewModel model)
            {
                viewModel = null;
                item      = null;
                workspace = null;
                return false;
            }

            viewModel = model;
            workspace = model.Moniker;

            if (sender is not MenuItem { CommandParameter: BySettingFilterMethodItem item2 })
            {
                item = null;
            }
            else
            {
                item = item2;
            }

            return true;
        }
    }
}