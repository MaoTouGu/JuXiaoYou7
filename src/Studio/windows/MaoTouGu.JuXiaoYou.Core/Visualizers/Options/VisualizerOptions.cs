// ----------------------------------------------------------
//            文件：VisualizerOptions.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月12日 17:18
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Shells.Inputs;

namespace MaoTouGu.JuXiaoYou.Visualizers
{
    public abstract class VisualizerOptions : ObservableObject, IVisualizerOptions
    {
        protected VisualizerOptions()
        {
            Save = new DelegateCommand(DoSaveCommand);
        }

        public abstract IEnumerable<string> GetMetadataSources();

        protected void FireStructureChanged() => StructureChanged?.Invoke(this, null);
        protected void FireOptionChanged() => OptionChanged?.Invoke(this, null);
        
        private void DoSaveCommand()
        {
            FireStructureChanged();
            FireOptionChanged();
        }


        /// <summary>
        /// 创建此对象实例的副本。
        /// </summary>
        /// <returns></returns>
        public IVisualizerOptions CreateOptions() => CreateOptions(ToBase64());

        /// <summary>
        /// 创建此对象实例的副本。
        /// </summary>
        /// <param name="base64">base64编码的JSON负载。</param>
        /// <returns></returns>
        public IVisualizerOptions CreateOptions(string base64) => Clone(base64);

        /// <summary>
        /// 克隆对象。
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        protected abstract IVisualizerOptions Clone(string base64);

        /// <summary>
        /// 深复制。
        /// </summary>
        /// <returns>返回BASE64编码的JSON</returns>
        public string ToBase64() => JSON2.ToBase64(this);

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual int MinHeight => 40;
        
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual int MinWidth => 40;
        
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual AdjustMode AdjustMode => AdjustMode.Default;

        /// <summary>
        /// 由<see cref="VisualizerControl"/>传递的获得ViewModel的方法。
        /// </summary>
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        internal Func<JuXiaoYouPage> FactoryInternal { get; set; }

        /// <summary>
        /// 当前视觉呈现器的Id，仅在FeatureManager中使用，序列化反序列化时不工作。
        /// </summary>
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public abstract string Id { get; }

        /// <summary>
        /// 当前视觉呈现器的名字，仅在FeatureManager中使用，序列化反序列化时不工作。
        /// </summary>
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public abstract string Name { get; }
        

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public ICommandEX Save { get; }

        [BsonIgnore]
        public event EventHandler OptionChanged;

        [BsonIgnore]
        public event EventHandler StructureChanged;
    }
}