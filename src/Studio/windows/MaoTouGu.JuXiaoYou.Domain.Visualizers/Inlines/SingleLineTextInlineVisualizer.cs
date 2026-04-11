// ----------------------------------------------------------
//            文件：SingleLineTextInlineVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 00:05
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Plugins;

namespace MaoTouGu.JuXiaoYou.Visualizers.Inlines
{
    public class SingleLineTextInlineVisualizer 
    {

        public IVisualizerOptions CreateOptions() => throw new NotImplementedException();
        public object CreateControl() => throw new NotImplementedException();
        public string Id   { get; }
        public string Name { get; }
    }
}