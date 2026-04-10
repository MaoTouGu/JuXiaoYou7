// ----------------------------------------------------------
//            文件：Feature.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月27日 18:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    /// <summary>
    /// <see cref="Feature"/> 用于表示程序中一个可以继承的功能。
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// 表示是否使用外部导航器。
        /// </summary>
        public bool UseExternalNavigator { get; init; }

        /// <summary>
        /// 若<see cref="UseExternalNavigator"/>属性为true时，将调用外部导航器来实现FeaturePoint的跳转。
        /// </summary>
        public Type ExternalNavigator { get; init; }

        /// <summary>
        /// 提供可继承功能的具体类型。
        /// </summary>
        public Type Type { get; init; }

        /// <summary>
        /// ID唯一标识符。
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// 功能的正式名称。
        /// </summary>
        public string Name { get; init; }
    }
}