// ----------------------------------------------------------
//            文件：IPluginManifest.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月29日 20:30
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public interface IPluginManifest
    {
        void RegisterVisualManagers();
        void RegisterFeatures();
    }
}