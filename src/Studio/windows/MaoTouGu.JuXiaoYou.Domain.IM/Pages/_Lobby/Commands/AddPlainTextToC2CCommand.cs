// ----------------------------------------------------------
//            文件：AddPlainTextToC2CCommand.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 20:40
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Services.Networks;
using MaoTouGu.Shells.Inputs;
using MaoTouGu.Studio.Database.Utils;

namespace MaoTouGu.JuXiaoYou.Domain.IM.Pages.Commands
{
    public sealed class AddPlainTextToC2CCommand(LobbyViewModel target) : ContextCommand<LobbyViewModel>(target)
    {
        private DateTime _lastVerifyTime;
        
        
        private async Task<bool> VerifyAccess()
        {
            if ((DateTime.Now - _lastVerifyTime).TotalMinutes > 5)
            {
                //
                // 上次验证的时间大于5分钟，则在验证一次。
                _lastVerifyTime = DateTime.Now;
                
                //
                // 确保服务器仍然在线。
                if (!await ServerHealth.IsAlive(Context.WebApi.Url))
                {
                    return false;
                }
            }

            if (Context.Target is null)
            {
                Context.Warning("警告", "发送消息前请务必选择一个聊天对象。");
                return false;
            }

            return true;
        }

        private void ClearTextBox()
        {
            GUI.RunOnUIThread(() =>
                              {
                                  Context.TextBox = null;
                              });
        }
        
        public override async void Execute(object parameter)
        {
            if (!await VerifyAccess())
            {
                return;
            }

            if (string.IsNullOrEmpty(Context.TextBox))
            {
                Context.Warning("错误", "要发送的消息不能为空。");
                return;
            }

            var sid = Context.WebApi.UserID;
            var tid = Context.Target.Id;

            var text = new PlainTextIMMessage
            {
                Id       = ID.Get(),
                Text     = Context.TextBox,
                TargetID = tid,
                SourceID = sid,
                SubjectID = MSG.GetSubjectID(sid, tid),
            };

            //
            //
            await Context.SendMessage(text);

            //
            //
            ClearTextBox();
        }
    }
}