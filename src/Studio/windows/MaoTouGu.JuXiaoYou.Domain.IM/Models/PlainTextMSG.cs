// ----------------------------------------------------------
//            文件：PlainTextMSG.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 20:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou.Domain.IM.Models
{
    public sealed class PlainTextMSG : IMMessageVPO
    {
        public PlainTextMSG(LobbyViewModel context, PlainTextIMMessage msg) : base(context, msg)
        {
            Target = msg;
        }

        public string Text => Target.Text;

        public PlainTextIMMessage Target { get;}
    }
}