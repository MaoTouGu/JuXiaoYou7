// ----------------------------------------------------------
//            文件：ModuleControl.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 17:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Core;

namespace MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Controls
{
    public abstract class ModuleControl : UserControl
    {
        protected ModuleControl()
        {
            VisualConnector.SetConnect(this, true);
        }
    }
}