// ----------------------------------------------------------
//            文件：VisualBlock`3.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 19:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Runtime.InteropServices;
using MaoTouGu.Studio.Database.Entities.VisualBlocks;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.Studio.Database.Entities.VisualBlocks
{
    public abstract class VisualBlock<T, E, K> : VisualBlock, IVisualBlockBuilder
        where T : VisualBlock
        where E : VisualBlockVPO<T>, new()
        where K : class, new()
    {
        VisualBlock IVisualBlockBuilder.Build(string json) => JSON.Deserialize<T>(json);

        VisualBlockVPO IVisualBlockBuilder.Build(VisualBlock block)
        {
            if (block is not T t)
            {
                return null;
            }

            return new E
            {
                Block = t,
            };
        }
        
        Type IVisualBlockBuilder.GetTemplatedControlType() => typeof(K);
    }
}