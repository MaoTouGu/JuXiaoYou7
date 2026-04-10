// ----------------------------------------------------------
//            文件：Folder.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月08日 02:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.References
{
    public class Folder : Nameable, IComparable<Folder>
    {
        public int CompareTo(Folder other)
        {
            if (ReferenceEquals(this, other))
                return 0;
            if (other is null)
                return 1;
            return Index.CompareTo(other.Index);
        }

        private int _count;
        private int _totalCount;

        /// <summary>
        /// 获取或设置 <see cref="TotalCount"/> 属性。
        /// </summary>
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int TotalCount
        {
            get => _totalCount;
            set => SetValue(ref _totalCount, value);
        }
        /// <summary>
        /// 用于统计数量的功能。
        /// </summary>
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }

        /// <summary>
        /// 父级
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// 当前的顺序。
        /// </summary>
        public int Index { get; set; }
    }
}