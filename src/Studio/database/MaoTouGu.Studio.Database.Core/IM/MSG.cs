// ----------------------------------------------------------
//            文件：MSG.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 13:22
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Numerics;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Identity;

namespace MaoTouGu.Studio.Database.IM
{
    public abstract class MSG : DatabaseObject
    {
        static BigInteger GuidToInt(Guid g)
        {
            var bytes = g.ToByteArray();
            return new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());
        }
        
        static BigInteger Pair(BigInteger a, BigInteger b)
        {
            var x = BigInteger.Min(a, b);
            var y = BigInteger.Max(a, b);
            return (x + y) * (x + y + 1) / 2 + y;
        }

        public static string GetSubjectID(string a, string b)
        {
            var g1 = Guid.Parse(a);
            var g2 = Guid.Parse(b);
            var i  = Pair(GuidToInt(g1), GuidToInt(g2));
            var x  = i.ToString("x");
            return User.Hash(x);
        }
        
        /// <summary>
        /// 消息的顺序。
        /// </summary>
        public long Index { get; set; }

        public string SourceID  { get; init; }
        public string TargetID  { get; init; }
        public string SubjectID { get; init; }
        public string GroupID   { get; init; }
    }
}