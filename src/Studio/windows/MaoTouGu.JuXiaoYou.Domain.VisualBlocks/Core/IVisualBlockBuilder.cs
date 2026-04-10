// ----------------------------------------------------------
//            文件：IVisualBlockBuilder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 18:31
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Domain.VisualBlocks.Core
{
    public interface IVisualBlockBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // VisualBlock New();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        VisualBlock Build(string json);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        VisualBlockVPO Build(VisualBlock block);
        
        
        Type GetTemplatedControlType();
    }
}