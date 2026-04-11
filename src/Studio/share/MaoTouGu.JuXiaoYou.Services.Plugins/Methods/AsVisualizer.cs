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
            if (generator is not null)
            {
                if (!Visualizers.TryAdd(generator.Id, generator))
                {
                    
                }
            }
        }
        public static void AsVisualizer<T>() where T : IVisualizerGenerator, new() => AsVisualizer(ClassStatic.CreateInstance<T>());
    }
}