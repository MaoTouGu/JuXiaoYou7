// ----------------------------------------------------------
//            文件：TeamWorkspace.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 13:38
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.JuXiaoYou.Teamspace;

namespace MaoTouGu.JuXiaoYou.Workspaces
{
    public sealed class TeamspaceWorkspace : SpecificWorkspace
    {
        public TeamspaceWorkspace()
        {
            Voting = new VotingTeamspaceItem();
            Voted  = new VotingTeamspaceItem();
            Vote = new TeamspaceFolder
            {
                Items =
                {
                    Voting,
                    Voted,
                },
            };
            Items.Add(Vote);
        }

        public TeamspaceFolder     Vote   { get; }
        public VotingTeamspaceItem Voting { get; }
        public VotingTeamspaceItem Voted  { get; }


    }
}