// ----------------------------------------------------------
//            文件：TypographyVisualizerVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 20:37
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using MaoTouGu.Studio.Database.Topology;

namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public sealed class TypographyVisualizerVPO : TypographyBlockVPO<TypographyWithVisualizer>
    {
        protected override TypographyBlockVPO OnCreate(TypographyWithVisualizer block, Moniker moniker)
        {
            return new TypographyVisualizerVPO
            {
                Instance = block,
                Moniker  = moniker,
            };
        }
    }
}