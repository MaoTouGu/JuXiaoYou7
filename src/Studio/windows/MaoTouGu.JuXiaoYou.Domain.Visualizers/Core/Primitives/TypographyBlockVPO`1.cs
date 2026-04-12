// ----------------------------------------------------------
//            文件：TypographyBlockVPO`1.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 20:32
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public abstract class TypographyBlockVPO<T> : TypographyBlockVPO where T : TypographyBlock
    {
        private readonly T _instance;

        protected sealed override bool CanAccept(TypographyBlock block) => block is T;
        protected sealed override TypographyBlockVPO OnCreate(TypographyBlock block, Moniker moniker) => OnCreate((T)block, moniker);

        protected abstract TypographyBlockVPO OnCreate(T block, Moniker moniker);


        public T Instance
        {
            get => _instance;
            init
            {
                Base        = value;
                _instance = value;
            }
        }
    }
}