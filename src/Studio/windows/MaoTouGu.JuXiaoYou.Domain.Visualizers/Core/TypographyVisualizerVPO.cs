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
    public sealed class TypographyVisualizerVPO : TypographyBlockVPO
    {
        private readonly TypographyWithVisualizer _visualizer;

        public TypographyWithVisualizer Visualizer
        {
            get => _visualizer;
            init
            {
                Base        = value;
                _visualizer = value;
            }
        }
    }
}