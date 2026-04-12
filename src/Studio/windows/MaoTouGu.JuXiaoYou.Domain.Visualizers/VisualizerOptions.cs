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
        public abstract IEnumerable<string> GetMetadataSources();

        public IVisualizerOptions CreateOptions() => CreateOptions(ToBase64());
        public IVisualizerOptions CreateOptions(string base64) => Clone(base64);
        
        protected abstract IVisualizerOptions Clone(string base64);

        public string ToBase64() => JSON2.ToBase64(this);

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public abstract string Id { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public abstract string Name { get; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Type ViewType => typeof(TView);

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Type SettingType => typeof(TSetting);


    }

}