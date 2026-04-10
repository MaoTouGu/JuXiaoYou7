// ----------------------------------------------------------
//            文件：GeographyPrototype.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月16日 13:14
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Core;

namespace MaoTouGu.JuXiaoYou.Domain.Geography.Models
{
    public sealed class GeographyPrototype : DatabaseObject
    {
        public string Name { get; set; }
        
        public int Height { get; set; }
        public int Width  { get; set; }

        public string Points { get; set; }
        public string Color  { get; set; }
    }
}