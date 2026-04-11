// ----------------------------------------------------------
//            文件：TypographyTemplate.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月10日 23:51
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Foundation.Collections;
using MaoTouGu.Studio.Database.Topology;

namespace MaoTouGu.Studio.Database.Templates
{
    /// <summary>
    /// 排版模板。
    /// </summary>
    public sealed class TypographyTemplate : DatabaseObject
    {
        private int _width;

        /// <summary>
        /// 用来记录占用了什么设定。
        /// </summary>
        public HashSet<string> OccupiedTable { get; init; }
        
        public Dictionary<string, string> Base64Table { get; init; }

        /// <summary>
        /// 用来记录有多少个页面。
        /// </summary>
        public ViewList<TypographyPage> Pages { get; init; }

        /// <summary>
        /// 页面的宽度，范围在1000~4000之间，建议在1400左右。
        /// </summary>
        public int Width
        {
            get => _width;
            set => SetValue(ref _width, value);
        }
    }
}