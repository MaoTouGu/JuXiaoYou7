// ----------------------------------------------------------
//            文件：IMMessageVPO.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 19:12
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------


namespace MaoTouGu.JuXiaoYou.Domain.IM.Models
{
    public abstract class IMMessageVPO : ObservableObject
    {
        protected IMMessageVPO(LobbyViewModel context, MSG msg)
        {
            IsSelf = context.User.Id == msg.SourceID;
            User   = context.User;
            Group  = context.GetGroup(msg.GroupID);
        }

        public BrainHoleAndCreative Group { get; }

        /// <summary>
        /// 是不是自己发送的消息。
        /// </summary>
        public bool IsSelf { get; }

        /// <summary>
        /// 创建此消息的用户。
        /// </summary>
        public User User { get; }
    }
}