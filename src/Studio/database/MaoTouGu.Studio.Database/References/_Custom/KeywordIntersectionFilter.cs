// ----------------------------------------------------------
//            文件：KeywordIntersectionFilter.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 21:01
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Text;
using MaoTouGu.Foundation.Collections;

namespace MaoTouGu.Studio.Database.References
{
    /// <summary>
    /// 标签交集过滤器。
    /// </summary>
    public sealed class KeywordIntersectionFilter : CustomFilter
    {
        private string Combine()
        {
            var temp = Keywords.ToList();
            temp.Sort((a, b) => string.Compare(a, b, StringComparison.OrdinalIgnoreCase));

            var sb = new StringBuilder();
            
            foreach (var v in temp)
            {
                sb.Append(v);
            }

            return sb.ToString();
        }

        public override bool Equals(CustomFilter other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other is not KeywordIntersectionFilter filter)
            {
                return false;
            }
            
            return Equals(Combine(), filter.Combine());
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((KeywordIntersectionFilter)obj);
        }
        
        public override int GetHashCode() => Combine().GetHashCode();

        //
        // 同时拥有标签的过滤器。
        public ViewList<string> Keywords { get; init; }
    }
}