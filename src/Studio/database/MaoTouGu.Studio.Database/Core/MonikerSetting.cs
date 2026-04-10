// ----------------------------------------------------------
//            文件：MonikerSetting.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月17日 20:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Globalization;

namespace MaoTouGu.Studio.Database.Core
{
    public class MonikerSetting
    {
        public MonikerSetting(){}
        public MonikerSetting(string value) => Value = value;
        
        public string Value { get; set; }

        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int AsInt
        {
            get => int.TryParse(Value, out var result) ? result : 0;
            set
            {
                Value = value.ToString();
            }
        }
        
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool AsBoolean
        {
            get => bool.TryParse(Value, out var result) && result;
            set
            {
                Value = value.ToString();
            }
        }
        
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public float AsFloat
        {
            get => float.TryParse(Value, out var result)  ? result : 0f;
            set
            {
                Value = value.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}