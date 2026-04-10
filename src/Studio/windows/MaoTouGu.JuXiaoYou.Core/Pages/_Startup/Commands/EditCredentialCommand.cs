// ----------------------------------------------------------
//            文件：EditCredentialCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月14日 14:59
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class EditCredentialCommand(StartupViewModel target) : ContextCommand<Credential, StartupViewModel>(target)
    {
        protected override async void Execute(Credential credential)
        {
            var r = await Context.Object<Credential>(new NewCredentialViewModel(credential));

            if (!r.IsFinished)
            {
                return;
            }
            
            if (r.Value.IsDefault && Context.Credentials.Any(x => x.IsDefault))
            {
                Context.Credentials
                       .ForEach(x => x.IsDefault = false);

                credential.IsDefault = true;
            }
            
            
            GlobalSettings.SaveProjectSettings();
        }
    }
}