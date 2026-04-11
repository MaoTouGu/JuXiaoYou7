// ----------------------------------------------------------
//            文件：VisualizerBlock.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 17:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Windows;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public sealed class VisualizerBlock : TypographyBlockVPO
    {
        public TypographyWithVisualizer Visualizer { get; init; }

        public object Control { get; init; }
    }
}