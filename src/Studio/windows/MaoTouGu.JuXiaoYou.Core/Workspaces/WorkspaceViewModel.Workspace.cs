// ----------------------------------------------------------
//            文件：WorkspaceViewModel.Workspace.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月07日 13:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Workspaces.Graphing;
using MaoTouGu.Studio;

namespace MaoTouGu.JuXiaoYou.Workspaces
{
    partial class WorkspaceViewModel
    {
        /*
         *       +---- [页面]
         *       |
         *       +---- [页面]
         *       |
         *                 |    本地Api
         *                 +---[后台数据更新到前台]---+
         *                 |                      |
         *                 |                      |
         *                 |                      |
         *           [发送到服务器]                 |
         *                 |                      |
         *                 |                      |
         *                 |                      |
         *                 +------[服务器推送]------+
         */

        //
        // --+----> 设定
        //   |----> 共有13个设定
        //   |----> 共有13个设定
        //
        void BuildProperties()
        {
        }

        /*******************************************************************
         *
         *
         *
         *
         *
         *******************************************************************/

        public MonikerWorkspace   Moniker   { get; }
        public WorldViewWorkspace WorldView { get; }
        public TeamspaceWorkspace Teamspace { get; }
        public FeatureWorkspace   Feature   { get; }
        public GraphingWorkspace  Graphing  { get; }

    }
}