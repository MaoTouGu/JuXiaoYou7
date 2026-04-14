// ----------------------------------------------------------
//            文件：AsVisualizer.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public static partial class FeatureManager
    {
        
        public static void AsVisualizer(IVisualizerGenerator generator)
        {
            if (generator is  null)
            {
                return;
            }
            
            if (!Visualizers.TryAdd(generator.Id, generator))
            {
                    
            }
        }

        public static IEnumerable<IGravatarWideVisualizer> AsGravatarWide() => Visualizers.Values.OfType<IGravatarWideVisualizer>();
        public static IEnumerable<IInlineWideVisualizer> AsInlineWide() => Visualizers.Values.OfType<IInlineWideVisualizer>();
        public static IEnumerable<IDocumentWideVisualizer> AsDocumentWide() => Visualizers.Values.OfType<IDocumentWideVisualizer>();
        public static IEnumerable<IBlockWideVisualizer> AsBlockWide() => Visualizers.Values.OfType<IBlockWideVisualizer>();
        public static IEnumerable<IVisualizerGenerator> AsVisualizers() => Visualizers.Values;
        
        public static void AsVisualizer<T>() where T : IVisualizerGenerator, new() => AsVisualizer(ClassStatic.CreateInstance<T>());
    }
}