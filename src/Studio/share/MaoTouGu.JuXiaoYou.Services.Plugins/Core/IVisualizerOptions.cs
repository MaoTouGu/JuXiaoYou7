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
    }
}