// ----------------------------------------------------------
//            文件：ListViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年03月28日 22:23
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Indexing
{
    public sealed class ListViewModel : CatalogViewModelBase
    {
        public ListViewModel(IndexingOption option) : base(option)
        {
            Title = option.Name;
        }
        

        protected override async void OnStart()
        {
            base.OnStart();
            
            //
            // 初始化系统。
            await IndexSystem.InitializeAsync();

            //
            //
            await LoadCatalogAsync();
            await LoadMonikerAsync();
        }
    }
}