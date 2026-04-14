// ----------------------------------------------------------
//            文件：VisualizerOptions`2.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月11日 18:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public abstract class VisualizerOptions<TSetting, TView> : VisualizerOptions, IVisualizerGenerator
        where TSetting : UserControl
        where TView : VisualizerControl
    {


        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        protected JuXiaoYouPage ViewModel => FactoryInternal();

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