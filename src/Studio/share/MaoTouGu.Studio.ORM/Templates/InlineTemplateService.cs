// ----------------------------------------------------------
//            文件：InlineTemplateService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月13日 16:36
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.Studio.Templates
{
    public class InlineTemplateService : AsyncCollectionService<InlineTemplate>
    {
        public InlineTemplateService() : base(EngineNames.Visualizer, CollectionNames.InlineTemplate)
        {

        }

        protected override async Task OnCollectionSetupAsync()
        {
            await Add(new InlineTemplate
            {
                Id    = ID.Get(),
                Items = new List<InlineTemplateItem>(),
                Name  = "默认方案",
            });
        }
    }
}