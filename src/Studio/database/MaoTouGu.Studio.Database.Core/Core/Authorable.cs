// ----------------------------------------------------------
//            文件：Authorable.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月15日 00:41
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

namespace MaoTouGu.Studio.Database.Core
{
    public abstract class Authorable : DatabaseObject
    {
        private string _creatorName;
        
        /// <summary>
        /// 谁创建的。
        /// </summary>
        public string Creator { get; init; }
        
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [BsonIgnore]
        public string CreatorName
        {
            get => _creatorName;
            set => SetValue(ref _creatorName, value);
        }

        public bool IsSoftDeleted { get; set; }
    }
}