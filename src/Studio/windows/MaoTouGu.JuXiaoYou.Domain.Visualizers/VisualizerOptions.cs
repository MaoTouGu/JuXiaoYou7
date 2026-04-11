// ----------------------------------------------------------
//            文件：VisualizerOptions.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public abstract class VisualizerOptions<TSetting, TView> : ObservableObject, IVisualizerOptions, IVisualizerGenerator
        where TSetting : UserControl
        where TView : VisualizerControl
    {

        public IVisualizerOptions CreateOptions() => CreateOptions(ToBase64());
        public IVisualizerOptions CreateOptions(string base64) => Clone(base64);
        protected abstract IVisualizerOptions Clone(string base64);

        public string ToBase64() => JSON2.ToBase64(this);

        public abstract string Id   { get; }
        public abstract string Name { get; }

        public Type ViewType    => typeof(TView);
        public Type SettingType => typeof(TSetting);


    }

}