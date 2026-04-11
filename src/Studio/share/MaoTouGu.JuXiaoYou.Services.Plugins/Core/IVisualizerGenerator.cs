// ----------------------------------------------------------
//            文件：IVisualizerGenerator.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public interface IVisualizerGenerator
    {
        /// <summary>
        /// 创建默认选项。
        /// </summary>
        /// <returns></returns>
        IVisualizerOptions CreateOptions();
        IVisualizerOptions CreateOptions(string base64);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Type ViewType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Type SettingType { get; }

        /// <summary>
        /// 
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
    }
}