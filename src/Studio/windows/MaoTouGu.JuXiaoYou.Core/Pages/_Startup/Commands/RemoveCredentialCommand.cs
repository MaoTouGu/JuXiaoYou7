using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaoTouGu.JuXiaoYou.Pages
{
    sealed class RemoveCredentialCommand(StartupViewModel target) : ContextCommand<Credential, StartupViewModel>(target)
    {
        protected override async void Execute(Credential credential)
        {
            var r = await Context.RemoveThis();

            if (!r)
            {
                return;
            }

            Context.Credentials.Remove(credential);
            Context.Project.Credentials.Remove(credential);

            GlobalSettings.SaveProjectSettings();
        }
    }
}
