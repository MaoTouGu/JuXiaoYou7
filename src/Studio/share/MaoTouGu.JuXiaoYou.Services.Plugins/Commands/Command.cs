// ----------------------------------------------------------
//            文件：Command.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月02日 17:09
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public sealed class Command : PseudoCommandItem
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Geometry的Base64形式。
        /// </summary>
        public string Icon { get; init; }
    }
}