// ----------------------------------------------------------
//            文件：IVisualizerSettingWorker.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 18:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers.Core
{
    public interface IVisualizerSettingWorker
    {

        void DoWork(Moniker moniker, IVisualizerOptions options, TypographyBlockVPO target, string name);
    }
}