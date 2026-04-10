// ----------------------------------------------------------
//            文件：IndexSystem.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月18日 01:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio;

namespace MaoTouGu.JuXiaoYou.Indexing
{
    public static partial class IndexSystem
    {
        public static readonly MonikerService     MonikerService;
        public static readonly KeywordService     KeywordService;
        public static readonly FolderService     CatalogService;
        public static readonly UniqueReferenceService UniqueReferenceService;


        private static volatile bool _IsInitialized;
        
        static IndexSystem()
        {
            UniqueReferenceService = DatabaseManager.GetService<UniqueReferenceService>();
            MonikerService     = DatabaseManager.GetService<MonikerService>();
            KeywordService     = DatabaseManager.GetService<KeywordService>();
            CatalogService     = DatabaseManager.GetService<FolderService>();
        }


        public static async Task InitializeAsync()
        {
            if (_IsInitialized)
            {
                return;
            }
            
            await MonikerService.Start();
            await KeywordService.Start();
            await CatalogService.Start();
            await UniqueReferenceService.Start();

            _IsInitialized = true;
        }
    }
}