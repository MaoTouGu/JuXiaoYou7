// ----------------------------------------------------------
//            文件：UniversalMapper.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月06日 16:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database
{
    public class UniversalMapper : BsonMapperBase
    {
        public UniversalMapper()
        {
            EnumAsInteger = true;
        }

        public static UniversalMapper Instance { get; } = new UniversalMapper();
    }
}