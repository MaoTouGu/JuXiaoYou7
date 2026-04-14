// ----------------------------------------------------------
//            文件：IVisualizerOptions.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 17:08
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.ComponentModel;
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database.Templates;

namespace MaoTouGu.JuXiaoYou.Services.Plugins
{
    public interface IVisualizerOptions : INotifyPropertyChanged, IMetadataSourceProvider
    {
        string ToBase64();

        event EventHandler OptionChanged;
        
        //
        // 例如RadarVisualizer：
        // 当RadarVisualizer中的集合Create、Delete时以及内部数据Update时，将会发生的改变。
        
        /// <summary>
        /// 结构调整
        /// </summary>
        event EventHandler StructureChanged;

        int MinWidth  { get; }
        int MinHeight { get; }
        
        AdjustMode AdjustMode { get; }
    }
}