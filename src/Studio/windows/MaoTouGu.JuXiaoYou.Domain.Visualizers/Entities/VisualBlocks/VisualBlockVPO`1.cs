// ----------------------------------------------------------
//            文件：VisualBlockVPO`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 18:21
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Entities.VisualBlocks
{
    public abstract class VisualBlockVPO<T> : VisualBlockVPO where T : VisualBlock
    {
        protected sealed override VisualBlock GetVisualBlock() => Block;

        public T Block { get; init; }
    }
}