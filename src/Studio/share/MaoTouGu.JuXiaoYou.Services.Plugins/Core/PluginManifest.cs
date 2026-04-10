// ----------------------------------------------------------
//            文件：PluginManifest.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 02:25
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public abstract class PluginManifest : ObservableObject, IPluginManifest
    {

        public abstract void RegisterVisualManagers();
        public abstract void RegisterFeatures();


        public string Path => GetType().Assembly.Location;
    }
}