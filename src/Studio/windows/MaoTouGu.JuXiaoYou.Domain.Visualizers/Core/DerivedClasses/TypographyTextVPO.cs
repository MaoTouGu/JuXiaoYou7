// ----------------------------------------------------------
//            文件：TypographyTextVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 14:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public class TypographyTextVPO : TypographyBlockVPO<TypographyText>
    {
        protected override TypographyBlockVPO OnCreate(TypographyText block, Moniker moniker)
        {
            return new TypographyTextVPO
            {
                Moniker  = moniker,
                Instance = block,
            };
        }

    }
}