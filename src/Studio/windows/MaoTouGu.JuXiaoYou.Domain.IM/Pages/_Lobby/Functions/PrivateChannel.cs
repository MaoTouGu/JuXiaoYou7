// ----------------------------------------------------------
//            文件：PrivateChannel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 19:26
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Domain.IM.Services;
using MaoTouGu.Shells.Inputs;
using Microsoft.AspNetCore.SignalR.Client;
namespace MaoTouGu.JuXiaoYou.Domain.IM.Pages
{
    public sealed partial class LobbyViewModel
    {
        private readonly PrivateMessageService _c2cSrv;
        
        private string _textBox;
        
        private async void OpenChannelAsync()
        {
            if (Target is null)
            {
                return;
            }
            
            //
            // TODO:
            var subjectID = MSG.GetSubjectID(User.Id, Target.Id);
            //var r         = await _c2cSrv.GetMessageCollectionAsync(subjectID);

            //
            //
            // C2CMsgCollection.AddMany(r.Select(GetVPO), true);
        }

        
        internal async Task SendMessage(MSG msg)
        {
            if (msg is null)
            {
                return;
            }

            await _connection.SendAsync(nameof(IPrivateHub.SendC2CAsync), msg);
            
            var vpo = GetVPO(msg);

            //
            //
            if (vpo is not null)
            {
                GUI.RunOnUIThread(() =>
                                  {
                                      C2CMsgCollection.Add(vpo);
                                  });
            }
        }

        public string TextBox
        {
            get => _textBox;
            set => SetValue(ref _textBox, value);
        }

        public ViewList<IMMessageVPO> C2CMsgCollection { get; }

        public ICommandEX AddPlainTextToC2C { get; }
        
        public ViewList<User> Other { get; }
        public ViewList<User> All   { get; }
    }
}