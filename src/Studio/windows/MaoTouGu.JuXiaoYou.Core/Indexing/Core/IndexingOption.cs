// ----------------------------------------------------------
//            文件：IndexingOption.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月28日 22:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public class IndexingOption : ObservableObject
    {
        private string _domain;
        private string _subject;
        private string _visualManager;

        private bool _allowCatalogOperation;

        private IndexingType _type;


        private string _name;



        public string GetInstanceID() => HashCode.Combine(Domain, Subject, VisualManager, Type)
                                                 .ToString("X");
        
        /// <summary>
        /// 名字。
        /// </summary>
        [BsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public IndexingType Type
        {
            get => _type;
            set => SetValue(ref _type, value);
        }

        public string ToBase64() => JSON2.ToBase64(this);

        /// <summary>
        /// 是否分类固定，不允许用户修改。
        /// </summary>
        public bool AllowCatalogOperation
        {
            get => _allowCatalogOperation;
            set => SetValue(ref _allowCatalogOperation, value);
        }

        public string VisualManager
        {
            get => _visualManager;
            set => SetValue(ref _visualManager, value);
        }

        public string Subject
        {
            get => _subject;
            set => SetValue(ref _subject, value);
        }

        public string Domain
        {
            get => _domain;
            set => SetValue(ref _domain, value);
        }


        /// <summary>
        /// 更多菜单中的命令。
        /// </summary>
        public ViewList<PseudoCommandItem> Commands { get; set; }
    }
}