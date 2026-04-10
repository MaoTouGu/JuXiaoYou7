// ----------------------------------------------------------
//            文件：Channels.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:39
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Threading.Channels;
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.IM;
using MaoTouGu.Studio.Database.Operations;

namespace MaoTouGu.Studio.Core
{
    public static class Channels
    {
        static Channels()
        {
            Login    = Channel.CreateUnbounded<IdentityOperation>();
            Security = Channel.CreateUnbounded<SecurityOperation>();
            Private  = Channel.CreateUnbounded<MSG>();
            Public   = Channel.CreateUnbounded<MSG>();
        }

        public static Channel<IdentityOperation> Login    { get; }
        public static Channel<SecurityOperation> Security { get; }
        public static Channel<MSG> Private { get; }
        public static Channel<MSG> Public { get; }
    }
}