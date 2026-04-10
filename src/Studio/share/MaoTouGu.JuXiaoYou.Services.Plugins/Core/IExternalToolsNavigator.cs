// ----------------------------------------------------------
//            文件：IExternalToolsNavigator.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 13:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Core;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public interface IExternalToolsNavigator
    {
        Task Navigate(IAppModel model, string name, string payload);
    }
}